using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonUI : MonoBehaviour
{
    private Dictionary<BuildingType, Transform> buttonTransformDictionary;
    [SerializeField] private Transform buttonTemplate;
    [SerializeField] private Sprite mouseSprite;

    private void Awake()
    {
        buttonTransformDictionary = new Dictionary<BuildingType, Transform>();
        buttonTemplate.gameObject.SetActive(false);
        BuildingTypeList buildingTypeList = Resources.Load<BuildingTypeList>(typeof(BuildingTypeList).Name);
        int index = 0;
        foreach (var buildingType in buildingTypeList.list)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            float positionOffset = 160f;
            buttonTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(index * positionOffset, 0f);
            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.ChangeActiveBuildingType(buildingType);
            });
            buttonTransformDictionary[buildingType] = buttonTransform;
            index++;
        }
    }
    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        UpdateButtonUI();
    }
    // This function will be called whenever OnActiveBuildingTypeChanged event on the BuildingManager is triggered.
    //It Updates the SelectedButtonUI with a white circle around it.
    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        UpdateButtonUI();
    }

    //This function will Update the UI for the selected Building Button
    //And sets the others to default by hiding their "Selected" Image.
    private void UpdateButtonUI()
    {
        foreach (var buildingType in buttonTransformDictionary.Keys)
        {
            Transform buttonTransform = buttonTransformDictionary[buildingType];
            buttonTransform.Find("Selected").gameObject.SetActive(false);
        }
        BuildingType activeBuildingType = BuildingManager.Instance.ReturnActiveBuildingType();
        buttonTransformDictionary[activeBuildingType].Find("Selected").gameObject.SetActive(true);
    }
}

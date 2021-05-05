using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonUI : MonoBehaviour
{
    private Dictionary<BuildingType, Transform> buttonTransformDictionary;
    [SerializeField] private Transform buttonTemplate;
    [SerializeField] private Sprite mouseSprite;
    private Transform arrowButton;
    [SerializeField] private List<BuildingType> ignoredBuildingTypeList;

    private void Awake()
    {
        buttonTransformDictionary = new Dictionary<BuildingType, Transform>();
        buttonTemplate.gameObject.SetActive(false);
        BuildingTypeList buildingTypeList = Resources.Load<BuildingTypeList>(typeof(BuildingTypeList).Name);
        int index = 0;
        // Create Mouse Button
        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);
        float positionOffset = 160f;
        arrowButton.Find("Image").GetComponent<Image>().sprite = mouseSprite;
        arrowButton.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(index * positionOffset, 0f);
        
        // Add a listener to the button so when it is clicked it will change the active building type.
        arrowButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.ChangeActiveBuildingType(null);
        });
        //Add listener so that when mouse is on the arrow button, it will show the Tooltip with "Arrow".
        MouseEnterExitEvents mouseEnterExitEvents = arrowButton.GetComponent<MouseEnterExitEvents>();
        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => {
            TooltipUI.Instance.Display("Arrow");
        };
        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => {
            TooltipUI.Instance.Hide();
        };
        index++;
        // Create Building Buttons
        foreach (var buildingType in buildingTypeList.list)
        {
            // Check if the building type needs to be ignored or not.
            if (ignoredBuildingTypeList.Contains(buildingType)) continue;
            //Create the button and place it next to the previous button.
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            buttonTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(index * positionOffset, 0f);
            // Add a listener to the button so when it is clicked it will change the active building type.
            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.ChangeActiveBuildingType(buildingType);
            });
            // Add listener so that when mouse is on the button, it will show the Tooltip with building type name.
            mouseEnterExitEvents = buttonTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => {
                TooltipUI.Instance.Display(buildingType.name + "\n" + buildingType.GetBuildingInformationString());
            };
            // Add lister so that when mouse is exit the object, it will hide the Tooltip.
            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => {
                TooltipUI.Instance.Hide();
            };

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
        arrowButton.Find("Selected").gameObject.SetActive(false);
        foreach (var buildingType in buttonTransformDictionary.Keys)
        {
            Transform buttonTransform = buttonTransformDictionary[buildingType];
            buttonTransform.Find("Selected").gameObject.SetActive(false);
        }
        BuildingType activeBuildingType = BuildingManager.Instance.ReturnActiveBuildingType();
        if(activeBuildingType == null)
            arrowButton.Find("Selected").gameObject.SetActive(true);
        else
            buttonTransformDictionary[activeBuildingType].Find("Selected").gameObject.SetActive(true);
    }
}

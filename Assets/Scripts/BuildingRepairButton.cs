using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingRepairButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ResourceType goldResourceType;

    private void Awake()
    {
        button.onClick.AddListener(() => 
        {
            //Calculate the repair cost.
            int missingHealth = healthSystem.GetMaxHealth() - healthSystem.GetCurrentHealth();
            int repairCost = missingHealth / 2;
            //Check if we can afford the repair cost.
            ResourceAmount[] resourceAmountCost = new ResourceAmount[] { new ResourceAmount { resourceType = goldResourceType, amount = repairCost } };
            if (ResourceManager.Instance.CanAffordBuilding(resourceAmountCost))
            {
                //Heal the building.
                ResourceManager.Instance.PayBuildingCost(resourceAmountCost);
                healthSystem.HealFull();
            }
            else
            {
                //Show player that they cant afford the cost
                TooltipUI.Instance.Display("Can't afford repair cost!", new TooltipUI.ToolTipTimer { timer = 2f });
            }
        });
    }
}

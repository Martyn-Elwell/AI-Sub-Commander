using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

public class SpawnController : MonoBehaviour
{
    [Header("Prefabs")]
    [HideInInspector]public GameObject Prefab;
    public GameObject commanderPrefab;
    public GameObject unitPrefab;
    public GameObject technicianPrefab;
    public GameObject reconPrefab;
    public unitEnum currentUnit = unitEnum.NONE;

    [Header("Costs")]
    public int unitCost = 1;
    public int technicianCost = 2;
    public int reconCost = 2;
    public int commanderCost = 3;
    public int maxUnitCost = 20; // Maximum total unit cost
    public int totalUnitCost = 0; // Total unit cost
    [SerializeField] private Text unitCostText;

    [Header("GlobalCounts")]
    public int commanderCount;
    public int unitCount;
    public int technicianCount;
    public int reconCount;



    public void SetCurrentUnit(string input)
    {
        switch (input)
        {
            case "commander":
                Prefab = commanderPrefab;
                currentUnit = unitEnum.COMMANDER;
                break;
            case "unit":
                Prefab = unitPrefab;
                currentUnit = unitEnum.UNIT;
                break;
            case "technician":
                Prefab = technicianPrefab;
                currentUnit = unitEnum.TECHNICIAN;
                break;
            case "recon":
                Prefab = reconPrefab;
                currentUnit = unitEnum.RECON;
                break;
        }

    }

    public bool checkNewCosts()
    {
        if (currentUnit == unitEnum.COMMANDER) { return false; }
        totalUnitCost = commanderCount * commanderCost + unitCount * unitCost + technicianCount * technicianCost + reconCount * reconCost;
        int newUnitCost = totalUnitCost;

        switch (currentUnit)
        {
            case unitEnum.COMMANDER:
                newUnitCost += commanderCost;
                break;
            case unitEnum.UNIT:
                newUnitCost += unitCost;
                break;
            case unitEnum.TECHNICIAN:
                newUnitCost += technicianCost;
                break;
            case unitEnum.RECON:
                newUnitCost += reconCost;
                break;
        }

        if (newUnitCost <= maxUnitCost)
        {
            totalUnitCost = newUnitCost;
            UpdateUnitCostText();
            return true;
        }
        return false;
    }

    public bool checkActivation()
    {
        totalUnitCost = commanderCount * commanderCost + unitCount * unitCost + technicianCount * technicianCost + reconCount * reconCost;
        int newUnitCost = totalUnitCost += commanderCost;

        if (newUnitCost <= maxUnitCost)
        {
            totalUnitCost = newUnitCost;
            UpdateUnitCostText();
            return true;
        }
        return false;
    }

    private void UpdateUnitCostText()
    {
        unitCostText.text = "Total Unit Cost: " + totalUnitCost + " / " + maxUnitCost;
    }
}

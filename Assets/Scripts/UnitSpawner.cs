using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using System.Linq;
using UnityEngine.Windows;
using Unity.VisualScripting;

public class UnitSpawner : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] private SpawnController controller;
    public bool active = false;

    private List<GameObject> prefabs = new List<GameObject>();

    [Header("Counts")]
    [SerializeField] private int commanderCount;
    [SerializeField] private int unitCount;
    [SerializeField] private int technicianCount;
    [SerializeField] private int reconCount;

    [Header("Spacing Paramenters")]
    public Transform unitSpawnpoint;
    public int gridSpacing = 2; // Spacing between grid elements

    public List<GameObject> instantiatedUnits = new List<GameObject>();

    [Header("UI")]
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;


    
    private void Start()
    {
        prefabs.Add(controller.commanderPrefab);
        prefabs.Add(controller.unitPrefab);
        prefabs.Add(controller.technicianPrefab);
        prefabs.Add(controller.reconPrefab);
    }

    private void Update()
    {

    }

    public void OnButtonClick()
    {
        if (!active)
        {
            Activate();
        }
        else
        {
            if (!controller.checkNewCosts()) { return; }
            switch (controller.currentUnit)
            {
                case unitEnum.COMMANDER:
                    commanderCount += 1;
                    controller.commanderCount += 1;
                    InstantiateUnits();
                    instantiatedUnits[0].GetComponent<Commander>().SetSquad(instantiatedUnits);
                    break;
                case unitEnum.UNIT:
                    unitCount += 1;
                    controller.unitCount += 1;
                    InstantiateUnits();
                    instantiatedUnits[0].GetComponent<Commander>().SetSquad(instantiatedUnits);
                    break;
                case unitEnum.TECHNICIAN:
                    technicianCount += 1;
                    controller.technicianCount += 1;
                    InstantiateUnits();
                    instantiatedUnits[0].GetComponent<Commander>().SetSquad(instantiatedUnits);
                    break;
                case unitEnum.RECON:
                    reconCount += 1;
                    controller.reconCount += 1;
                    InstantiateUnits();
                    instantiatedUnits[0].GetComponent<Commander>().SetSquad(instantiatedUnits);
                    break;
            }
        }
    }

    private void Activate()
    {
        if (!controller.checkActivation()) { return; }
        active = true;
        commanderCount = 1;
        InstantiateUnits();
        controller.commanderCount += commanderCount;
        buttonImage.sprite = activeSprite;

    }

    private int InstantiateUnits()
    {
        ClearInstantiatedUnits();

        List<int> counts = new List<int>();
        counts.Add(commanderCount); counts.Add(unitCount); counts.Add(technicianCount); counts.Add(reconCount);
        int count = commanderCount + unitCount + technicianCount + reconCount;


        int returnValue = 0;
        int index = 0;
        int loopCycle = 0;

        int columns = Mathf.CeilToInt(Mathf.Sqrt(count));
        int rows = Mathf.CeilToInt((float)count / columns);

        // Instantiate prefabs in a grid
        for (int y = 0; y < rows;y++)
        {
            for (int x = 0; x < columns; x++)
            {
                if (count <= 0)
                    return returnValue;

                while (loopCycle == counts[index]) { index++; loopCycle = 0; if (index > counts.Count) { return returnValue; } }

                float xPos = x * gridSpacing;
                float yPos = y * gridSpacing;

                int rotationValue = Mathf.CeilToInt(transform.rotation.eulerAngles.y / 90);
                Vector3 offset = new Vector3(0f,0f,0f);

                if ((rotationValue % 2) == 1)
                {
                    // Vertical Rotation
                    offset = new Vector3(yPos * transform.forward.x, 0f, xPos * -transform.right.z);

                }
                else if ((rotationValue % 2) == 0)
                {
                    // Horizontal Rotation
                    offset = new Vector3(xPos * - transform.right.x, 0f, yPos * transform.forward.z);
                }

                
                GameObject instantiatedUnit = Instantiate(prefabs[index], unitSpawnpoint.position + offset, transform.rotation);
                if (instantiatedUnit.GetComponent<Unit>() != null) { instantiatedUnit.GetComponent<Unit>().SetSpawn(this); }
                //instantiatedUnit.GetComponentInChildren<Canvas>().worldCamera = controller.overheadCamera;
                instantiatedUnits.Add(instantiatedUnit);

                count--;
                loopCycle++;
                
                returnValue++;
            }
        }
        return returnValue;
    }

    private void ClearInstantiatedUnits()
    {
        foreach (GameObject unit in instantiatedUnits)
        {
            Destroy(unit);
        }
        instantiatedUnits.Clear();
    }

    public void RemoveAndDestroyUnit(GameObject unitToRemove, unitEnum unitType)
    {
        if (instantiatedUnits.Contains(unitToRemove))
        {
            instantiatedUnits.Remove(unitToRemove);

            Destroy(unitToRemove);

            switch (unitType)
            {
                case unitEnum.UNIT:
                    unitCount -= 1;
                    controller.unitCount -= 1;
                    InstantiateUnits();
                    instantiatedUnits[0].GetComponent<Commander>().SetSquad(instantiatedUnits);
                    controller.setNewCosts();
                    break;
                case unitEnum.TECHNICIAN:
                    technicianCount -= 1;
                    controller.technicianCount -= 1;
                    InstantiateUnits();
                    instantiatedUnits[0].GetComponent<Commander>().SetSquad(instantiatedUnits);
                    controller.setNewCosts();
                    break;
                case unitEnum.RECON:
                    reconCount -= 1;
                    controller.reconCount -= 1;
                    InstantiateUnits();
                    instantiatedUnits[0].GetComponent<Commander>().SetSquad(instantiatedUnits);
                    controller.setNewCosts();
                    break;
            }
        }
        else
        {
            Debug.LogWarning("GameObject not found in the list.");
        }
    }

    public void RemoveAndDestroySquad()
    {
        ClearInstantiatedUnits();

        controller.commanderCount -= commanderCount;
        controller.unitCount -= unitCount;
        controller.technicianCount -= technicianCount;
        controller.reconCount -= reconCount;

        commanderCount = 0;
        unitCount = 0;
        technicianCount = 0;
        reconCount = 0;

        buttonImage.sprite = inactiveSprite;
        controller.setNewCosts();
        active = false;


        
    }
}



public enum unitEnum
{
    NONE,
    COMMANDER,
    UNIT,
    TECHNICIAN,
    RECON
}

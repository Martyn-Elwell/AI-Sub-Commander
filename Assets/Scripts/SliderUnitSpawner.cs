using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using System.Linq;

public class SliderUnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public GameObject technicianPrefab;
    public GameObject reconPrefab;

    public Slider unitSlider;
    public Slider technicianSlider;
    public Slider reconSlider;

    public int unitCost = 1;
    public int technicianCost = 2;
    public int reconCost = 2;

    public int maxUnitCost = 20; // Maximum total unit cost
    private int totalUnitCost = 0; // Total unit cost

    private int unitCount;
    private int technicianCount;
    private int reconCount;

    [SerializeField] private Text unitCountText;
    [SerializeField] private Text technicianCountText;
    [SerializeField] private Text reconCountText;
    [SerializeField] private Text unitCostText;

    public Transform unitSpawnpoint;
    public int gridSpacing = 2; // Spacing between grid elements

    public List<GameObject> instantiatedUnits = new List<GameObject>();

    
    private void Start()
    {
        unitSlider.onValueChanged.AddListener(OnSliderValueChanged);
        technicianSlider.onValueChanged.AddListener(OnSliderValueChanged);
        reconSlider.onValueChanged.AddListener(OnSliderValueChanged);
        UpdateSliderMaxValues();
        UpdateUnitCostText();
    }

    private void OnDestroy()
    {
        unitSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
        technicianSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
        reconSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
        unitSlider.transform.gameObject.SetActive(false);
        technicianSlider.transform.gameObject.SetActive(false);
        reconSlider.transform.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateSliderMaxValues();
    }

    public void UpdateSliderMaxValues()
    {
        int remainingCost = maxUnitCost - totalUnitCost;

        unitSlider.maxValue = unitCount + Mathf.FloorToInt(remainingCost / unitCost);
        technicianSlider.maxValue = technicianCount + Mathf.FloorToInt(remainingCost / technicianCost);
        reconSlider.maxValue = reconCount + Mathf.FloorToInt(remainingCost / reconCost);
    }

    private void UpdateUnitCostText()
    {
        unitCountText.text = "Operators: " + unitCount;
        technicianCountText.text = "Technicians: " + technicianCount;
        reconCountText.text = "Recon: " + reconCount;
        unitCostText.text = "Total Unit Cost: " + totalUnitCost + " / " + maxUnitCost;
    }

    private void OnSliderValueChanged(float value)
    {
        InstantiateUnits();
    }

    private void InstantiateUnits()
    {
        ClearInstantiatedUnits();
        totalUnitCost = 0;

        int uCost = Mathf.RoundToInt(unitSlider.value) * unitCost;
        int tCost = Mathf.RoundToInt(technicianSlider.value) * technicianCost; // Technician costs 2
        int rCost = Mathf.RoundToInt(reconSlider.value) * reconCost; // Recon costs 2

        int uCount = Mathf.RoundToInt(unitSlider.value);
        int tCount = Mathf.RoundToInt(technicianSlider.value);
        int rCount = Mathf.RoundToInt(reconSlider.value);

        totalUnitCost = uCost + tCost + rCost;

        if (totalUnitCost <= maxUnitCost)
        {
            

            unitCount = InstantiateUnitsOfType(unitPrefab, uCount, Vector3.zero);
            technicianCount = InstantiateUnitsOfType(technicianPrefab, tCount, new Vector3(10f, 0f, 0f));
            reconCount = InstantiateUnitsOfType(reconPrefab, rCount, new Vector3(16f, 0f, 0f));

            UpdateUnitCostText();
        }
        else
        {
            Debug.LogWarning("Total unit cost exceeds the maximum limit.");
        }
    }

    private int InstantiateUnitsOfType(GameObject prefab, int cost, Vector3 offset)
    {
        int count = cost; // In this case, count is equal to cost
        int returnValue = 0;

        int columns = Mathf.CeilToInt(Mathf.Sqrt(count));
        int rows = Mathf.CeilToInt((float)count / columns);

        // Instantiate prefabs in a grid
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                if (count <= 0)
                    return returnValue;

                float xPos = x * gridSpacing + offset.x;
                float yPos = y * gridSpacing + offset.z;

                Vector3 spawnPosition = new Vector3(xPos, 0f, yPos);
                GameObject instantiatedUnit = Instantiate(prefab, unitSpawnpoint.position + spawnPosition, Quaternion.identity);
                instantiatedUnits.Add(instantiatedUnit);

                count--;
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
}

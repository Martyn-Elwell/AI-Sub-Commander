using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public List<GameObject> instantiatedUnits;
    public Slider unitSlider;
    public Transform unitSpawnpoint;
    public int gridSpacing = 2; // Spacing between grid elements

    private void Start()
    {
        unitSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDestroy()
    {
        unitSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        int count = Mathf.RoundToInt(value);
        InstantiatePrefabs(count);
    }

    private void InstantiatePrefabs(int count)
    {
        // Clear previous instantiations
        foreach (GameObject unit in instantiatedUnits)
        {
            Destroy(unit);
        }
        instantiatedUnits = new List<GameObject>();

        // Calculate number of rows and columns based on count
        int columns = Mathf.CeilToInt(Mathf.Sqrt(count));
        int rows = Mathf.CeilToInt((float)count / columns);

        // Instantiate prefabs in a grid
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                if (count <= 0)
                    return;

                float xPos = x * gridSpacing;
                float yPos = y * gridSpacing;

                Vector3 spawnPosition = new Vector3(xPos, 0f, -yPos);
                GameObject instantiatedUnit = Instantiate(unitPrefab);
                instantiatedUnit.transform.localPosition = spawnPosition + unitSpawnpoint.position;
                instantiatedUnits.Add(instantiatedUnit);

                count--;
            }
        }
    }
}

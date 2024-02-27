using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    [SerializeField] private UnitSpawner swatVan;
    [SerializeField] private List<GameObject> squad;
    // Start is called before the first frame update
    void Start()
    {
        swatVan = FindObjectOfType<UnitSpawner>();
        squad = swatVan.instantiatedUnits;
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void scatter()
    {
        swatVan = FindObjectOfType<UnitSpawner>();
        squad = swatVan.instantiatedUnits;
        foreach (GameObject unit in squad)
        {
            float randomX = Random.Range(-10f, 10f);
            float randomZ = Random.Range(-10f, 10f);


            unit.GetComponent<Unit>().SetDestination(new Vector3(randomX, 1, randomZ));
        }
    }

    public void setSquad(List<GameObject> inputList)
    {
        squad = inputList;
    }
}

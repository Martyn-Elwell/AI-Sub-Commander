using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : Unit
{
    [SerializeField] private List<GameObject> squad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void scatter()
    {
        foreach (GameObject unit in squad)
        {
            float randomX = Random.Range(-10f, 10f);
            float randomZ = Random.Range(-10f, 10f);
            if (unit == this.gameObject) { continue; }
            unit.GetComponent<Unit>().SetDestination(new Vector3(randomX, 0f, randomZ));
        }
    }

    public void SetSquad(List<GameObject> inputList)
    {
        squad = inputList;
    }

    public override void DeleteUnit()
    {
        spawnZone.RemoveAndDestroySquad();
    }
}

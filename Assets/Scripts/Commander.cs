using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : Unit
{
    [SerializeField] private List<GameObject> squad;
    [SerializeField] private List<GameObject> units;
    [SerializeField] private List<GameObject> recons;
    [SerializeField] private List<GameObject> technicians;

    private List<GameObject> breaches;
    private List<GameObject> covers;
    private List<GameObject> sabotages;
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
        foreach (GameObject unit in squad)
        {
            if (unit == this.gameObject) { continue; }
            switch (unit.GetComponent<Unit>().type)
            {
                case unitEnum.UNIT:
                    units.Add(unit); break;
                case unitEnum.RECON:
                    recons.Add(unit); break;
                case unitEnum.TECHNICIAN:
                    technicians.Add(unit); break;
            }
        }
    }

    public override void DeleteUnit()
    {
        spawnZone.RemoveAndDestroySquad();
    }
}

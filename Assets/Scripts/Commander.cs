using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Commander : Unit
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> squad;
    [SerializeField] private List<GameObject> units;
    [SerializeField] private List<GameObject> recons;
    [SerializeField] private List<GameObject> technicians;


    [SerializeField] private List<GameObject> breaches;
    [SerializeField] private List<GameObject> covers;
    [SerializeField] private List<GameObject> sabotages;

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

    public void SetSquad(List<GameObject> inputList, GameObject _player)
    {
        player = _player;
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

    private void AssignSquad()
    {
        Debug.Log("Reassinging Squad");
        List<GameObject> _units = new List<GameObject>(units); List<GameObject> _recons = new List<GameObject>(recons); List<GameObject> _technicians = new List<GameObject>(technicians);
        List<GameObject> _breaches = new List<GameObject>(breaches); List<GameObject> _covers = new List<GameObject>(covers); List<GameObject> _sabotages = new List<GameObject>(sabotages);
        List<GameObject> tasks = new List<GameObject>(_breaches); tasks.AddRange(_covers); tasks.AddRange(_sabotages);

        foreach (GameObject unit in squad)
        {
            if (unit != this.gameObject)
            {
                unit.GetComponent<Unit>().ClearTask();
            }
        }

        foreach (GameObject task in tasks)
        {
            task.GetComponent<Interactable>().ClearUnits();
        }

        // Assign specialised tasks first
        // Assign sabotages first
        if (_technicians.Any() && _sabotages.Any())
        {
            foreach (GameObject sabotage in _sabotages)
            {
                if (_technicians.Count == 0) break;

                AssignTaskToSoldier(_technicians[0], sabotage, sabotage.GetComponent<Interactable>().standingPoints[0].position, InteractionType.SABOTAGE);
                _technicians.RemoveAt(0);
            }
        }
        else { /*Debug.Log("No Technicians to Sabotage ");*/ }


        // Assign covers next
        if (_recons.Any() && _covers.Any())
        {
            foreach (GameObject cover in _covers)
            {
                if (_covers.Count == 0) break;

                AssignTaskToSoldier(_recons[0], _covers[0], cover.GetComponent<Interactable>().standingPoints[0].position, InteractionType.COVER);
                _recons.RemoveAt(0);
            }
        }
        else { /*Debug.Log("No Recons to Cover ");*/ }

        // Assign breaches last
        // Assigns remaining technician to each breach

        if (_breaches.Any())
        {
            int totalBreachUnitCount = _units.Count + technicians.Count;
            if (totalBreachUnitCount >= _breaches.Count * 2) { /*Debug.Log("Enough breach units to breach all objects");*/  }
            foreach (GameObject breach in _breaches)
            {
                if (totalBreachUnitCount < 2)
                {
                    Debug.Log("Not enough units to create new breaches");
                    // If there is an odd number assign final unit to intial breach
                    if (_technicians.Count == 1)
                    {
                        AssignTaskToSoldier(_technicians[0], breaches[0], breach.GetComponent<Interactable>().standingPoints[2].position, InteractionType.BREACH);
                        _technicians.RemoveAt(0);
                        totalBreachUnitCount = _units.Count + technicians.Count;
                    }
                    // If there is an odd number assign final unit to intial breach
                    else if (_units.Count == 1)
                    {
                        AssignTaskToSoldier(_units[0], breaches[0], breach.GetComponent<Interactable>().standingPoints[2].position, InteractionType.BREACH);
                        _units.RemoveAt(0);
                        totalBreachUnitCount = _units.Count + technicians.Count;
                    }
                    break;
                }

                // Has two units for a breach
                // Has a technician to lead breach
                if (_technicians.Count >= 1 && _units.Count >= 1)
                {
                    AssignTaskToSoldier(_technicians[0], breach, breach.GetComponent<Interactable>().standingPoints[0].position, InteractionType.BREACH);
                    _technicians.RemoveAt(0);
                    AssignTaskToSoldier(_units[0], breach, breach.GetComponent<Interactable>().standingPoints[1].position, InteractionType.BREACH);
                    _units.RemoveAt(0);
                    totalBreachUnitCount = _units.Count + technicians.Count;
                }
                // Has two base units to breach
                else if (_units.Count >= 2)
                {
                    AssignTaskToSoldier(_units[0], breach, breach.GetComponent<Interactable>().standingPoints[0].position,InteractionType.BREACH);
                    _units.RemoveAt(0);
                    AssignTaskToSoldier(_units[0], breach, breach.GetComponent<Interactable>().standingPoints[1].position, InteractionType.BREACH);
                    _units.RemoveAt(0);
                    totalBreachUnitCount = _units.Count + technicians.Count;
                }
                
                
            }
            totalBreachUnitCount = _units.Count + technicians.Count;
            // When all Breaches have at least 2 units/technicians assigned to them assign the rest spread across
            int cycleCount = 0;
            List<GameObject> RemainingUnits = new List<GameObject>(_technicians);
            RemainingUnits.AddRange(_units);
            foreach (GameObject unit in RemainingUnits)
            {
                AssignTaskToSoldier(unit, breaches[cycleCount % breaches.Count], InteractionType.BREACH);

            }
        }
        else { /*Debug.Log("No Units to Breach");*/ }
    }

    private void AssignTaskToSoldier(GameObject soldier, GameObject task, InteractionType type)
    {
        soldier.GetComponent<Unit>().AssignTask(task, type);
        task.GetComponent<Interactable>().AssignUnitToObject(soldier, type);
        soldier.GetComponent<Unit>().SetDestination(task.transform.position);
    }

    private void AssignTaskToSoldier(GameObject soldier, GameObject task, Vector3 position, InteractionType type)
    {
        soldier.GetComponent<Unit>().AssignTask(task, type);
        task.GetComponent<Interactable>().AssignUnitToObject(soldier, type);
        soldier.GetComponent<Unit>().SetDestination(position);
    }

    public void AllUnitsBreach()
    {
        foreach (GameObject unit in squad)
        {
            unit.GetComponent<Unit>().active = true;
        }

        foreach (GameObject breach in breaches)
        {
            for (int i = 0; i < breach.GetComponent<Interactable>().assignedUnits.Count(); i++)
            {
                Vector3 destination = breach.GetComponent<Interactable>().navigationPoints[i].transform.position;
                breach.GetComponent<Interactable>().assignedUnits[i].GetComponent<Unit>().SetDestination(destination);
            }

        }
    }

    public void AddObjectToBreach()
    {
        GameObject objectToAdd = player.GetComponent<PlayerController>().GetCurrentInteractable();
        if (objectToAdd != null)
        {
            if (!breaches.Contains(objectToAdd))
            {
                breaches.Add(objectToAdd);
            }

            if (covers.Contains(objectToAdd))
            {
                covers.Remove(objectToAdd);
            }

            if (sabotages.Contains(objectToAdd))
            {
                sabotages.Remove(objectToAdd);
            }
        }
        AssignSquad();
    }
    public void AddObjectToCover()
    {
        GameObject objectToAdd = player.GetComponent<PlayerController>().GetCurrentInteractable();
        if (objectToAdd != null)
        {
            if (breaches.Contains(objectToAdd))
            {
                breaches.Remove(objectToAdd);
            }

            if (!covers.Contains(objectToAdd))
            {
                covers.Add(objectToAdd);
            }

            if (sabotages.Contains(objectToAdd))
            {
                sabotages.Remove(objectToAdd);
            }
        }
        AssignSquad();
    }
    public void AddObjectToSabotage()
    {
        GameObject objectToAdd = player.GetComponent<PlayerController>().GetCurrentInteractable();
        if (objectToAdd != null)
        {
            if (breaches.Contains(objectToAdd))
            {
                breaches.Remove(objectToAdd);
            }

            if (covers.Contains(objectToAdd))
            {
                covers.Remove(objectToAdd);
            }

            if (!sabotages.Contains(objectToAdd))
            {
                sabotages.Add(objectToAdd);
            }
        }
        AssignSquad();
    }
}

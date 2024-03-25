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
        List<GameObject> _units = new List<GameObject>(units); List<GameObject> _recons = new List<GameObject>(recons); List<GameObject> _technicians = new List<GameObject>(technicians);
        List<GameObject> _breaches = new List<GameObject>(breaches); List<GameObject> _covers = new List<GameObject>(covers); List<GameObject> _sabotages = new List<GameObject>(sabotages);
        List<GameObject> unitsToRemove = new List<GameObject>();

        // Assign sabotages first
        if (_technicians.Any() && _sabotages.Any())
        {
            foreach (GameObject technician in _technicians)
            {
                if (_sabotages.Count == 0) break;

                AssignTaskToSoldier(technician, _sabotages[0], InteractionType.SABOTAGE);
                _sabotages.RemoveAt(0);
                unitsToRemove.Add(technician);
            }
        }
        else { Debug.Log("No Technicians to Sabotage "); }
        _technicians.Except(unitsToRemove).ToList();
        unitsToRemove.Clear();


        // Assign breaches next
        // Assigns remaining technician to each breach

        if (_technicians.Any() && _breaches.Any())
        {
            int cycleCount = 0;
            foreach (GameObject technician in _technicians)
            {
                if (_breaches.Count == 0) break;

                AssignTaskToSoldier(technician, breaches[cycleCount % breaches.Count], InteractionType.BREACH);

            }
            technicians.Clear();
        }

        // Assign 2 units to each breach
        if (_units.Any() && _breaches.Any())
        {
            int breachSoldierCount = 0;
            foreach (GameObject unit in _units)
            {
                if (breachSoldierCount >= 2 || _breaches.Count == 0)
                {
                    breachSoldierCount = 0;
                    _breaches.RemoveAt(0);
                    break;
                }

                AssignTaskToSoldier(unit, _breaches[0], InteractionType.BREACH);
                unitsToRemove.Add(unit);
                breachSoldierCount++;
            }
        }
        else { Debug.Log("No Units to Breach"); }
        _units.Except(unitsToRemove).ToList();
        unitsToRemove.Clear();


        // Assign covers last
        if (_recons.Any() && _covers.Any())
        {
            foreach (GameObject recon in _recons)
            {
                if (_covers.Count == 0) break;

                AssignTaskToSoldier(recon, _covers[0], InteractionType.COVER);
                _covers.RemoveAt(0);
                unitsToRemove.Add(recon);
            }
        }
        else { Debug.Log("No Recons to Cover "); }
        _recons.Except(unitsToRemove).ToList();
        unitsToRemove.Clear();


        // Use Units to cover as Backup
        if (_covers.Any() && _covers.Any())
        {
            foreach (GameObject unit in _units)
            {
                if (_covers.Count == 0) break;

                AssignTaskToSoldier(unit, _covers[0], InteractionType.COVER);
                _covers.RemoveAt(0);
                unitsToRemove.Add(unit);
            }
        }
        else { Debug.Log("No Unit to Cover "); }
        _units.Except(unitsToRemove).ToList();
        unitsToRemove.Clear();


        // Assign remaining Units to breach
        if (_units.Any() && _breaches.Any())
        {
            int cycleCount = 0;
            foreach (GameObject unit in _units)
            {
                if (_breaches.Count == 0) break;

                AssignTaskToSoldier(unit, breaches[cycleCount % breaches.Count], InteractionType.BREACH);
            }
        }
        _units.Clear();

        if (_recons.Any() && _breaches.Any())
        {
            int cycleCount = 0;
            foreach (GameObject recon in _recons)
            {
                if (_breaches.Count == 0) break;

                AssignTaskToSoldier(recon, breaches[cycleCount % breaches.Count], InteractionType.BREACH);
            }
        }
        _recons.Clear();

    }

    private void AssignTaskToSoldier(GameObject soldier, GameObject task, InteractionType type)
    {
        soldier.GetComponent<Unit>().assignedTask = task;
        soldier.GetComponent<Unit>().taskType = type;
        task.GetComponent<Interactable>().AssignUnitToObject(soldier, type);
        //soldier.GetComponent<Unit>().SetDestination(task.transform.position);
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
        }
        AssignSquad();
    }
    public void AddObjectToCover()
    {
        GameObject objectToAdd = player.GetComponent<PlayerController>().GetCurrentInteractable();
        if (objectToAdd != null)
        {
            if (!covers.Contains(objectToAdd))
            {
                covers.Add(objectToAdd);
            }
        }
        AssignSquad();
    }
    public void AddObjectToSabotage()
    {
        GameObject objectToAdd = player.GetComponent<PlayerController>().GetCurrentInteractable();
        if (objectToAdd != null)
        {
            if (!covers.Contains(objectToAdd))
            {
                sabotages.Add(objectToAdd);
            }
        }
        AssignSquad();
    }
}

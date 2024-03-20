using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UnitSpawner redZone;
    [SerializeField] private Commander redCommander;
    [SerializeField] private UnitSpawner yellowZone;
    [SerializeField] private Commander yellowCommander;
    [SerializeField] private UnitSpawner greenZone;
    [SerializeField] private Commander greenCommander;

    private void Awake()
    {
        redCommander = redZone.instantiatedUnits.First().GetComponent<Commander>();
        yellowCommander = yellowZone.instantiatedUnits.First().GetComponent<Commander>();
        greenCommander = greenZone.instantiatedUnits.First().GetComponent<Commander>();
    }
}

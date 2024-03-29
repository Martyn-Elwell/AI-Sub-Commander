using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectedRooms;
    [SerializeField] private List<GameObject> breaches;
    [SerializeField] private List<GameObject> covers;
    [SerializeField] private List<GameObject> sabotages;
    [SerializeField] private List<GameObject> enemies;

    private void Start()
    {
        
    }
}

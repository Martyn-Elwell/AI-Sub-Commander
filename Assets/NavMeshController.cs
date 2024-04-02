using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshController : MonoBehaviour
{
    public NavMeshSurface surface;
    public List<GameObject> volumes;
    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
    }

    public void ClearObstacles()
    {
        foreach (GameObject volume in volumes)
        {
            volume.SetActive(false);
        }

        surface.BuildNavMesh();
    }
}

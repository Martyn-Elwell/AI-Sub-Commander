using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : Interactable
{
    public GameObject sparksPrefab;

    public void Sabotage()
    {
        GameObject sparks = Instantiate(sparksPrefab, transform.position, Quaternion.identity);
        Destroy(sparks, 5f );
    }
}

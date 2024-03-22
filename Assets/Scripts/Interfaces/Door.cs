using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private LayerMask originalLayer;

    public void Start()
    {
        originalLayer = transform.gameObject.layer;
    }
    public void Interact()
    {
        Debug.Log("Interacted");
    }

    public void Outline(bool active)
    {
        if (active) { transform.gameObject.layer = LayerMask.NameToLayer("Outline");  }
        else { transform.gameObject.layer = originalLayer; }
    }
}

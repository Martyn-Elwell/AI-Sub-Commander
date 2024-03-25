using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    private LayerMask originalLayer;
    [SerializeField] private List<GameObject> modelsToOutline;
    [SerializeField] private List<GameObject> unitsInteracting;
    [SerializeField] private InteractionType interactionType;

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
        if (active)
        {
            foreach (GameObject model in modelsToOutline)
            {
                model.layer = LayerMask.NameToLayer("Outline");
            }
        }
        else
        {
            foreach (GameObject model in modelsToOutline)
            {
                model.layer = originalLayer;
            }
        }
    }

    public void AssignUnitToObject(GameObject unit, InteractionType type)
    {
        unitsInteracting.Add(unit);
        interactionType = type;
    }
}

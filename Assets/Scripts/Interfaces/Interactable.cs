using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    private LayerMask originalLayer;
    private GameObject room;
    [SerializeField] private List<GameObject> modelsToOutline;
    [SerializeField] public List<GameObject> assignedUnits;
    [SerializeField] public List<GameObject> navigationPoints;
    [SerializeField] public List<Transform> standingPoints;
    [SerializeField] public InteractionType primaryInteraction;
    [SerializeField] public InteractionType secondaryInteraction;
    [SerializeField] private InteractionType currentInteraction;

    public void Start()
    {
        originalLayer = transform.gameObject.layer;
        room = transform.parent.parent.gameObject;
    }
    public void Interact()
    {
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
        assignedUnits.Add(unit);
        currentInteraction = type;
    }
    public void ClearUnits()
    {
        assignedUnits.Clear();
        currentInteraction = InteractionType.NONE;
    }
}

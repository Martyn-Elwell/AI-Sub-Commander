using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Transform cameraTransform;

    [Header("Commander References")]
    [SerializeField] private UnitSpawner redZone;
    [SerializeField] public Commander redCommander;
    [SerializeField] private UnitSpawner yellowZone;
    [SerializeField] public Commander yellowCommander;
    [SerializeField] private UnitSpawner greenZone;
    [SerializeField] public Commander greenCommander;

    [Header("UI References")]
    [SerializeField] private GameObject ringMenu;
    [SerializeField] private GameObject interactText;
    [SerializeField] private float interactDistance = 5f;

    public GameObject currentInteractable = null;
    [HideInInspector] public InteractionType currentInteractionType;

    private void Awake()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;

        if (redZone.instantiatedUnits.Count > 0) { redCommander = redZone.instantiatedUnits.First().GetComponent<Commander>(); }
        if (yellowZone.instantiatedUnits.Count > 0) { yellowCommander = yellowZone.instantiatedUnits.First().GetComponent<Commander>(); }
        if (greenZone.instantiatedUnits.Count > 0) { greenCommander = greenZone.instantiatedUnits.First().GetComponent<Commander>(); }

        ringMenu.GetComponent<RingMenu>().SetButtonReferences();
        LockCursor(true);
    }

    void Update()
    {
        UpdateContextRaycast();
        UpdateInputs();
    }

    private void UpdateInputs()
    {
        // Left Mouse Button
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentInteractionType = InteractionType.BREACH;
            Debug.Log(currentInteractionType);
            InteractWithObject();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractionType = InteractionType.COVER;
            Debug.Log(currentInteractionType);
            InteractWithObject();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentInteractionType = InteractionType.SABOTAGE;
            Debug.Log(currentInteractionType);
            InteractWithObject();
        }
    }

    private void UpdateContextRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            IInteractable interactable = hitObject.GetComponent<IInteractable>();
            // Ray hit interactable
            if (interactable != null)
            {
                interactText.SetActive(true);
                interactable.Outline(true);
                currentInteractable = hitObject;
            }
            // Ray hit non interactable
            else
            {
                interactText.SetActive(false);
                if (currentInteractable != null) { currentInteractable.GetComponent<IInteractable>().Outline(false); }
            }
        }
        // Ray did not hit anything
        else
        {
            if (currentInteractable != null) { currentInteractable.GetComponent<IInteractable>().Outline(false); }
        }

        // Player walked away from interactable
        if (currentInteractable != null)
        {
            if (Vector3.Distance(transform.position, currentInteractable.transform.position) > interactDistance)
            {
                interactText.SetActive(false);
                currentInteractable.GetComponent<IInteractable>().Outline(false);
                ringMenu.SetActive(false);
                LockCursor(true);
            }
        }
    }



    private void InteractWithObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            IInteractable interactable = hitObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                ringMenu.SetActive(!ringMenu.activeSelf);
                ringMenu.GetComponent<RingMenu>().SetInteractableObject(interactable);
                LockCursor(Cursor.visible);
            }
            else
            {
                LockCursor(true);
                ringMenu.SetActive(false);

            }
        }
        else
        {
            LockCursor(true);
            ringMenu.SetActive(false);

        }
    }



    private void LockCursor(bool lockState)
    {
        if (lockState)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public GameObject GetCurrentInteractable()
    {
        if (currentInteractable!= null) { return currentInteractable; }
        else { return null; }
    }
}

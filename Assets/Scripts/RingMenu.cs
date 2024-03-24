using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMenu : MonoBehaviour
{
    public Vector2 normalisedMousePosition;
    public float currentAngle;
    public int selection;
    private int previousSelection;

    [SerializeField] private float deadzone = 25f;

    public List<GameObject> menuItems;

    private MenuItem menuItemSc;
    private MenuItem previousMenuItemSc;

    private IInteractable currentInteractable;
    private
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMenu();

        UpdateInputs();
    }

    public void ShowMenu()
    {

    }

    private void UpdateMenu()
    {
        normalisedMousePosition = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);

        if (normalisedMousePosition.magnitude <= deadzone) { return; }
        currentAngle = Mathf.Atan2(normalisedMousePosition.y, normalisedMousePosition.x) * Mathf.Rad2Deg;

        currentAngle = (currentAngle + 360) % 360;

        selection = (int)currentAngle / (360 / menuItems.Count);

        if (selection != previousSelection)
        {
            previousMenuItemSc = menuItems[previousSelection].GetComponent<MenuItem>();
            previousMenuItemSc.Deselect();
            previousSelection = selection;
            if (menuItems[selection].GetComponent<MA_OrderCommander>() != null)
            { 
                if (menuItems[selection].GetComponent<MA_OrderCommander>().getCommander())
                {
                    menuItemSc = menuItems[selection].GetComponent<MenuItem>();
                    menuItemSc.Select();
                }
            }
            else
            {
                menuItemSc = menuItems[selection].GetComponent<MenuItem>();
                menuItemSc.Select();
            }
        }
    }
    private void UpdateInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            menuItems[selection].GetComponent<MenuItem>().Click();
        }

    }

    public void SetInteractableObject(IInteractable interactable)
    {
        currentInteractable = interactable;
    }

    public void SetButtonReferences()
    {
        foreach (GameObject button in menuItems)
        {
            if (button.GetComponent<MA_OrderCommander>()  != null)
            {
                button.GetComponent<MA_OrderCommander>().setCommander();
            }
            
        }
    }
}

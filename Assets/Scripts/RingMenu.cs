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

            menuItemSc = menuItems[selection].GetComponent<MenuItem>();
            menuItemSc.Select();
        }
    }
    private void UpdateInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(selection);
        }

    }
}

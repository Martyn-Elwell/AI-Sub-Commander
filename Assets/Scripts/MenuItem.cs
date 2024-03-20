using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    [SerializeField] private Color hoverColour;
    [SerializeField] private Color baseColour;
    [SerializeField] private Image background;
    [SerializeField] private GameObject accent;

    [SerializeField] public IMenuAction action;
    // Start is called before the first frame update
    void Start()
    {
        background.color = baseColour;
        action = GetComponent<IMenuAction>();
    }

    public void Select()
    {
        background.color = hoverColour;
        accent.SetActive(true);
    }

    public void Deselect()
    {
        background.color = baseColour;
        accent.SetActive(false);
    }

    public void Click()
    {
        if (action != null)
        {
            action.Activate();
        }
    }

    public void Click(IInteractable interction)
    {
        if (action != null)
        {
            action.Activate();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    [SerializeField] private Color hoverColour;
    [SerializeField] private Color baseColour;
    [SerializeField] private Image background;
    // Start is called before the first frame update
    void Start()
    {
        background.color = baseColour;
    }

    public void Select()
    {
        background.color = hoverColour;
    }

    public void Deselect()
    {
        background.color = baseColour;
    }


}

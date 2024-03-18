using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA_Debug : MonoBehaviour, IMenuAction
{
    public void Activate()
    {
        Debug.Log("Clicked");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA_Breach : MonoBehaviour, IMenuAction
{
    [SerializeField] private PlayerController player;
    public void Activate()
    {
        if (player != null)
        {
            player.OrderBreach();
        }
    }
}

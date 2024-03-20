using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA_OrderCommander : MonoBehaviour, IMenuAction
{
    [SerializeField] Commander commander;
    public void Activate()
    {
        commander.scatter();
    }
}

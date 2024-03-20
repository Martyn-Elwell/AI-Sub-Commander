using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA_OrderCommander : MonoBehaviour, IMenuAction
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Commander commander;
    [SerializeField] private squad colour;

    public void Activate()
    {
        
        commander.scatter();
    }

    public void setCommander()
    {
        switch (colour)
        {
            case squad.RED:
                commander = player.redCommander;
                break;
            case squad.YELLOW:
                commander = player.yellowCommander;
                break;
            case squad.GREEN:
                commander = player.greenCommander;
                break;
        }
    }
}

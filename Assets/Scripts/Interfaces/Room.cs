using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    private GameObject doorParent;
    private GameObject windowParent;
    private GameObject modifierParent;

    [SerializeField] private List<GameObject> connectedRooms;
    [SerializeField] private List<Door> doors;
    [SerializeField] private List<Window> windows;
    [SerializeField] private List<PowerBox> modifiers;

    [SerializeField] private List<GameObject> breaches;
    [SerializeField] private List<GameObject> covers;
    [SerializeField] private List<GameObject> sabotages;
    [SerializeField] private List<GameObject> enemies;

    private void Start()
    {
        doorParent = FindChildByName(gameObject, "Doors");
        windowParent = FindChildByName(gameObject, "Windows");
        modifierParent = FindChildByName(gameObject, "Modifiers");

        Door[] temp = doorParent.GetComponentsInChildren<Door>();
        doors = temp.ToList();
        Window[] temp2 = windowParent.GetComponentsInChildren<Window>();
        windows = temp2.ToList();
        PowerBox[] temp3 = modifierParent.GetComponentsInChildren<PowerBox>();
        modifiers = temp3.ToList();
    }

    private GameObject FindChildByName(GameObject parentObject, string objectName)
    {
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == objectName)
            {
                return gameObject.transform.GetChild(i).gameObject;
            }

            if (gameObject.transform.childCount > 0)
            {
                GameObject temp = FindChildByName(parentObject.transform.GetChild(i).gameObject, objectName);
                if (temp != null)
                {
                    return temp;
                }
            }
           
        }

        return null;
    }

    public void AssignEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
}


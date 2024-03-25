using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public List<GameObject> setToActive;
    public List<GameObject> setToInactive;
    // Start is called before the first frame update
    public void Click()
    {
        foreach (GameObject obj in setToActive)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in setToInactive)
        {
            obj.SetActive(false);
        }
    }
}

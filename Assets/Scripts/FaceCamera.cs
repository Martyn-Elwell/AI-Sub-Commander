using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Vector3 vector;
    private Quaternion rotation =  Quaternion.Euler(90f, 0f, 0f);
    
    void Update()
    {
        transform.rotation = rotation;
    }
}

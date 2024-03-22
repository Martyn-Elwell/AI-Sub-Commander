using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSync : MonoBehaviour
{
    private Camera syncCamera;
    [SerializeField] private Camera cameraToSync;

    private void Awake()
    {
        syncCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (cameraToSync != null)
        {
            syncCamera.fieldOfView = cameraToSync.fieldOfView;
            syncCamera.aspect = cameraToSync.aspect;
            syncCamera.transform.position = cameraToSync.transform.position;
            syncCamera.transform.rotation = cameraToSync.transform.rotation;

        }
    }
}

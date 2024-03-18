using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 100f;
    public float jumpForce = 1f;
    public float gravity = -9.81f;
    public float interactDistance = 5f;

    private CharacterController characterController;
    private Transform cameraTransform;
    [SerializeField] private GameObject ringMenu;

    private float verticalRotation = 0f;
    private Vector3 playerVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;

        LockCursor();
    }

    void Update()
    {
        UpdateInputs();

        UpdateMovement();



    }

    private void UpdateMovement()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        characterController.Move(movementDirection * movementSpeed * Time.deltaTime);

        // Gravity
        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Jumping
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void UpdateInputs()
    {
        // Left Mouse Button
        if (Input.GetMouseButtonDown(0))
        {
            InteractWithObject();
        }
    }

    private void InteractWithObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            IInteractable interactable = hitObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                ringMenu.SetActive(true);

                UnLockCursor();
            }
            else
            {
                ringMenu.SetActive(false);
                LockCursor();
            }
        }
        else
        {
            ringMenu.SetActive(false);
            LockCursor();
        }
    }

    private void LockCursor() { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }

    private void UnLockCursor() { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; }
}

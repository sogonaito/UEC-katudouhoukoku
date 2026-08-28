using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 7f;
    [SerializeField] private float height = 1.5f;
    [SerializeField] private float mouseSensitivity = 0.15f;
    [SerializeField] private float minVerticalAngle = -25f;
    [SerializeField] private float maxVerticalAngle = 65f;

    private float horizontalAngle;
    private float verticalAngle = 15f;

    private void Start()
    {
        Vector3 startRotation = transform.eulerAngles;

        horizontalAngle = startRotation.y;
        verticalAngle = startRotation.x;

        LockCursor();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Mouse mouse = Mouse.current;

        if (mouse == null)
        {
            return;
        }

        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UnlockCursor();
        }

        if (mouse.leftButton.wasPressedThisFrame)
        {
            LockCursor();
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Vector2 mouseMove = mouse.delta.ReadValue();

            horizontalAngle += mouseMove.x * mouseSensitivity;
            verticalAngle -= mouseMove.y * mouseSensitivity;

            verticalAngle = Mathf.Clamp(
                verticalAngle,
                minVerticalAngle,
                maxVerticalAngle
            );
        }

        Quaternion cameraRotation =
            Quaternion.Euler(verticalAngle, horizontalAngle, 0f);

        Vector3 lookPosition =
            target.position + Vector3.up * height;

        transform.position =
            lookPosition - cameraRotation * Vector3.forward * distance;

        transform.rotation = cameraRotation;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

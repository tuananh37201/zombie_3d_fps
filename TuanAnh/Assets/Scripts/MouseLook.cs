using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        /* ======= Rotate playerBody along the vertical axis and Rotate Camera along the horizontal axis ======= */
        // Invert mouse
        xRotation -= mouseY;
        // Lock rotate -90 < Mouse X < 90
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate parent
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Rotate playberBody along the vertical axis
        playerBody.Rotate(Vector3.up * mouseX); // up = (0, 1, 0)

    }
}
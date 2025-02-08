using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera
    public float horRotationSpeed = 100f;
    public float verRotationSpeed = 100f;
    public float zoomSpeed = 5f;
    public float minZoom = 3.5f;   // Minimum distance from the pivot
    public float maxZoom = 20f;  // Maximum distance from the pivot
    public float minVerticalAngle = -45f;
    public float maxVerticalAngle = 45f;

    private float verticalRotation = 0f;
    private float zoomDistance;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Auto-assign the main camera if not set
        }

        // Set initial zoom distance
        zoomDistance = Vector3.Distance(cameraTransform.position, transform.position);
    }

    void Update()
    {
        // Get input for rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate horizontally around Y-axis
        transform.Rotate(Vector3.down * horRotationSpeed * horizontalInput * Time.deltaTime, Space.World);

        // Adjust vertical rotation
        verticalRotation -= verticalInput * verRotationSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        // Apply the vertical rotation
        transform.localRotation = Quaternion.Euler(verticalRotation, transform.localRotation.eulerAngles.y, 0f);

        // Zooming with Mouse Scroll
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        zoomDistance -= scrollInput * zoomSpeed;
        zoomDistance = Mathf.Clamp(zoomDistance, minZoom, maxZoom);

        // Move the camera closer or farther while keeping focus on the pivot
        cameraTransform.position = transform.position - cameraTransform.forward * zoomDistance;
    }
}
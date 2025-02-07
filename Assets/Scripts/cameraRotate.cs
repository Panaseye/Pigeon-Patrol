using UnityEngine;

public class cameraRotate : MonoBehaviour
{
    public float horRotationSpeed;
    public float verRotationSpeed;
    public float minVerticalAngle = -45f; 
    public float maxVerticalAngle = 45f;  

    private float verticalRotation = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 currentRotation = transform.localRotation.eulerAngles;

        // Rotate horizontally around Y-axis
        transform.Rotate(Vector3.down * horRotationSpeed * horizontalInput * Time.deltaTime, Space.World);

        // Adjust vertical rotation value
        verticalRotation -= verticalInput * verRotationSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle); // Clamp vertical rotation

        // Apply the clamped rotation (preserve Y rotation but limit X rotation)
        transform.localRotation = Quaternion.Euler(verticalRotation, transform.localRotation.eulerAngles.y, 0f);
    }
}

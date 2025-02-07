using UnityEngine;

public class cameraRotate : MonoBehaviour
{
    public float horRotationSpeed;
    public float verRotationSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.down * horRotationSpeed * horizontalInput * Time.deltaTime);
        transform.Rotate(Vector3.right * verRotationSpeed * verticalInput * Time.deltaTime);
    
    }
}

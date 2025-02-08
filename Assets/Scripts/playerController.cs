using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public house house;

    public string balloonTag = "balloon";
    public string pigeonTag = "enemy";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click or touch
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Convert screen position to a ray
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Cast the ray
            {
                if (hit.collider.CompareTag(balloonTag)) // Check if the object has the target tag
                {
                    Destroy(hit.collider.gameObject);
                    house.BalloonPoped();
                }
                else if (hit.collider.CompareTag(pigeonTag))
                {
                    Debug.Log("pigeonTag touched");
                    //send damage to pigeon if clicked
                    hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(10);
                    house.PigeonDead();
                }
            }
        }
    }
}

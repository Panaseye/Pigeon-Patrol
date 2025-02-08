using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;

public class house : MonoBehaviour
{
    public gameManager gameManager;

    public float buoyancy;
    public float speed;
    public float maxHeight;
    public float minHeight;
    public PigeonCounter pigeonCounter;
    public int pigeonCount;

    public string balloonTag = "balloon"; 
    public string pigeonTag = "enemy";
    public float balloonBuoyancy = 2f;
    public float pigeonBuoyancy = -1f; 
    public float balloonAll;
    public float pigeonAll;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        int balloonCount = CountAllChildrenWithTag(balloonTag, transform);
        Debug.Log("Number of children with tag '" + balloonTag + "': " + balloonCount);
    }

    // Update is called once per frame
    void Update()
    {
       pigeonCounter.CountStationaryPigeons(OnPigeonCountReady);
        UpdateBalloonCount();
    buoyancy = pigeonAll + balloonAll;

        Debug.Log("Balloon boyancy " + balloonAll);
        Debug.Log("Pigeon boyancy " + pigeonAll);

        if (gameObject.transform.position.y <= maxHeight )
{
            // // Calculate vertical movement based on buoyancy
            // //float movementY =gameObject.transform.position.y + -(buoyancy * Time.deltaTime * speed);
            // float movementY = buoyancy * Time.deltaTime * speed;
            // float totalMovementY =  transform.position.y+movementY;
            
            // if(movementY>=0){totalMovementY = Mathf.Clamp(transform.position.y+movementY, transform.position.y , maxHeight) - transform.position.y ;}
            
            //  Debug.Log("Total Buoyancy movement " + totalMovementY);
    
            // // Adjust vertical movement to not go below the maxHeight
            // // if (gameObject.transform.position.y + movementY > maxHeight)
            // // {
            // //     movementY = maxHeight - gameObject.transform.position.y;
            // // }

            // // // Apply the movement
            // gameObject.transform.Translate(
            // gameObject.transform.position.x,
            // totalMovementY, 
            // gameObject.transform.position.z);

            float movementY = buoyancy * Time.deltaTime * speed;
            float totalMovementY;
            if(movementY>=0)
            {
                totalMovementY = Mathf.Clamp(transform.position.y+movementY, transform.position.y , maxHeight) - transform.position.y;
            }
            else
            {
                totalMovementY = Mathf.Clamp(transform.position.y+movementY, minHeight , transform.position.y) - transform.position.y;
            }
            // Debug.Log("Total Buoyancy movement " + totalMovementY);

    // Apply the movement
    gameObject.transform.Translate(
        gameObject.transform.position.x,
             totalMovementY, 
        gameObject.transform.position.z);


}
    }    

    public void BalloonPoped()
{
    Invoke(nameof(UpdateBalloonCount), 0.05f); // Small delay
}

private void UpdateBalloonCount()
{
    int balloonCount = CountAllChildrenWithTag(balloonTag, transform);
       // Debug.Log("Balloon count: " + balloonCount);
    balloonAll = balloonCount * balloonBuoyancy;
       // Debug.Log(balloonAll + " buoy " + balloonBuoyancy + " count " + balloonCount);
}

public void PigeonDead()
{
    StartCoroutine(DelayedPigeonCount());
    gameManager.feathers ++;
}

private IEnumerator DelayedPigeonCount()
{
    yield return new WaitForEndOfFrame(); // Ensures Unity fully removes the object
    
    yield return null; 

    pigeonCounter.CountStationaryPigeons(OnPigeonCountReady);
}

void OnPigeonCountReady(int count)
{
    pigeonCount = count;
        //Debug.Log("Stationary Pigeons: " + count);
    pigeonAll = pigeonCount * pigeonBuoyancy;
       // Debug.Log(pigeonAll + " buoy " + pigeonBuoyancy + " count " + pigeonCount);
}

  
    public int CountAllChildrenWithTag(string targetTag, Transform parent)
    {
        int count = 0;

        // Loop through all children of this GameObject
        foreach (Transform child in parent)
        {
            if (child.CompareTag(targetTag)) // Check if the child has the desired tag
            {
                count++;
            }

            count += CountAllChildrenWithTag(targetTag, child);
        }

        return count;
    }

    
    
        
        
    








    
}

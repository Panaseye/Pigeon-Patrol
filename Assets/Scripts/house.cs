using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;

public class house : MonoBehaviour
{

    public float buoyancy;
    public float speed;
    public float maxHeight;
    public PigeonCounter pigeonCounter;
    public int pigeonCount;

    public string balloonTag = "balloon"; 
    public string pigeonTag = "enemy";
    public float balloonBuoyancy = 5f;
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
buoyancy = pigeonAll + balloonAll;

if (gameObject.transform.position.y <= maxHeight)
{
    // Calculate vertical movement based on buoyancy
    float movementY =gameObject.transform.position.y + -(buoyancy * Time.deltaTime * speed);
    Debug.Log(movementY);
    
    // Adjust vertical movement to not go below the maxHeight
    if (gameObject.transform.position.y + movementY > maxHeight)
    {
        movementY = maxHeight - gameObject.transform.position.y;
    }

    // Apply the movement
    gameObject.transform.Translate(
        gameObject.transform.position.x,
        movementY, 
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
    balloonAll = balloonCount * balloonBuoyancy;
    Debug.Log(balloonAll + " buoy " + balloonBuoyancy + " count " + balloonCount);
}

public void PigeonDead()
{
    StartCoroutine(DelayedPigeonCount());
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
    Debug.Log("Stationary Pigeons: " + count);
    pigeonAll = pigeonCount * pigeonBuoyancy;
    Debug.Log(pigeonAll + " buoy " + pigeonBuoyancy + " count " + pigeonCount);
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

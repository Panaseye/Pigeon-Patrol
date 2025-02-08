using NUnit.Framework.Constraints;
using UnityEngine;

public class house : MonoBehaviour
{

    public float buoyancy;

    public string balloonTag = "balloon"; 
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
        // buoyancy = pigeonAll + balloonAll;
        if (buoyancy < 0)
        {
            gameObject.transform.Translate(gameObject.transform.position.x,(gameObject.transform.position.y + buoyancy) ,gameObject.transform.position.z ); 
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


            
    // we can use this to count pigeons aswell, we just add a function like 
    // for the baloons that will be triggered by the pigeon that is coming to settle
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

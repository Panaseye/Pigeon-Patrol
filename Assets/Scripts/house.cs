using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;

public class house : MonoBehaviour
{

    [SerializeField] GameObject balloonPrefab;
    public gameManager gameManager;
    public float houseDamage;

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

    public float houseProtection =1f;
    public AudioSource flap;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        houseDamage = 0;
        houseProtection=1f;
        int balloonCount = CountAllChildrenWithTag(balloonTag, transform);
        //Debug.Log("Number of children with tag '" + balloonTag + "': " + balloonCount);
    }

    // Update is called once per frame
    void Update()
    {
        pigeonCounter.CountStationaryPigeons(OnPigeonCountReady);
        UpdateBalloonCount();
        buoyancy = pigeonAll*houseProtection + balloonAll - houseDamage;

        // Debug.Log("Balloon boyancy " + balloonAll);
        //Debug.Log("Pigeon boyancy " + pigeonAll);

        if (gameObject.transform.position.y == minHeight)
        {
            gameManager.GameOver();

        }


        if (gameObject.transform.position.y <= maxHeight )
        {
            //  Calculate vertical movement based on buoyancy
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
        gameManager.feathers ++;
        gameManager.pigeonsKilled++;
        flap.Play();
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

    public void RepairHouseDamage(int repairAmount)
    {
        if(houseDamage<=repairAmount)
        {
            houseDamage=0;
            return;
        }
        houseDamage -= repairAmount;
    }
    
    public void AddBalloons()
    {
        //z 0.55, -0.55
        //y 4 , 4.8
        //x -2.3,2.3

        //hard coded values for now
        GameObject houseColoredObj = GameObject.FindGameObjectWithTag("Player").transform.Find("housecolored").Find("Roof").gameObject;
        Renderer playerRenderer = houseColoredObj.GetComponent<Renderer>();

        if(playerRenderer==null){return;}

        float spawnPosX = Random.Range(playerRenderer.bounds.min.x, playerRenderer.bounds.max.x);
        float spawnPosY= playerRenderer.bounds.max.y ;
        float spawnPosZ = Random.Range(playerRenderer.bounds.min.z, playerRenderer.bounds.max.z);


        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY , spawnPosZ);

        Transform balloonParent = GameObject.FindGameObjectWithTag("Player").transform.Find("balloons");
        Transform balloonChild = balloonParent.Find("Ballooncolorecd");

        GameObject instance = Instantiate(balloonPrefab , spawnPos , Quaternion.identity ,balloonParent);
        instance.transform.localScale = Vector3.one * 0.2f;
        Debug.Log("Balloon added");
    }
        
        
    








    
}

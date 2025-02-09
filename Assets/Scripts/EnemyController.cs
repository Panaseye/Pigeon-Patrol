
using System.Collections;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [Header("Enemy Stats")]
    [SerializeField] int enemyHealth = 20;
    [SerializeField] float enemySpeed = 5f;

    Vector3 randomLandingPoint;

    Renderer playerRenderer;
    GameObject houseObj;
    house house;
    GameObject houseColoredObj;

    public GameObject movingModel;  // Assign the "moving" model prefab
    public GameObject idleModel;    // Assign the "idle" model prefab
    public float movementThreshold = 0.01f; // Sensitivity for detecting movement
    public int checkFrames = 10; // How many frames to wait before switching

    private Vector3 lastPosition;
    private bool isChecking = false;

    [SerializeField] bool isLanded = false;
    public AudioSource coo;
    


    
    void Awake() 
    {
        
        //Calculating a random landing point for enemy
        houseObj = GameObject.FindGameObjectWithTag("Player").transform.Find("House").gameObject;
        house = GameObject.FindGameObjectWithTag("Player").GetComponent<house>();
        //playerRenderer = houseObj.GetComponent<Renderer>();
        houseColoredObj = GameObject.FindGameObjectWithTag("Player").transform.Find("housecolored").Find("Roof").gameObject;
        playerRenderer = houseColoredObj.GetComponent<Renderer>();

        //playerRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>();
        randomLandingPoint = GetRandomLandingPos();
        //Debug.Log("landing point "+ randomLandingPoint);
        lastPosition = transform.position;
        SwitchToMoving();

    
    }


    void Update() 
    {

        MoveEnemy();
        if(isLanded)
        {
            isChecking = true;
            SwitchToIdle();
        }
        
        if (!isChecking)
        {
            StartCoroutine(CheckMovement());
        }
        
        //keeps pigeons together with the house if it moves
        transform.position= new Vector3(transform.position.x , playerRenderer.bounds.max.y , transform.position.z);
    
    }

    private IEnumerator CheckMovement()
    {
        isChecking = true;
        Vector3 initialPos = transform.position;

        yield return new WaitForSeconds(checkFrames * Time.deltaTime); // Wait a few frames

        Vector3 currentPos = transform.position;
        float distanceMoved = Vector3.Distance(initialPos, currentPos);

        if (distanceMoved > movementThreshold)
        {
            SwitchToMoving();
        }
        else
        {
            SwitchToIdle();

        }
            isChecking = false;
        
    }

    void MoveEnemy()
    {
        var moveStep = enemySpeed * Time.deltaTime;
        if(playerRenderer!=null)
        {
            transform.position = Vector3.MoveTowards(transform.position , randomLandingPoint , moveStep);
        }
        if(transform.position == randomLandingPoint)
        {
            isLanded = true;

        }
    }



    Vector3 GetRandomLandingPos()
    {
        if(playerRenderer==null){return Vector3.zero;}

        float landX = Random.Range(playerRenderer.bounds.min.x, playerRenderer.bounds.max.x);
        float landY = playerRenderer.bounds.max.y;
        float landZ = Random.Range(playerRenderer.bounds.min.z, playerRenderer.bounds.max.z);

        //Debug.Log("herer" +new Vector3(landX , landY , landZ));
        return new Vector3(landX , landY , landZ);
    }


    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        Debug.Log("taken damage");
        if(enemyHealth<=0)
        {
            house.PigeonDead();
            Destroy(gameObject,0.1f);

            
            
        }
    }

    void SwitchToMoving()
    {
        movingModel.SetActive(true);
        idleModel.SetActive(false);
    }

    void SwitchToIdle()
    {
        movingModel.SetActive(false);
        idleModel.SetActive(true);
        
    }









}

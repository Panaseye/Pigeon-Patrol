
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [Header("Enemy Stats")]
    [SerializeField] int enemyHealth = 20;
    [SerializeField] float enemySpeed = 5f;

    [SerializeField] float landYPos = 0.53f; 
    Vector3 randomLandingPoint;

    Renderer playerRenderer;

    
    void Start() {
        
        //Calculating a random landing point for enemy

        playerRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>();   
        randomLandingPoint = GetRandomLandingPos();


    }


    void Update() {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        var moveStep = enemySpeed * Time.deltaTime;
        if(playerRenderer!=null)
        {
            transform.position = Vector3.MoveTowards(transform.position , randomLandingPoint , moveStep);
        }
    }



    Vector3 GetRandomLandingPos()
    {
        if(playerRenderer==null){return Vector3.zero;}

        float landX = Random.Range(playerRenderer.bounds.min.x, playerRenderer.bounds.max.x);
        float landY = landYPos;
        float landZ = Random.Range(playerRenderer.bounds.min.z, playerRenderer.bounds.max.z);

        //Debug.Log(new Vector3(landX , landY , landZ));
        return new Vector3(landX , landY , landZ);
    }


    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        
        if(enemyHealth<=0)
        {
            Destroy(gameObject,0.1f);
        }
    }









}

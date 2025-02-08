
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [Header("Enemy Stats")]
    [SerializeField] int enemyHealth;


    [SerializeField] float enemySpeed = 5f;


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
        float landY = 0.53f;
        float landZ = Random.Range(playerRenderer.bounds.min.z, playerRenderer.bounds.max.z);

        //Debug.Log(new Vector3(landX , landY , landZ));
        return new Vector3(landX , landY , landZ);
    }











}

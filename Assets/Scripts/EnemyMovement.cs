using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    

    [SerializeField] float enemyXSpeed = 5f;
    //[SerializeField] float enemyYSpeed = 3f;

    [SerializeField] GameObject rightEdge;
    [SerializeField] GameObject leftEdge;

  

    Vector3 landingRange;
    Vector3 randomLandingPoint;
    
    void Start() {
        
        //Calculating a random landing point for enemy
        landingRange = rightEdge.transform.position - leftEdge.transform.position;
        randomLandingPoint = leftEdge.transform.position + Random.value*landingRange;
    
    }


    void Update() {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        var moveStep = enemyXSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position , randomLandingPoint , moveStep);

        
    }










}

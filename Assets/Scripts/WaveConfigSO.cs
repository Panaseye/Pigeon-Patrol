using UnityEngine;

[CreateAssetMenu(fileName = "New WaveConfig", menuName = "WaveConfigSO")]
public class WaveConfigSO : ScriptableObject
{
    
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int numbOfEnemiesToSpawn;

    float timeBtwSpawn;
    [SerializeField] float baseSpawnTime =1f;
    [SerializeField] float spawnTimeVariance = 0.5f;



    public int GetNumberOfEnemiesToSpawn() {return numbOfEnemiesToSpawn;}

    public GameObject GetEnemyPrefab() {return enemyPrefab;}


    public float GetRandomTimeBtwSpawn()
    {
        timeBtwSpawn = Random.Range(baseSpawnTime-spawnTimeVariance , baseSpawnTime + spawnTimeVariance);
        return timeBtwSpawn;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBtwWaves = 2f;

    [SerializeField] bool spawnWaves = true;

    WaveConfigSO currWave;



    [Header("Spawn Bounds")]
    [SerializeField] float maxBoundZ;
    [SerializeField] float minBoundZ;
    [SerializeField] float maxBoundY;
    [SerializeField] float minBoundY;
    
    
    void Start() {

        StartCoroutine(SpawnEnemyWaves());    
    }


    IEnumerator SpawnEnemyWaves()
    {
        do{
            foreach(WaveConfigSO currConfig in waveConfigs)
            {
                currWave = currConfig;
                int enemyCount = currWave.GetNumberOfEnemiesToSpawn();
                GameObject enemyPrefab = currWave.GetEnemyPrefab();
                
                for(int i = 0 ; i < enemyCount ; i++)
                {
                    Instantiate(enemyPrefab,
                                GetRandomSpawnPos(),
                                Quaternion.identity,
                                transform);
                    
                    yield return new WaitForSeconds(currWave.GetRandomTimeBtwSpawn());
                }

                yield return new WaitForSeconds(timeBtwWaves);
            }

        }while(spawnWaves);


    }



    Vector2 GetRandomSpawnPos()
    {
        float spawnPosZ = Random.Range(minBoundZ , maxBoundZ);
        float spawnPosY = maxBoundY;

        return new Vector2(spawnPosZ , spawnPosY);
    }

}

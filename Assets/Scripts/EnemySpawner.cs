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



    Vector3 GetRandomSpawnPos()
    {
        //clockwise defind spawn regions 0,1,2
        //left screen(0), top screen(1) , right screen(2)
        int spawnRegion = Random.Range(0,3);
        float spawnPosZ=0f;
        float spawnPosY=0f;

        if(spawnRegion==0)
        {
            //spawn pos in left screen
            spawnPosZ = minBoundZ;
            spawnPosY = Random.Range(minBoundY , maxBoundY);

        }
        else if (spawnRegion==1)
        {
            //spawn pos in top screen
            spawnPosZ = Random.Range(minBoundZ , maxBoundZ);
            spawnPosY = maxBoundY;
        }
        else{
            //spawn pos in right screen
            spawnPosZ = maxBoundZ;
            spawnPosY =Random.Range(minBoundY , maxBoundY);

        }
        
        //dont know if 'x' afefcts spawn pos
        return new Vector3(3f , spawnPosY , spawnPosZ);
    }

}

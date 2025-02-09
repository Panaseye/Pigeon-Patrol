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

    
    [SerializeField] float maxBoundX;
    [SerializeField] float minBoundX;
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
                    Debug.Log("ranmdom spawn pos " + GetRandomSpawnPos());
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
        float spawnPosX=0f;
        float spawnPosY=0f;

        if(spawnRegion==0)
        {
            //spawn pos in left screen
            spawnPosX = minBoundX;
            spawnPosY = Random.Range(minBoundY , maxBoundY);

        }
        else if (spawnRegion==1)
        {
            //spawn pos in top screen
            spawnPosX = Random.Range(minBoundX , maxBoundX);
            spawnPosY = maxBoundY;
        }
        else{
            //spawn pos in right screen
            spawnPosX = maxBoundX;
            spawnPosY =Random.Range(minBoundY , maxBoundY);

        }
        
        //dont know if 'x' afefcts spawn pos
        return new Vector3(spawnPosX , spawnPosY , 0f);
    }

}

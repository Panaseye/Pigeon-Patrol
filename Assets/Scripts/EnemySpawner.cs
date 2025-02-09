using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] List<WaveConfigSO> waveConfigs;
    float timeBtwWaves = 2f;

    [SerializeField] bool spawnWaves = true;

    WaveConfigSO currWave;

    [SerializeField] List<GameObject> enemyPrefabsList;
    [SerializeField] GameObject bossPrefab;

    int startNumb=3;
    int currNumb;
    int currWaveNumber = 1;
    int bossWave=0;
    [Header("Spawn Bounds")]

    
    [SerializeField] float maxBoundX;
    [SerializeField] float minBoundX;
    [SerializeField] float maxBoundY;
    [SerializeField] float minBoundY;
    
    
    void Start() {
        currNumb = startNumb;
        StartCoroutine(SpawnEnemyWaves());    
    }


    IEnumerator SpawnEnemyWaves()
    {
        do{

            
            AddWave(currNumb);

            foreach(WaveConfigSO currConfig in waveConfigs)
            {

                Debug.Log("Current Wave -------- "+ currWaveNumber + ";"+ currNumb + ";" + currConfig.GetNumberOfEnemiesToSpawn());

                //wait for some time before spawning
                if(currWaveNumber==1){yield return new WaitForSecondsRealtime(5f);}

                currWave = currConfig;
                int enemyCount = currWave.GetNumberOfEnemiesToSpawn();
                
                
                for(int i = 0 ; i < enemyCount ; i++)
                {


                    GameObject enemyPrefab = currWave.GetEnemyPrefabAt(Random.Range(0,currWave.GetEnemyPrefabs().Count))    ;
                    Debug.Log("ranmdom spawn pos " + GetRandomSpawnPos());
                    
                    //with a 50-50 probability, change the enemy prefab to boss prefab
                    
                    int spawnBoss = Random.Range(0,2)*bossWave;
                    if(spawnBoss==1){
                        enemyPrefab = currWave.GetBossPrefab();
                    }
                    
                    
                    Instantiate(enemyPrefab,
                                GetRandomSpawnPos(),
                                Quaternion.identity,
                                transform);


                    yield return new WaitForSeconds(currWave.GetRandomTimeBtwSpawn());
                }
                bossWave = ChangeWaveNumber();
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

    //wave number in multiples of 3
    // wave - 3,6,9,12,15,18..
    void AddWave(int waveNumber)
    {
        WaveConfigSO newWave = ScriptableObject.CreateInstance<WaveConfigSO>();

        //setting parameters
        newWave.SetEnemiesToSpawn(waveNumber);
        newWave.SetTimeSpawn( currWaveNumber , 0.1f);
        //Debug.Log(enemyPrefabsList[1] + " comes here ");
        newWave.SetEnemyPrefabs(enemyPrefabsList);
        newWave.SetBossEnemyPrefab(bossPrefab);
        
        
        waveConfigs.Add(newWave);
    }

    void SetTimeBtwWaves()
    {
        timeBtwWaves = 0.05f;
    }

    int ChangeWaveNumber()
    {
        currWaveNumber++;
        //currNumb = startNumb*currWaveNumber;

        currNumb  *= currNumb;
        //return 1 , means boss wave
        //return 0 , means normal wave
        if(0!=0){return 1;}
        else {return 0;}
        
    }

}

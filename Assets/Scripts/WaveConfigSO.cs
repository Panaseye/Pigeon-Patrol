using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WaveConfig", menuName = "WaveConfigSO")]
public class WaveConfigSO : ScriptableObject
{
    
     List<GameObject> enemyPrefabs;
     int numbOfEnemiesToSpawn;

    float timeBtwSpawn;
     float baseSpawnTime =1f;
     float spawnTimeVariance = 0.5f;

    GameObject bossEnemyPrefab;

    public int GetNumberOfEnemiesToSpawn() {return numbOfEnemiesToSpawn;}

    public List<GameObject> GetEnemyPrefabs() {return enemyPrefabs;}

    public GameObject GetEnemyPrefabAt(int index){return enemyPrefabs[index];}
    public float GetRandomTimeBtwSpawn()
    {
        timeBtwSpawn = Random.Range(baseSpawnTime-spawnTimeVariance , baseSpawnTime + spawnTimeVariance);
        return timeBtwSpawn;
    }



    public void SetEnemiesToSpawn(int enemies)
    {
        numbOfEnemiesToSpawn = enemies;
    }

    public void SetTimeSpawn(float baseSpawnTime , float spawnTimeVar)
    {
        this.baseSpawnTime = baseSpawnTime;
        spawnTimeVariance = spawnTimeVar;

    }

    public void SetEnemyPrefabs(List<GameObject> enemyPrefabsList)
    {
        //if(enemyPrefabsList!=null){Debug.Log(enemyPrefabsList[1] + " hereee ");}
            // for(int i = 0 ; i < enemyPrefabsList.Count ; i++)
            // {
            //     Debug.Log("iteration " + i + enemyPrefabsList[i]);
            //     enemyPrefabs.Add(enemyPrefabsList[i]);
            // }
        enemyPrefabs = enemyPrefabsList;
    }
    


    public GameObject GetBossPrefab(){return bossEnemyPrefab;}
    
    public void SetBossEnemyPrefab(GameObject bossPrefab)
    {
        bossEnemyPrefab = bossPrefab;
    }
}

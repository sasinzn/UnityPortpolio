using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private float time = 0;
    [SerializeField]
    private uint totalSpawnEnemyNum = 1;
    private uint currentSpawnEnemyNum;
    private bool isSpawn = false;
    private float spawnInterval = 0.5f;
    private Vector3 pos;
    private List<string> pathNames = new List<string>();

    private void Start()
    {
        EnemyManager.instance.CreateEnemys("Enemy1", "Prefabs/EnemyA", 50, 0);
        EnemyManager.instance.CreateEnemys("Enemy2", "Prefabs/EnemyB", 20, 0);
        EnemyManager.instance.CreateEnemys("middleBoss", "Prefabs/EnemyC", 10, 1);
        EnemyManager.instance.CreateEnemys("Boss", "Prefabs/Boss", 1, 2);
        LoadPathData();
        totalSpawnEnemyNum = 1;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (isSpawn == false)
            StartCoroutine(SpawnIntv());
    }

    private void Spawn()
    {
        int ranNum = 0;

        

        if (totalSpawnEnemyNum > 250)
            totalSpawnEnemyNum = 0;

        pos.x = Random.Range(-2.0f, 2.0f);
        pos.y = 5.5f;
        pos.z = 0;

        if (totalSpawnEnemyNum % 100 < 1)
        {
            pos.x = 0;
            pos.y = 6;
            pos.z = 0;
            EnemyManager.instance.Spawn("Boss", pos, DataManager.instance.GetPath(pathNames[8]));
            totalSpawnEnemyNum++;
            return;
        }

        if (totalSpawnEnemyNum % 20 < 1)
        {
            pos.x = 0;
            pos.y = 6;
            pos.z = 0;
            EnemyManager.instance.Spawn("middleBoss", pos, DataManager.instance.GetPath(pathNames[8]));
            totalSpawnEnemyNum++;
            return;
        }

        if (totalSpawnEnemyNum % 5 > 3)
        {
            ranNum = Random.Range(4, 8);
            pos = DataManager.instance.GetPath(pathNames[ranNum])[0];
            EnemyManager.instance.Spawn("Enemy2", pos, DataManager.instance.GetPath(pathNames[ranNum]));
            totalSpawnEnemyNum++;
        }
        else
        {
            ranNum = Random.Range(0, 4);
            pos = DataManager.instance.GetPath(pathNames[ranNum])[0];
            EnemyManager.instance.Spawn("Enemy1", pos, DataManager.instance.GetPath(pathNames[ranNum]));
            totalSpawnEnemyNum++;
        }

    }


    IEnumerator SpawnIntv()
    {
        isSpawn = true;

        Spawn();

        yield return new WaitForSeconds(spawnInterval);

        isSpawn = false;
    }

    private void LoadPathData()
    {
        pathNames = DataManager.instance.GetPathKey();

    }
}

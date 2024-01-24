using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager instance
    {
        get
        {
            if (_instance == null)
            {
                //Create Empty
                GameObject obj = new GameObject("EnemyManager");
                _instance = obj.AddComponent<EnemyManager>();
            }

            return _instance;
        }
    }

    public int poolSize = 300;
    public float spawnInterval = 1.0f;
    public int spawnAmount = 3;
    public float spawnOffset = 0;

    private WaitForSeconds spawnTime;

    private List<GameObject> enemys = new List<GameObject>();
    private Transform target;

    private GameObject enemyPrefab;
    private Dictionary<string, List<GameObject>> totalEnemy = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        //CreateEnemys();
        BulletManager.instance.CreateBullets("Enemy", "Prefabs/EnemyBullet", 200);
    }
    public void CreateEnemys(string key, string prefab, int poolSize, int attackType = 0)
    {
        enemyPrefab = Resources.Load<GameObject>(prefab);

        List<GameObject> enemis = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.GetComponent<Enemy>().enemyAttackType = attackType;
            enemy.GetComponent<EnemyHit>().exp = (uint)Random.Range(20.0f + attackType * 20, 60.0f + attackType * 20);
            enemy.SetActive(false);
            enemis.Add(enemy);
            enemys.Add(enemy);
        }

        totalEnemy[key] = enemis;
    }
    public void Spawn(string key, Vector3 pos)
    {
        foreach (GameObject enemy in totalEnemy[key])
        {
            if (!enemy.activeSelf)
            {
                enemy.transform.position = pos;
                enemy.SetActive(true);
                return;
            }
        }
    }
    public void Spawn(string key, Vector3 pos, List<Vector3> path)
    {
        foreach (GameObject enemy in totalEnemy[key])
        {
            if (!enemy.activeSelf)
            {
                enemy.transform.position = pos;
                enemy.SetActive(true);
                foreach(Vector3 dest in path)
                {
                    enemy.GetComponent<EnemyMove>().AddPath(dest);
                }
                    
                return;
            }
        }
    }

    private void CreateEnemys()
    {
        GameObject enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);
            enemys.Add(enemy);
        }
    }

    public Transform GetClosestEnemy(Vector3 pos)
    {
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach(GameObject enemy in enemys)
        {
            if (!enemy.activeSelf) continue;
            if (!enemy.GetComponent<Enemy>().isVisible()) continue;

            float distance = Vector3.Distance(pos, enemy.transform.position);
        
            if(minDistance > distance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    public Transform GetClosestEnemy(Transform pos)
    {
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemys)
        {
            if (!enemy.activeSelf) continue;
            if (enemy.GetComponent<EnemyHit>().IsLockedOn()) continue;
            if (!enemy.GetComponent<Enemy>().isVisible()) continue;

            float distance = Vector3.Distance(pos.position, enemy.transform.position);

            if (minDistance > distance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
                enemy.gameObject.GetComponent<EnemyHit>().LockedOn(pos);
            }
        }

        return closestEnemy;
    }

    public List<Transform> GetClosestEnemys(Vector3 pos, int size)
    {
        List<Transform> closestEnemy = new List<Transform>();
        Dictionary<float, Transform> map = new Dictionary<float, Transform>();

        foreach (GameObject enemy in enemys)
        {
            if (!enemy.activeSelf) continue;
            if (!enemy.gameObject.GetComponent<Enemy>().isVisible()) continue;

            float distance = Vector3.Distance(pos, enemy.transform.position);

            map.Add(distance, enemy.transform);
        }

        List<float> list = map.Keys.ToList();
        list.Sort();

        foreach (float distance in list)
        {
            closestEnemy.Add(map[distance]);

            if (closestEnemy.Count == size)
                return closestEnemy;
        }

        return closestEnemy;
    }

}

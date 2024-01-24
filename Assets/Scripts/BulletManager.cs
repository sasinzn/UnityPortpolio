using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    protected BulletData bulletData;
    public static BulletManager instance;

    private GameObject bulletPrefab;
    //private List<GameObject> bullets = new List<GameObject>();    
    private Dictionary<string, List<GameObject>> totalBullet = new Dictionary<string, List<GameObject>>();
    private bool isFire = false;

    private void Awake()
    {
        instance = this;
        bulletData = new BulletData();
    }

    public void CreateBullets(string key, string prefab, int poolSize)
    {
        bulletPrefab = Resources.Load<GameObject>(prefab);

        List<GameObject> bullets = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }

        totalBullet[key] = bullets;
        print(key + "create complite");
    }

    public void Fire(string key, Vector3 pos)
    {
        foreach (GameObject bullet in totalBullet[key])
        {
            if (!bullet.activeSelf)
            {
                bullet.SetActive(true);
                bullet.transform.position = pos;
                return;
            }
        }
    }


    public void Fire(string key, Vector3 firePos, Vector3 targetPos)
    {
        foreach (GameObject bullet in totalBullet[key])
        {
            if (!bullet.activeSelf)
            {
                bullet.GetComponent<Bullet>().SetFire(firePos, targetPos);
                return;
            }
        }
    }


    public void FireAngle(string key, Vector3 firePos, float angle)
    {
        foreach (GameObject bullet in totalBullet[key])
        {
            if (!bullet.activeSelf)
            {
                bullet.GetComponent<Bullet>().SetFire(firePos, angle);
                return;
            }
        }
    }
}

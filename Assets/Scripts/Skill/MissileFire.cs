using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFire : MonoBehaviour
{
    public int level = 0;
    
    private List<GameObject> missiles = new List<GameObject>();
    private WaitForSeconds skillCoolTime;
    private int missileNum = 0;
    private int currentMissileNum = 1;


    private void Awake()
    {
        skillCoolTime = new WaitForSeconds(4.0f);

        GameObject prefab = Resources.Load<GameObject>("Prefabs/Missile");
        

        for (int i = 0; i < 30; i++)
        {
            GameObject obj = Instantiate(prefab,transform);
            obj.SetActive(false);
            missiles.Add(obj);
        }
    }

    private void Start()
    {
        StartCoroutine(MissileFiring());
    }


    private IEnumerator MissileFiring()
    {
        
        while (true)
        {
            MissileSet();
            yield return skillCoolTime;
        }
    }

    private void MissileSet()
    {
        if (level == 0) return;

        missileNum = level * 2;
        currentMissileNum = 1;

        foreach (GameObject missile in missiles)
        {
            if (!missile.activeSelf)
            {
                missile.SetActive(true);
                missile.transform.position = transform.position;

                if (currentMissileNum == missileNum)
                    return;    
                else
                    currentMissileNum++;
      
            }
        }
    }

    public void GetPower()
    {
        if (level > 4) return;
        level++;
    }
}

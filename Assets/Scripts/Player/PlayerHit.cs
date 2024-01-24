using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private List<GameObject> followerList = new List<GameObject>();

    private int followerNum = 0;

    public int hp = 10;

    private void Awake()
    {

        GameObject followerPrefab = Resources.Load<GameObject>("Prefabs/Follower");

        for (int i = 0; i < 4; i++)
        {
            GameObject follower = Instantiate(followerPrefab, transform);
            follower.SetActive(false);
            followerList.Add(follower);
        }

        Vector3 pos = new Vector3(-0.18f,-0.07f,0);
        followerList[0].transform.localPosition = pos;
        pos = new Vector3(0.18f, -0.07f, 0);
        followerList[1].transform.localPosition = pos;
        pos = new Vector3(-0.33f,-0.07f,0);
        followerList[2].transform.localPosition = pos;
        pos = new Vector3(0.33f,-0.07f,0);
        followerList[3].transform.localPosition = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if(collidedObject.CompareTag("EnemyBullet"))
        {
            PlayerInfo.instance.GetHitDamage(1);
            collidedObject.SetActive(false);

            
        }

        //if (collidedObject.CompareTag("PowerItem"))
        //{
        //    print("PowerItem");
        //    GetPower();
        //    
        //}
        //if (collidedObject.CompareTag("CoinItem"))
        //{
        //    print("CoinItem");
        //    
        //}
        //if (collidedObject.CompareTag("BoomItem"))
        //{
        //    print("BoomItem");
        //    
        //}

        
    }

    public void GetPower()
    {
        if (followerNum > 4)
            return;
        else
        {
            followerList[followerNum].SetActive(true);
            followerNum++;
        }
    }
}

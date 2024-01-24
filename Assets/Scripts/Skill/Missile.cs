using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5.0f;
    private Transform target = null;
    private Vector3 direction;
    private float lifeTime = 0;

    private void OnEnable()
    {
        direction.x = 0;
        direction.y = 1;
        direction.z = 0;

        if(target == null)
            target = EnemyManager.instance.GetClosestEnemy(transform);
        lifeTime = 0;
    }

    private void Update()
    {
        if (lifeTime > 2.0f)
        {
            gameObject.SetActive(false);
            target = null;
        }    
        else
        {
            lifeTime += Time.deltaTime;
            MissileMove();
        }
            
    }

    private void MissileMove()
    {
        if (!target.gameObject.activeSelf)
        {
            target = EnemyManager.instance.GetClosestEnemy(transform);
        }

        if(target == null)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            return;
        }

        direction = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) + Mathf.PI * 0.5f;
        Vector3 rot = new Vector3();
        rot.z = Mathf.Rad2Deg * angle;
        transform.rotation = Quaternion.Euler(rot);
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    private float fireInterval = 0.2f;
    private bool isFire = false;

    Transform originParent = null;
    Vector3 originLocalPos = Vector3.zero;

    Transform target = null;
    Vector3 offset = Vector3.down * 0.5f;
    

    private void Awake()
    {
        gameObject.SetActive(false);
        originParent = transform.parent;
        originLocalPos = transform.localPosition;
    }

    private void Start()
    {
        if (!isFire)
        {
            StartCoroutine(FireIntv());
        }
    }

    private void Update()
    {
        if (target == null)
            SetTarget();
        else
            TargetState();
    }

    IEnumerator FireIntv()
    {
        while(true)
        {
            isFire = true;

            BulletManager.instance.Fire("Follower", transform.position);

            yield return new WaitForSeconds(fireInterval);

            isFire = false;
        }
    }

    public void ActiveFollower()
    {
        gameObject.SetActive(true);
    }

    private void SetTarget()
    {
        target = EnemyManager.instance.GetClosestEnemy(transform.position);
        transform.parent = target;
        transform.localPosition = offset * target.gameObject.GetComponent<Enemy>().Targeted();
        
    }

    private void TargetState()
    {

        if (!target.gameObject.GetComponent<Enemy>().isVisible())
        { 
            target = null; 
            transform.parent = originParent;
            transform.localPosition = originLocalPos;
        }

        if (!target.gameObject.activeSelf)
        {   
            target = null;
            transform.parent = originParent; 
            transform.localPosition = originLocalPos;
        }
    }
}

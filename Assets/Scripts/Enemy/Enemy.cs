using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR;

public class Enemy : MonoBehaviour
{
    enum FireState
    {
        Idle,
        StraightShot,
        CircleShot,
        FastRotationShot,
        WaveShot,
        FanShot,
        End
    }

    public float fireInterval = 3f;
    public float patternInterval = 2.0f;
    public int pattern = 0;

    private bool isFire = false;
    private bool isPettern = false;
    private bool onVisible = false;
    private float fireTime = 0.0f;

    private Transform target;
    private bool isTarget = false;
    private int targetNum = 0;
    private FireState fireState = FireState.Idle;

    private float spawnTime = 0.0f;

    private GameObject jakoPrefab;
    private List<GameObject> jakos = new List<GameObject>();

    public int enemyAttackType = 0;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.Find("Player").transform;
        pattern = 1;
        
    }

    private void OnEnable()
    {
        onVisible = false;
        targetNum = 0;
        isTarget = false;
        isPettern = false;
        pattern = 1;
    }

    private void Update()
    {
        fireTime += Time.deltaTime;

        //fireState = (FireState)pattern;
        if(spriteRenderer.isVisible)
            onVisible = true;
        else
            onVisible = false;

        if(onVisible)
            Attack();

        ChangeState();
    }

    IEnumerator FireInterval()
    {
        isPettern = true;

        BulletManager.instance.Fire("Enemy", transform.position, target.position);

        yield return new WaitForSeconds(fireInterval);

        isPettern = false;
    }

    IEnumerator FastRotationShot()
    {
        float angle = 0.0f;
        float angleStep = 15.0f;
        isPettern = true;

        while (true)
        {
            BulletManager.instance.FireAngle("Enemy", transform.position, angle);

            angle -= angleStep;

            yield return new WaitForSeconds(0.05f);

            if (fireState != FireState.FastRotationShot)
            {
                isPettern = false;
                yield break;
            }
        }
    }

    IEnumerator CircleShot()
    {
        int count = 20;
        //float angleStep = Mathf.PI * 2.0f / count;
        float angleStep = 360 / count;
        float offset = 0.0f;
        isPettern = true;

        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                float angle = angleStep * i + offset;
                BulletManager.instance.FireAngle("Enemy", transform.position, angle);
            }

            offset -= 70.0f;
            yield return new WaitForSeconds(fireInterval);

            if (fireState != FireState.CircleShot)
            {
                isPettern = false;
                yield break;
            }
        }
    }

    IEnumerator WaveShot()
    {
        float angle = -90.0f;
        float angleStep = -10.0f;
        isPettern = true;

        while (true)
        {
            BulletManager.instance.FireAngle("Enemy", transform.position, angle);

            if (angle < -140.0f)
                angleStep = 10.0f;
            if(angle > -40.0f)            
                angleStep = -10.0f;


            angle += angleStep;

            yield return new WaitForSeconds(0.1f);

            if (fireState != FireState.WaveShot)
            {
                isPettern = false;
                yield break;
            }
        }
    }

    IEnumerator FanShot()
    {
        int count = 15;
        //float angleStep = Mathf.PI * 2.0f / count;
        float angleStep = 120 / count;
        float offset = -145.0f;
        isPettern = true;

        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                float angle = angleStep * i + offset;
                BulletManager.instance.FireAngle("Enemy", transform.position, angle);
            }

            //offset += 60.0f;
            yield return new WaitForSeconds(fireInterval);

            if (fireState != FireState.FanShot)
            {
                isPettern = false;
                yield break;
            }
        }
    }


    private void Attack()
    {
        switch (fireState)
        {
            case FireState.StraightShot:
                if (!isPettern)
                    StartCoroutine(FireInterval());
                break;
            case FireState.CircleShot:
                if (!isPettern)
                    StartCoroutine(CircleShot());
                break;
            case FireState.FastRotationShot:
                if (!isPettern)
                    StartCoroutine(FastRotationShot());
                break;

            case FireState.WaveShot:
                if (!isPettern)
                    StartCoroutine(WaveShot());
                break;
            case FireState.FanShot:
                if (!isPettern)
                    StartCoroutine(FanShot());
                break;
        }
    }

    private void ChangeState()
    {
        if (fireTime > patternInterval)
        {
            fireTime = 0.0f;

            if (fireState == FireState.Idle)
            {
                if(enemyAttackType == 0)
                    fireState = (FireState)Random.Range((int)FireState.StraightShot, (int)FireState.CircleShot);

                if (enemyAttackType == 1)
                    fireState = (FireState)Random.Range((int)FireState.StraightShot, (int)FireState.FastRotationShot);

                if (enemyAttackType == 2)
                    fireState = (FireState)Random.Range((int)FireState.StraightShot, (int)FireState.End);

                patternInterval = 3.0f;
            }
            else
            {
                fireState = FireState.Idle;
                patternInterval = 1.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    public bool isVisible()
    {
        return onVisible;
    }

    public int Targeted()
    {
        if (!isTarget)
        {
            isTarget = true;
            targetNum++;
        }
        else
            targetNum++;

        return targetNum;
    }

}

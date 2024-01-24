using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public int power = 1;
    private float fireInterval = 0.2f;
    private float missileInterval = 2.0f;
    private bool isFire = false;
    private bool isBoom = false;
    private bool isMissileFire = false;
    private Animator animator;
    private GameObject boom;
    public float doubleClickSecond = 0.25f;
    private bool isOneClick = false;
    private double timer = 0;
    private Rigidbody2D body = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        boom = GameObject.Find("Effect");
        BulletManager.instance.CreateBullets("Player", "Prefabs/PlayerBullet", 50);
        BulletManager.instance.CreateBullets("Follower", "Prefabs/FollowerBullet", 50);
    }

    private void Update()
    {
        if (!isFire)
        {
            StartCoroutine(FireIntv());
        }

        if (isOneClick && ((Time.time - timer) > doubleClickSecond))
        {
            isOneClick = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isOneClick)
            {
                timer = Time.time;
                isOneClick = true;
            }
            else if (isOneClick && ((Time.time - timer) < doubleClickSecond))
            {
                isOneClick = false;
                if (!isBoom)
                {
                    animator.SetTrigger("SpecialBoom");
                    StartCoroutine(BoomIntv());
                    StartCoroutine(PowerOverWhelmingIntv());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!isBoom)
            {
                animator.SetTrigger("SpecialBoom");
                StartCoroutine(BoomIntv());
            }
           
            
        }

    }

    IEnumerator FireIntv()
    {
        isFire = true;

        BulletManager.instance.Fire("Player", transform.position);

        if (power > 1)
        {
            Vector3 pos = transform.position;
            pos.x -= 0.3f;
            BulletManager.instance.Fire("Player", pos);

            pos = transform.position;
            pos.x += 0.3f;
            BulletManager.instance.Fire("Player", pos);
        }
        SoundManager.instance.PlayFX(SoundKey.FIRE);
        yield return new WaitForSeconds(fireInterval);

        isFire = false;
    }

    IEnumerator BoomIntv()
    {
        isBoom = true;
        
        yield return new WaitForSeconds(0.8f);

        boom.GetComponent<Boom>().UseX86Boom();
        isBoom = false;
        
    }

    IEnumerator PowerOverWhelmingIntv()
    {
        body.simulated = false;
        yield return new WaitForSeconds(1.5f);
        body.simulated = true;
    }
}

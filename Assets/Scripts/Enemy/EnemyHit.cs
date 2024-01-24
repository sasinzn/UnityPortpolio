using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public int maxHp;
    public uint exp;

    public int currentSpriteIndex = 0;
    public Material flashWhiteMaterial;
    private Material currentMaterial;
    private SpriteRenderer spriteRenderer;
    private Sprite enemyOriginSprite;
    private GameObject dying;
    private GameObject item;
    private Transform locked = null;

    private bool isHit = false;
    private int currentHp;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentMaterial = spriteRenderer.material;
        enemyOriginSprite = spriteRenderer.sprite;
        
    }

    private void Start()
    {
        dying = GameObject.Find("Effect");
        item = GameObject.Find("Item");
    }

    private void OnEnable()
    {
        currentHp = maxHp;
        isHit = false;
        locked = null;
    }

    private void Update()
    {
        if (currentHp <= 0)
        {
            ItemDrop(Random.Range(0, 2));
            dying.GetComponent<Dying>().Die(transform.position);
            spriteRenderer.sprite = enemyOriginSprite;
            PlayerInfo.instance.GetExp(exp);
            PlayerInfo.instance.SetScore((uint)(exp*0.5f));
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.CompareTag("EnemyBullet"))
        {
            return;
        }

        if (collidedObject.CompareTag("PlayerBullet"))
        {
            currentHp -= (int)PlayerInfo.instance.GetAttackDamage();
            collidedObject.SetActive(false);

            if (isHit == false)
            {
                spriteRenderer.material = flashWhiteMaterial;
                
                StartCoroutine(RevertSprite());
            }
        }
        if(collidedObject.CompareTag("Missile"))
        {
            currentHp -= 3;
            collidedObject.SetActive(false);
        }
    }

    private IEnumerator RevertSprite()
    {
        isHit = true;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.material = currentMaterial;
        isHit = false;
    }

    private void ItemDrop(int index)
    {
        switch (index)
        {
            //case 0:
            //    item.GetComponent<Item>().SpawnCoin(transform.position);
            //    break;
            //case 1:
            //    item.GetComponent<Item>().SpawnPower(transform.position);
            //    break;
            //case 2:
            //    item.GetComponent<Item>().SpawnBoom(transform.position);
            //    break;
        }
    }

    public void Damaged(float  damage)
    {
        currentHp -= (int)damage;
    }

    public bool IsLockedOn()
    {
        if (locked == null)
            return false;
        else
            return true;
    }
    public void LockedOn(Transform targeted)
    {
        locked = targeted;
    }
}


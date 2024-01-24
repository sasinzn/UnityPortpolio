using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Sprite sprite;
    public Vector3 direction;
    public float speed = 10.0f;

    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (!renderer.isVisible)
            gameObject.SetActive(false);
    }

    public void SetFire(Vector3 firePos, Vector3 targetPos)
    {
        gameObject.SetActive(true);
        direction = (targetPos - firePos).normalized;
        transform.position = firePos;

        float angle = Mathf.Atan2(direction.y, direction.x) + Mathf.PI * 0.5f;
        Vector3 rot = new Vector3();
        rot.z = Mathf.Rad2Deg * angle;
        transform.rotation = Quaternion.Euler(rot);
    }

    public void SetFire(Vector3 firePos, float angle)
    {
        gameObject.SetActive(true);
        direction.x = Mathf.Cos(angle * Mathf.Deg2Rad);
        direction.y = Mathf.Sin(angle * Mathf.Deg2Rad);
        transform.position = firePos;

        Vector3 rot = new Vector3();
        rot.z = angle + 90;
        transform.rotation = Quaternion.Euler(rot);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    gameObject.SetActive(false);
    //}
}

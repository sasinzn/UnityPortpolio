using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour
{
    private List<Vector3> path = new List<Vector3>();
    private Vector3 destPos;
    private Transform target;
    public float moveSpeed = 1;
    
    private bool isVisible = false;

    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.Find("Player").transform;

        
    }

    private void OnEnable()
    {
        destPos = transform.position;
    }

    private void Update()
    {
        Move();

    }
    private void Move()
    {
        Vector3 direction = destPos - transform.position;

        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

        if (direction.magnitude < 0.01f)
        {
            if(path.Count == 0)
                gameObject.SetActive(false);

            if (path.Count > 0)
            {
                destPos = path[0];
                path.RemoveAt(0);
            }
        }
    }

    public void AddPath(Vector3 pos)
    {
        path.Add(pos);
    }


   
}

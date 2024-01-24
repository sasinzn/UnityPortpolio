using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GameManager.instance.GoLogin();

        if(Input.GetMouseButtonDown(0))
            GameManager.instance.GoLogin();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0;
        }
    }

    public void InventoryOpen()
    {
        gameObject.SetActive(true);
    }

    public void InventoryClose()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}

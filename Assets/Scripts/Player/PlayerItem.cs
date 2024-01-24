using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    private GameObject inventory;

    // Start is called before the first frame update
    private void Start()
    {
        inventory = GameObject.Find("Inventory");
    }

   
}

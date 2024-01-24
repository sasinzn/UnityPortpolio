using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    private Transform powerUpItemBtn1;
    private Text explainText;
    private ItemData selectItem;

    private void Awake()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();

        foreach (Transform t in transforms)
        {
            if (t.name == "PowerUpBtn3")
                powerUpItemBtn1 = t;
        }
    }

    private void Start()
    {
        CreateUpgradeItem();
    }

    private void CreateUpgradeItem()
    {
        Dictionary<int,ItemData> items = DataManager.instance.itemDatas;

        foreach(KeyValuePair<int,ItemData> item in items)
        {
            powerUpItemBtn1.GetComponent<UpgradeItemBtn>().SetData(item.Value);
        }
    }

    public void SelectItem(ItemData item)
    {
        selectItem = item;
        explainText.text = item.name.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemBtn : MonoBehaviour
{
    private Image image;
    private ItemData itemData;
    private Button button;
    public int type = 0;

    private GameObject player;
   

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        button.onClick.AddListener(ClickItem);
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        

    }

    public void SetData(ItemData itemData)
    {
        this.itemData = itemData;

        Sprite sprite = Resources.Load<Sprite>(itemData.sprite);
        image.sprite = sprite;
    }

    private void ClickItem()
    {
        if(type == 0)
        {
            player.GetComponent<LightningSkill>().GetPower();
            transform.parent.parent.gameObject.SetActive(false);
            PlayerInfo.instance.touch = true;
        }


        if(type == 1)
        {
            player.GetComponent<PlayerHit>().GetPower();
            transform.parent.parent.gameObject.SetActive(false);
            PlayerInfo.instance.touch = true;
        }
        
        if(type == 2)
        {
            player.GetComponent<MissileFire>().GetPower();
            transform.parent.parent.gameObject.SetActive(false);
            PlayerInfo.instance.touch = true;
        }
    }
}

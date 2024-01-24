using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    private Transform parentpanel;
    private Transform upgradePanel;
    [SerializeField]
    private GameObject upgradeUI;
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private GameObject score;
    [SerializeField]
    private GameObject exp;
    [SerializeField]
    private GameObject level;
    private Button button;
    private Transform scoreUI;

    public bool uiPanelOn = false;
    


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        upgradeUI = GameObject.Find("UpgradeUI");
        UI = GameObject.Find("UI");
        score = GetChildObj(UI.transform, "Score");
        exp = GetChildObj(UI.transform, "EXP");
        level = GetChildObj(UI.transform, "Level");

        upgradeUI.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UpgradePanelOnOff();
        }

        if(upgradeUI.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;


        UpdateExp();
        level.GetComponent<TextMeshProUGUI>().text = PlayerInfo.instance.GetLevel().ToString();
        score.GetComponent<TextMeshProUGUI>().text = PlayerInfo.instance.GetScore().ToString();
    }

    private GameObject GetChildObj(Transform modelTr, string strName)
    {
        Transform[] AllData = modelTr.GetComponentsInChildren<Transform>();

        foreach (Transform trans in AllData)
        {
            if (trans.name == strName)
            {
                return trans.gameObject;
            }
        }
        return null;
    }

    public void UpgradePanelOnOff()
    {
        upgradeUI.SetActive(!upgradeUI.activeSelf);
        uiPanelOn = !uiPanelOn;
        //upgradePanel.gameObject.SetActive(!upgradePanel.gameObject.activeSelf);
    }

    private void UpdateExp()
    {
        exp.GetComponent<Slider>().value = PlayerInfo.instance.GetExpPercent();
    }
}

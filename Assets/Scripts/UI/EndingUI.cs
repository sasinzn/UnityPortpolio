using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingUI : MonoBehaviour
{
    [SerializeField]
    private GameObject rankingPenel;
    [SerializeField]
    private GameObject rankingNum;
    [SerializeField]
    private GameObject rankingNick;
    [SerializeField]
    private GameObject rankingScore;

    private List<NetworkManager.RankData> rankings = new List<NetworkManager.RankData>();

    private void Awake()
    {
        rankingPenel = GetChildObj(transform, "PanelReaderBoard");
        rankingNum = GetChildObj(transform, "RankingNum");
        rankingNick = GetChildObj(transform, "RankingNick");
        rankingScore = GetChildObj(transform, "RankingScore");
        rankingPenel.SetActive(false);
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

    public void OnClickRestart()
    {
        PlayerInfo.instance.ReStart();
        GameManager.instance.SelectStart();
    }

    public void OnClickRankingView()
    {
        rankings = NetworkManager.instance.ranking;

        string rankNum = "순위\n";
        string rankNick = "닉네임\n";
        string rankScore = "점수\n";

        foreach (NetworkManager.RankData rank in rankings)
        {
            rankNum += (rank.rankNum.ToString()+"\n");
            rankNick += (rank.nickName + "\n");
            rankScore += (rank.score.ToString() + "\n");
        }
        rankingNum.GetComponent<TextMeshProUGUI>().text = rankNum;
        rankingNick.GetComponent<TextMeshProUGUI>().text = rankNick;
        rankingScore.GetComponent<TextMeshProUGUI>().text = rankScore;

        rankingPenel.SetActive(true);
    }
    public void OnClickOK()
    {
        rankingPenel.SetActive(false);
    }
}

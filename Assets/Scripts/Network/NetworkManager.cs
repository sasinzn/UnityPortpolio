using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EconomyModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class NetworkManager : MonoBehaviour
{
    private enum State
    {
        LOGIN,
        CREATEACCOUNT,
        ERROR
    };
    public struct RankData
    {
        public int rankNum;
        public string nickName;
        public int score;
    }
    public static NetworkManager instance;
    public TMP_InputField idInput;
    public TMP_InputField pwInput;
    public TMP_InputField pw2Input;
    public TMP_InputField inputNickName;
    public TextMeshProUGUI showNickName;
    public TextMeshProUGUI topMessage;
    public TextMeshProUGUI bottomMessage;
    public Text errorText;
    public List<RankData> ranking = new List<RankData>();
    public bool rankingLoad = false;

    [SerializeField]
    private GameObject ui;
    [SerializeField]
    private string userId;
    [SerializeField]
    private string userPassword;
    [SerializeField]
    private string correctPassword;
    [SerializeField] 
    private string userNickname;
    [SerializeField]
    private State state;
    private State curState;

    private GameObject loginPenel;
    private GameObject createPenel;
    private GameObject errorPenel;

    private void Awake()
    {
        instance = this;
        state = State.LOGIN;
        PlayFabSettings.TitleId = "E3F05";
        ui = GameObject.Find("LoginUI");
        DontDestroyOnLoad(gameObject);

        loginPenel = GetChildObj(ui.transform, "PanelLogin");
        createPenel = GetChildObj(ui.transform, "PanelCreateAccount");
        errorPenel = GetChildObj(ui.transform, "PanelError");
        topMessage = GetChildObj(ui.transform, "TopMessage").GetComponent<TextMeshProUGUI>();
        bottomMessage = GetChildObj(ui.transform, "BottomMessage").GetComponent<TextMeshProUGUI>();
        createPenel.SetActive(false);
        errorPenel.SetActive(false);
    }

    private void ChangeState(State state)
    {
        if(state == State.CREATEACCOUNT)
        {
            ResetText();
            loginPenel.SetActive(false);
            createPenel.SetActive(true);
            this.state = state;
        }
        if(state == State.LOGIN)
        {
            ResetText();
            createPenel.SetActive(false);
            loginPenel.SetActive(true);
            this.state = state;
        }
        if(state == State.ERROR)
        {
            errorPenel.SetActive(true);
            curState = this.state;
            this.state = state;
        }
    }

    private void ResetText()
    {
        if (idInput != null) idInput.text = "";
        if (pwInput != null) pwInput.text = "";
        if (pw2Input != null) pw2Input.text = "";
        if (inputNickName != null) inputNickName.text = "";
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

    public void IDValueChanged()
    {
        if(state == State.LOGIN)
        {
            idInput = GetChildObj(ui.transform, "InputField_ID").GetComponent<TMP_InputField>();
            userId = idInput.text.ToString();
            //userId = userId.Remove(userId.Length - 1);
        }

        if (state == State.CREATEACCOUNT)
        {
            idInput = GetChildObj(ui.transform, "ID_Input").GetComponent<TMP_InputField>();
            userId = idInput.text.ToString();
            //userId = userId.Remove(userId.Length - 1);
        }
    }

    public void PWValueChanged()
    {
        if (state == State.LOGIN)
        {
            pwInput = GetChildObj(ui.transform, "InputField_PW").GetComponent<TMP_InputField>();
            userPassword = pwInput.text.ToString();
            //userPassword = userPassword.Remove(userPassword.Length - 1);
        }

        if (state == State.CREATEACCOUNT)
        {
            pwInput = GetChildObj(ui.transform, "PW_Input").GetComponent<TMP_InputField>();
            userPassword = pwInput.text.ToString();
            //userPassword = userPassword.Remove(userPassword.Length - 1);
        }
    }

    public void PW2ValueChanged()
    {
        if (state == State.CREATEACCOUNT)
        {
            pw2Input = GetChildObj(ui.transform, "PW_Input2").GetComponent<TMP_InputField>();
            correctPassword = pw2Input.text.ToString();
            //userPassword2 = userPassword2.Remove(userPassword2.Length - 1);
        }
    }

    public void NickNameValueChanged()
    {
        if (state == State.CREATEACCOUNT)
        {
            inputNickName = GetChildObj(ui.transform, "Nick_input").GetComponent<TMP_InputField>();
            userNickname = inputNickName.text.ToString();
            //userNickname = userNickname.Remove(userNickname.Length - 1);
        }
    }


    public void Login()
    {
        //������ ID �� PW ������ ���� ��ġ�ϴ� ������ �ִ��� ��û
        var request = new LoginWithPlayFabRequest { Username = userId, Password = userPassword };
        //�α��� ������ LoginSuccess�� ���н� LoginFailure�� request ����
        PlayFabClientAPI.LoginWithPlayFab(request, LoginSuccess, LoginFailure);
    }

    public void Register()
    {
        //�Է¹��� �н������ �н����� Ȯ���� ��ġ�ϴ��� Ȯ��
        if(userPassword == correctPassword)
        {
            //ID�� PW, �г��� ������ ������ �������� ��û, �̸���������
            var request = new RegisterPlayFabUserRequest { Username = userId, Password = userPassword, 
                DisplayName = userNickname, RequireBothUsernameAndEmail = false };
            //���Լ����� RegisterSuccess�� ���н� RegisterFailure�� result����
            PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
            topMessage.text = "���Լ���";
            bottomMessage.text = "ȸ�������� �Ϸ�Ǿ����ϴ�.";
            ChangeState(State.ERROR);
        }
        else
        {
            topMessage.text = "����";
            bottomMessage.text = "�н����尡 ���� ��ġ���� �ʽ��ϴ�.";
            ChangeState(State.ERROR);
        }
       
    }

    private void LoginSuccess(LoginResult result)
    {
        Debug.Log("�α��� ����");
        //SceneManager.LoadScene("Launcher");

        // �������� �޾ƿ�
        var request = new GetAccountInfoRequest { Username = userId };
        // �������� ��û ������ GetAcountSuccess�� ���н� GetAccountFailure�� result����
        PlayFabClientAPI.GetAccountInfo(request, GetAccountSuccess, GetAccountFailure);

        GameManager.instance.SelectStart();
    }

    private void LoginFailure(PlayFabError error)
    {
        Debug.LogWarning("�α��� ����");
        Debug.LogWarning(error.GenerateErrorReport());
        errorText.text = error.GenerateErrorReport();
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("���� ����");
        errorText.text = "���� ����";
    }

    private void RegisterFailure(PlayFabError error)
    {
        Debug.LogWarning("���� ����");
        Debug.LogWarning(error.GenerateErrorReport());
        errorText.text = error.GenerateErrorReport();
    }

    private void GetAccountSuccess(GetAccountInfoResult result)
    {
        Debug.Log("Accout�� ���������� �޾ƿ�");

        string nickname = result.AccountInfo.TitleInfo.DisplayName;
        if (nickname == null)
        {
            //�޾ƿ� �г����� ������ �г��� ���� ������
            //SceneManager.LoadScene("NicknameSet");
            showNickName.text = "�г����� �������ּ���";
        }
        else
        {
            //�޾ƿ� �г����� ������ �����ϰ� ��������
            //PlayerPrefs.SetString("Nickname", nickname);
            //SceneManager.LoadScene("Lobby");
            //showNickName.text = nickname;
        }
    }

    private void GetAccountFailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void OnclickRegister()
    {
        ChangeState(State.CREATEACCOUNT);
    }

    public void OnclickCancel()
    {
        ChangeState(State.LOGIN);
    }

    public void OnclickOK()
    {
        errorPenel.SetActive(false);
        this.state = curState;
    }
    private void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendReaderBoard(int score)
    {
        // �Է¹��� ������ Playfab�� ����ǥ�� ������Ʈ StatisticName = ����ǥ �̸�, value ���ް�
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Score",
                    Value = score
                }
            }
        };
        //������Ʈ ������ OnReaderboardUpdate �� ���н� OnError�� result����
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnReaderboardUpdate, OnError);
    }

    private void OnReaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful Leaderboard Sent");
    }

    public void GetReaderBoard()
    {
        //Playfab ������ ����ǥ ������ ��û�� StatisticName : ����ǥ �̸�
        //StartPosition : �޾ƿ� ����ǥ�� ������ġ MaxResultsCount : �޾ƿ� ����ǥ ������ 
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Score",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        //������ ����ǥ ��û ������ OnLeaderboardGet ���� ���н� OnError�� result����
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        Debug.Log("Successful Leaderboard Get");

        ranking.Clear();

        //PlayFab �������� �޾ƿ� �������� �ʿ��� ������ ���� / ����, �г���, ����
        foreach (var item in result.Leaderboard)
        {
            RankData rank;
            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
            rank.rankNum = item.Position + 1;   //���� PlayFab�� 0���� ����
            rank.nickName = item.DisplayName;   //�г���
            rank.score = item.StatValue;        //����
            
            ranking.Add(rank);
        }
        rankingLoad = true;
    }

    public void ScoreSent(int scoreNum)
    {
        SendReaderBoard(scoreNum);
    }
}

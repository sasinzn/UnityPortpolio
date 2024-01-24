using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int selectNum;
    
    
    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        SoundManager.instance.PlayBGM(SoundKey.LOBBY_BGM);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
       
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(0);
        SoundManager.instance.PlayBGM(SoundKey.LOBBY_BGM);
    }

    public void GoLogin()
    {
        SceneManager.LoadScene(4);
    }

    public void GoMain()
    {
        SceneManager.LoadScene(1);
    }

    public void SelectStart()
    {
        SceneManager.LoadScene(2);
        SoundManager.instance.PlayBGM(SoundKey.SELECT_BGM);
        //SceneManager.LoadScene("SelectScene");
    }

    public void GameStart()
    {
        SceneManager.LoadScene(3);
        SoundManager.instance.PlayBGM(SoundKey.PLAY_BGM);
        //SceneManager.LoadScene("PlayScene");

    }

    public void GameEnd()
    {
        SceneManager.LoadScene(5);
        SoundManager.instance.PlayBGM(SoundKey.END_BGM);
    }

    public void SelectPlane(int selectNum)
    {
        this.selectNum = selectNum;
    }

    public int GetSelectPlane()
    {
        return selectNum;
    }
}

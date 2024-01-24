using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public void OnClickRestartGame()
    {
        GameManager.instance.GoTitle();
    }

    public void OnClickReaderBoard()
    {

    }
}

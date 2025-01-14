using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Realtime;
using AuthenticationValues = Photon.Chat.AuthenticationValues;
#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif

public class CanvasWelcome : UICanvas
{
    public void StartButton()
    {
        GameManager.Ins.ChangeState(GameState.Playing);
        GameManager.Ins.SetActiveGamePlay(true);
        Close(0);
    }

    public void ExitButton()
    {
        // #if UNITY_EDITOR
        // UnityEditor.EditorApplication.isPlaying = false;
        // #else
        // Application.Quit();
        // #endif
        PhotonNetwork.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Close(0);
    }

    public void RankButton()
    {
        GameManager.Ins.ChangeState(GameState.Pause);
        GUIManager.Ins.OpenUI<CanvasRank>();
        Close(0);
    }
}


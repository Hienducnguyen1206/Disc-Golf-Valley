using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif

        Close(0);
    }
}


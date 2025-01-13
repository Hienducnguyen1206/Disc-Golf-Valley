using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRank : UICanvas
{
    public void ExitButton()
    {
        GUIManager.Ins.OpenUI<CanvasWelcome>();
        Close(0);
    }
}


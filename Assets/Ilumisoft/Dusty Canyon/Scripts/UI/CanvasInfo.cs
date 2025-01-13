using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasInfo : UICanvas
{
    public TMP_Text scoreText;

    public void ResumeButton()
    {
        GameManager.Ins.ChangeState(GameState.Playing);
        GameManager.Ins.Init();
        Close(0);
    }

    public void ExitButton()
    {
        GameManager.Ins.ChangeState(GameState.Start);
        GUIManager.Ins.OpenUI<CanvasWelcome>();
        Close(0);
    }

    private void Awake()
    {
        Debug.Log("Score: " + GameManager.Ins.GetScore());
        scoreText.text = GameManager.Ins.GetScore();
    }

    private void Start()
    {
        Debug.Log("Score: " + GameManager.Ins.GetScore());
        scoreText.text = GameManager.Ins.GetScore();
    }
}

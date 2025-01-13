using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasScore : UICanvas
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
        if (PlayerPrefs.HasKey("Score"))
        {
            scoreText.text = PlayerPrefs.GetString("Score");
            GameManager.Ins.AddOrUpdateItem(PlayerPrefs.GetString("AccountName"), int.Parse(scoreText.text));
        }
    }
}

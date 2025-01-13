using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasScore : UICanvas
{
    public TMP_Text scoreText;
    public TMP_Text winloseText;

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

    private void Update()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            scoreText.text = PlayerPrefs.GetString("Score");
            if (int.Parse(PlayerPrefs.GetString("Score")) >= 95)
            {
                winloseText.text = "You Win!";
            }
            else if (int.Parse(PlayerPrefs.GetString("Score")) < 95)
            {
                winloseText.text = "You Lose!";
            }
            return;
        }
    }
}

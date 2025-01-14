using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarChoiceBtn : MonoBehaviour
{
    private Button btn;
    private Image avatarImage; 
    void Start()
    {
        btn = GetComponent<Button>();
        avatarImage = GetComponent<Image>();
        btn.onClick.AddListener(SetNewAvatar);
    }

    public void SetNewAvatar()
    {
        
        if (Avatar.instance != null)
        {

            Avatar.instance.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Avatar/" + avatarImage.sprite.name);
            FirebaseAuthManager.instance.CurrentPlayerData.AvatarCode = avatarImage.sprite.name;
            FirebaseAuthManager.instance.SavePlayerData(FirebaseAuthManager.instance.auth.CurrentUser.UserId);
        }
        else
        {
            Debug.LogWarning("Avatar instance is not set!");
        }
    }
}

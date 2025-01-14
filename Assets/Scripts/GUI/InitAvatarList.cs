using UnityEngine;
using UnityEngine.UI;

public class InitAvatarList : MonoBehaviour
{
    [SerializeField] GameObject AvatarBtnPrefabs; 
    [SerializeField] Transform container; 


    void Start()
    {
        InitAvatarButtons();
    }

  
    void InitAvatarButtons()
    {
      
        Sprite[] avatarSprites = Resources.LoadAll<Sprite>("Avatar");

       
        foreach (Sprite avatarSprite in avatarSprites)
        {
          
            GameObject avatarButton = Instantiate(AvatarBtnPrefabs, container);

            Image buttonImage = avatarButton.GetComponent<Image>();
            buttonImage.sprite = avatarSprite;

        
        }
    }

    // Sự kiện khi người dùng chọn avatar
   
}

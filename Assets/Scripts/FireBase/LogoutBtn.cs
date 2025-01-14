using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoutBtn : MonoBehaviour
{
    Button btn;
    FirebaseAuthManager firebaseManager;

    private void Start()
    {
        firebaseManager  = FindObjectOfType<FirebaseAuthManager>();
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(Logout);

    }

   


    public void Logout()
    {
        if (firebaseManager != null)
        {
            PlayerPrefs.DeleteAll();
            firebaseManager.SignOut(); 
        }
    }
   
    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIManager : MonoBehaviour
{
    public static LoginUIManager Instance;

    [SerializeField]
    private GameObject loginPanel;

    [SerializeField]
    private GameObject registrationPanel;

    [SerializeField]
    private GameObject emailVerificationPanel;

    [SerializeField]
    private TextMeshProUGUI emailVerificationText;

    [SerializeField]
    public TextMeshProUGUI Notification;


    [Space]
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    // Registration Variables
    [Space]
    [Header("Registration")]

    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField confirmPasswordRegisterField;


    public Button QuitGameBtn;

    private void Awake()
    {
        CreateInstance();
        QuitGameBtn.onClick.AddListener(QuitGame);
    }

    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void OpenLoginPanel()
    {   

        loginPanel.SetActive(true);
        registrationPanel.SetActive(false);
        ResetNortification();
    }

    public void OpenRegistrationPanel()
    {
        registrationPanel.SetActive(true);
        loginPanel.SetActive(false);
        ResetNortification();
    }


    public void ClearUI()
    {
         loginPanel.SetActive(false );
         registrationPanel.SetActive(false);
        emailVerificationPanel.SetActive(false);
        ResetNortification();
    }


    public void ShowVerificationResponse(bool isEmailSent, string emailId, string errorMessage)
    {
        ClearUI();
         emailVerificationPanel.SetActive(true);

        if (isEmailSent)
        {
            emailVerificationText.text = $" Verification email has been sent to {emailId}";
        }
        else
        {
            emailVerificationText.text = $" Please check and vertify your email";
        }
    }

    public void ResetNortification()
    {
        Notification.text = "";
    }



    public void QuitGame()
    {
#if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Tho�t game khi build
            Application.Quit();
#endif
    }




}
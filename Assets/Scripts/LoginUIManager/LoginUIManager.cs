using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private void Awake()
    {
        CreateInstance();
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
    }

    public void OpenRegistrationPanel()
    {
        registrationPanel.SetActive(true);
        loginPanel.SetActive(false);
    }


    public void ClearUI()
    {
        loginPanel.SetActive(false );
        registrationPanel.SetActive(false);
        emailVerificationPanel.SetActive(false);
    }


    public void ShowVerificationResponse(bool isEmailSent, string emailId, string errorMessage)
    {
        ClearUI();
        emailVerificationPanel.SetActive(true);

        if (isEmailSent)
        {
            emailVerificationText.text = $"Please verify your email address \n Verification email has been sent to {emailId}";
        }
        else
        {
            emailVerificationText.text = $"Couldn't sent email : {errorMessage}";
        }
    }



}
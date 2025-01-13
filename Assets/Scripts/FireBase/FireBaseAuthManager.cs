using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class FirebaseAuthManager : MonoBehaviour
{
    // Firebase variable
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;

    // Login Variables
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


    public static FirebaseAuthManager instance;

    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        


       
    




    // Check that all of the necessary dependencies for firebase are present on the system
    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
            }
        });
    }


    private void Start()
    {
        InitUIReference();
    }

    private void FixedUpdate()
    {
        InitUIReference();

    }


    public void InitUIReference()
    {    if (emailLoginField == null) {
            emailLoginField = LoginUIManager.Instance.emailLoginField;
            passwordLoginField = LoginUIManager.Instance.passwordLoginField;


            emailRegisterField = LoginUIManager.Instance.emailRegisterField;
            passwordRegisterField = LoginUIManager.Instance.passwordRegisterField;
            confirmPasswordRegisterField = LoginUIManager.Instance.confirmPasswordRegisterField;
        }
        
    }



    void InitializeFirebase()
    {
        //Set the default instance object
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    public void Login()
    {
        StartCoroutine(LoginAsync(emailLoginField.text, passwordLoginField.text));
    }

    private IEnumerator LoginAsync(string email, string password)
    {
        PlayerPrefs.SetString("AccountName", email);
        PlayerPrefs.Save();
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
           
           

            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;
          

            string failedMessage = "Login Failed! Because ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    failedMessage += "Email is invalid";
                    LoginUIManager.Instance.Notification.text = "Email is invalid";
                    break;
                case AuthError.WrongPassword:
                    failedMessage += "Wrong Password";
                    LoginUIManager.Instance.Notification.text = "Wrong Password";
                    break;
                case AuthError.MissingEmail:
                    failedMessage += "Email is missing";
                    LoginUIManager.Instance.Notification.text = "Email is missing";
                    break;
                case AuthError.MissingPassword:
                    failedMessage += "Password is missing";
                    LoginUIManager.Instance.Notification.text = "Password is missing";
                    break;
                case AuthError.UserNotFound:
                    LoginUIManager.Instance.Notification.text = "User not found";
                    break;
                case AuthError.TooManyRequests:
                        LoginUIManager.Instance.Notification.text = "Too many request";
                    break ;
                default:
                    failedMessage = "Login Failed";
                    break;
            }

          
        }
        else
        {
            user = loginTask.Result.User;

            Debug.LogFormat("{0} You Are Successfully Logged In", user.DisplayName);


            if (user.IsEmailVerified)
            {
                References.userName = user.DisplayName;
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
            }else
            {
                SendEmailForVerification();
            }
            
        }
    }

    public void Register()
    {
        StartCoroutine(RegisterAsync( emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterAsync( string email, string password, string confirmPassword)
    {
        if (email == "")
        {
            
            LoginUIManager.Instance.Notification.text = "Email is empty";
        }
        else if ( password == "")
        {
           
            LoginUIManager.Instance.Notification.text = "Password field is empty";
        }
        else if (confirmPassword == "")
        {          
            LoginUIManager.Instance.Notification.text = "ConfirmPassword field is empty";
        }else if(passwordRegisterField.text != confirmPasswordRegisterField.text)
        {
            LoginUIManager.Instance.Notification.text = "Password does not match";
        }
       
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
              

                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;

          
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        LoginUIManager.Instance.Notification.text = "Email is invalid";
                        break;
                    case AuthError.WrongPassword:
                        LoginUIManager.Instance.Notification.text = "Wrong Password";
                        break;
                    case AuthError.MissingEmail:
                        LoginUIManager.Instance.Notification.text = "Email is missing";
                        break;
                    case AuthError.MissingPassword:
                        LoginUIManager.Instance.Notification.text = "Password is missing";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        LoginUIManager.Instance.Notification.text = "Email already in use";
                        break;
                    case AuthError.WeakPassword:
                        LoginUIManager.Instance.Notification.text = "Weak password";
                        break ;
                    case AuthError.TooManyRequests:
                        LoginUIManager.Instance.Notification.text = "Too many request";
                        break ;
                    default:
                        LoginUIManager.Instance.Notification.text = "Registration Failed";
                        break;
                }

              
            }
            else
            {
                // Get The User After Registration Success
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = name };

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception != null)
                {
                    // Delete the user if user update failed
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);

                    FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)firebaseException.ErrorCode;


                    string failedMessage = "Profile update Failed! Becuase ";
                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failedMessage += "Email is invalid";
                            break;
                        case AuthError.WrongPassword:
                            failedMessage += "Wrong Password";
                            break;
                        case AuthError.MissingEmail:
                            failedMessage += "Email is missing";
                            break;
                        case AuthError.MissingPassword:
                            failedMessage += "Password is missing";
                            break;
                        default:
                            failedMessage = "Profile update Failed";
                            break;
                    }

                    Debug.Log(failedMessage);
                }
                else
                {
                    Debug.Log("Registration Sucessfuly Welcome " + user.DisplayName);
                    if (user.IsEmailVerified) {
                        LoginUIManager.Instance.OpenLoginPanel();
                    }
                    {
                        SendEmailForVerification();
                    }
                   


                }
            }
        }
    }


    public void SendEmailForVerification()
    {
       
            StartCoroutine(SendEmailForVerificationAsync());
        
    }


    IEnumerator SendEmailForVerificationAsync()
    {
        if (user != null)
        {
            var sendEmailTask  = user.SendEmailVerificationAsync();

            yield return new WaitUntil (() => sendEmailTask.IsCompleted);

            if (sendEmailTask.Exception != null)
            {
                FirebaseException firebaseException = sendEmailTask.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string errorMessage = "Unknown Error: Please try again later";
                switch (error)
                {
                    case AuthError.Cancelled:
                        errorMessage = "Email vertification was cancelled";
                        break;
                    case AuthError.TooManyRequests:
                        errorMessage = "Too many request";
                        break;                  
                    case AuthError.InvalidRecipientEmail:
                        errorMessage = "Email invalid";
                        break;
                }
                LoginUIManager.Instance.ShowVerificationResponse(false, user.Email, errorMessage);
            }
            else
            {
                Debug.Log("Email has succesfully sent");
                LoginUIManager.Instance.ShowVerificationResponse(true, user.Email, null);
            }

        }
    }

    private void OnApplicationQuit()
    {
        SignOut();
    }

    public void SignOut()
    {
        if (auth.CurrentUser != null)
        {
            auth.SignOut();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
            Debug.Log("User signed out successfully.");
           
        }
    }

    bool CheckError(AggregateException exception, int firebaseExceptionCode)
    {
      
        foreach (var e in exception.Flatten().InnerExceptions)
        {
            if (e is Firebase.FirebaseException fbEx) 
            {
                return fbEx.ErrorCode == firebaseExceptionCode; 
            }
        }
        return false; 
    }



}


using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.Events;
using Firebase;

public class AuthenticatorManager : MonoBehaviour
{
    private FirebaseAuth firebaseAuthenticator;


    [Header("Log In Events")]
    public UnityEvent logInFail_Event;
    public UnityEvent logInSuccess_Event;

    [Header("Sign Up Events")]
    public UnityEvent signUpFail_Event;
    public UnityEvent signUpSuccess_Event;
    public UnityEvent signUpInvalidEmail_Event;
    public UnityEvent signUpWeakPassword_Event;
    public UnityEvent signUpEmailAlreadyInUse_Event;

    [Header("Sign Out Events")]
    public UnityEvent signOutSuccess_Event;

    [Header("Reset Password Events")]
    public UnityEvent resetPasswordFail_Event;
    public UnityEvent userNotFound_Event;
    public UnityEvent invalidEmail_Event;
    public UnityEvent resetPasswordSuccess_Event;


    [Header("Delete Account Events")]
    public UnityEvent deleteAccountFail_Event;
    public UnityEvent deleteAccountSuccess_Event;

    [Header("Send Email Verification Events")]
    public UnityEvent sendEmailVerificationFail_Event;
    public UnityEvent sendEmailVerificationSuccess_Event;



    void Start()
    {
        firebaseAuthenticator = FirebaseAuth.DefaultInstance;
    }

    private void OnEnable()
    {
        firebaseAuthenticator.StateChanged += AuthStateChanged;
    }

    private void OnDisable()
    {
        firebaseAuthenticator.StateChanged -= AuthStateChanged;
    }


    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (firebaseAuthenticator.CurrentUser != null && firebaseAuthenticator.CurrentUser.IsValid())
        {
            // کاربر از قبل لاگین شده است!
            // در اینجا صفحه "خروج از حساب" را نشان دهید
        }
        else
        {
            // کاربر لاگین نکرده است
            // صفحه "ورود/لاگین" را نشان دهید
        }
    }


    public void LogIn_WithEmail(string email, string password)
    {
        firebaseAuthenticator.SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    logInFail_Event?.Invoke();
                    return;
                }

                logInSuccess_Event?.Invoke();
            });
    }

    public void SignUp_WithEmail(string email, string password)
    {
        firebaseAuthenticator.CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    signUpFail_Event?.Invoke();
                    return;
                }
                if (task.IsFaulted)
                {
                    signUpFail_Event?.Invoke();

                    foreach (var e in task.Exception.Flatten().InnerExceptions)
                    {
                        FirebaseException firebaseEx = e as FirebaseException;
                        if (firebaseEx != null)
                        {
                            var errorCode = (AuthError)firebaseEx.ErrorCode;

                            switch (errorCode)
                            {
                                case AuthError.InvalidEmail:
                                    signUpInvalidEmail_Event?.Invoke();
                                    break;
                                case AuthError.WeakPassword:
                                    signUpWeakPassword_Event?.Invoke();
                                    break;
                                case AuthError.EmailAlreadyInUse:
                                    signUpEmailAlreadyInUse_Event?.Invoke();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    return;
                }

                signUpSuccess_Event?.Invoke();
            });
    }


    public void SignOut()
    {
        if (firebaseAuthenticator.CurrentUser != null)
        {
            firebaseAuthenticator.SignOut();
            signOutSuccess_Event?.Invoke();
        }
        else
        {
            Debug.Log("No user is currently logged in.");
        }
    }

    public void DeleteAccount(string email, string password)
    {
        SignOut();

        firebaseAuthenticator.SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    deleteAccountFail_Event?.Invoke();
                    return;
                }

                task.Result.User.DeleteAsync().ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCanceled || task.IsFaulted)
                        {
                            deleteAccountFail_Event?.Invoke();
                            return;
                        }

                        deleteAccountSuccess_Event?.Invoke();
                    });
            });
    }


    public void SendEmailVerification()
    {
        FirebaseUser user = firebaseAuthenticator.CurrentUser;

        if (user == null)
        {
            sendEmailVerificationFail_Event?.Invoke();
            return;
        }

        user.SendEmailVerificationAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    sendEmailVerificationFail_Event?.Invoke();
                    return;
                }

                sendEmailVerificationSuccess_Event?.Invoke();
            });
    }

    public void ResetPassword(string email)
    {
        firebaseAuthenticator.SendPasswordResetEmailAsync(email)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    resetPasswordFail_Event?.Invoke();
                    return;
                }
                if (task.IsFaulted)
                {
                    resetPasswordFail_Event?.Invoke();

                    var exception = task.Exception?.GetBaseException() as Firebase.FirebaseException;
                    if (exception != null)
                    {
                        var errorCode = (Firebase.Auth.AuthError)exception.ErrorCode;
                        switch (errorCode)
                        {
                            case Firebase.Auth.AuthError.UserNotFound:
                                userNotFound_Event?.Invoke();
                                break;
                            case Firebase.Auth.AuthError.InvalidEmail:
                                invalidEmail_Event?.Invoke();
                                break;
                        }
                    }
                    return;
                }

                resetPasswordSuccess_Event?.Invoke();
            });
    }







}

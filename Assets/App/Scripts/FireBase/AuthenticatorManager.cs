using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Auth;


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



    private void Awake()
    {
        firebaseAuthenticator = FirebaseAuth.DefaultInstance;
    }


    private void OnEnable()
    {
        if (firebaseAuthenticator != null)
            firebaseAuthenticator.StateChanged += AuthStateChanged;
    }


    private void OnDisable()
    {
        if (firebaseAuthenticator != null)
            firebaseAuthenticator.StateChanged -= AuthStateChanged;
    }



    private void AuthStateChanged(object sender, EventArgs eventArgs)
    {
        FirebaseUser user = firebaseAuthenticator.CurrentUser;


        if (user != null && user.IsValid())
        {
            Debug.Log($"User logged in : {user.Email}");
        }
        else
        {
            Debug.Log("No user logged in.");
        }
    }



    // ==========================
    // LOGIN
    // ==========================

    public async Task LogIn_WithEmail(string email, string password)
    {
        try
        {
            await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password);

            EventsManager.InvokeOnLoggedIn();
        }
        catch (Exception ex)
        {
            FirebaseException firebaseException = ex.GetBaseException() as FirebaseException;

            if (firebaseException == null)
            {
                //wrongEmailOrPassword
                return;
            }

            AuthError error = (AuthError)firebaseException.ErrorCode;

            switch (error)
            {
                case AuthError.UserNotFound:
                    break;

                case AuthError.WrongPassword:
                    break;

                case AuthError.InvalidEmail:
                    break;

                case AuthError.NetworkRequestFailed:
                    break;

                case AuthError.TooManyRequests:
                    break;

                case AuthError.OperationNotAllowed:
                    break;

                default:
                    break;
            }
        }
    }



    // ==========================
    // SIGN UP
    // ==========================

    public async Task SignUp_WithEmail(string email, string password)
    {
        try
        {
            await FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password);

            EventsManager.InvokeOnLoggedIn();
        }
        catch (Exception ex)
        {
            FirebaseException firebaseException = ex.GetBaseException() as FirebaseException;

            if (firebaseException == null)
            {
                return;
            }

            AuthError error = (AuthError)firebaseException.ErrorCode;

            switch (error)
            {
                case AuthError.InvalidEmail:
                    break;

                case AuthError.WeakPassword:
                    break;
                case AuthError.EmailAlreadyInUse:
                    break;

                case AuthError.NetworkRequestFailed:
                    break;

                case AuthError.TooManyRequests:
                    break;

                case AuthError.OperationNotAllowed:
                    break;

                default:
                    break;
            }
        }
    }



    // ==========================
    // SIGN OUT
    // ==========================

    public void SignOut()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
            EventsManager.InvokeOnLoggedOut();
        }
        //Else do else
    }




    // ==========================
    // DELETE ACCOUNT
    // ==========================

    public async Task DeleteAccount(string email, string password)
    {
        try
        {
            FirebaseUser user = firebaseAuthenticator.CurrentUser;


            if (user == null)
            {
                deleteAccountFail_Event?.Invoke();
                return;
            }



            Credential credential =
                EmailAuthProvider.GetCredential(email, password);



            await user.ReauthenticateAsync(credential);



            await user.DeleteAsync();



            deleteAccountSuccess_Event?.Invoke();
        }
        catch (Exception ex)
        {
            deleteAccountFail_Event?.Invoke();

            LogFirebaseException(ex);
        }
    }




    // ==========================
    // EMAIL VERIFICATION
    // ==========================

    public async Task SendEmailVerification()
    {
        try
        {
            FirebaseUser user = firebaseAuthenticator.CurrentUser;


            if (user == null)
            {
                sendEmailVerificationFail_Event?.Invoke();
                return;
            }



            await user.SendEmailVerificationAsync();



            sendEmailVerificationSuccess_Event?.Invoke();
        }
        catch (Exception ex)
        {
            sendEmailVerificationFail_Event?.Invoke();

            LogFirebaseException(ex);
        }
    }




    // ==========================
    // RESET PASSWORD
    // ==========================

    public async Task ResetPassword(string email)
    {
        try
        {
            await firebaseAuthenticator.SendPasswordResetEmailAsync(email);


            resetPasswordSuccess_Event?.Invoke();
        }
        catch (Exception ex)
        {
            resetPasswordFail_Event?.Invoke();


            AuthError? error = GetAuthError(ex);


            switch (error)
            {
                case AuthError.UserNotFound:
                    userNotFound_Event?.Invoke();
                    break;


                case AuthError.InvalidEmail:
                    invalidEmail_Event?.Invoke();
                    break;
            }


            LogFirebaseException(ex);
        }
    }




    // ==========================
    // ERROR HANDLING
    // ==========================


    private AuthError? GetAuthError(Exception ex)
    {
        FirebaseException firebaseException =
            ex.GetBaseException() as FirebaseException;


        if (firebaseException == null)
            return null;


        return (AuthError)firebaseException.ErrorCode;
    }



    private void LogFirebaseException(Exception ex)
    {
        Debug.LogException(ex);


        AuthError? error = GetAuthError(ex);


        if (error.HasValue)
        {
            Debug.Log($"Firebase Auth Error : {error.Value}");
        }
    }
}




/*
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.Events;
using Firebase;
using System.Threading.Tasks;

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
        //1
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

        //2
        Task task = firebaseAuthenticator.SignInWithEmailAndPasswordAsync(email, password);
        if (task.IsCanceled || task.IsFaulted)
        {
            logInFail_Event?.Invoke();
            return;
        }

        logInSuccess_Event?.Invoke();
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
*/
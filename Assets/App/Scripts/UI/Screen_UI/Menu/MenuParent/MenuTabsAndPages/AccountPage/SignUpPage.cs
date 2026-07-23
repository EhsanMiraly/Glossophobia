using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UIElements;


public class SignUpPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    AccountPage accountPage;

    VisualElement signUpPage_VisualElement;


    #region SignUpPage Parts
    Label signUp_Label;
    TextField username_TextField;
    Label username_TextField_Label;
    TextElement username_TextField_TextElement;
    TextField password_TextField;
    Label password_TextField_Label;
    TextElement password_TextField_TextElement;
    TextField repeatPassword_TextField;
    Label repeatPassword_TextField_Label;
    TextElement repeatPassword_TextField_TextElement;
    VisualElement goToLogInButton_TemplateContainer;
    Label goToLogInButton_Label;
    VisualElement signUpButton_TemplateContainer;
    Label signUpButton_Label;
    Label problems_Label;
    Label howTo_Label;
    #endregion


    #region TextFields
    string enteredUsername = "";
    string enteredPassword = "";
    string enteredRepeatPassword = "";
    #endregion



    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        accountPage = GetComponent<AccountPage>();
    }

    private void OnDisable()
    {
        RemoveFunctionality();
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }



    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        signUpPage_VisualElement = root.Q<VisualElement>("SignUpPage_VisualElement");

        signUp_Label = signUpPage_VisualElement.Q<Label>("SignUp_Label");

        username_TextField = signUpPage_VisualElement.Q<TextField>("Username_TextField");
        username_TextField_Label = (Label)username_TextField.Query<TextElement>().ToList()[0];
        username_TextField_TextElement = username_TextField.Query<TextElement>().ToList()[1];

        password_TextField = signUpPage_VisualElement.Q<TextField>("Password_TextField");
        password_TextField_Label = (Label)password_TextField.Query<TextElement>().ToList()[0];
        password_TextField_TextElement = password_TextField.Query<TextElement>().ToList()[1];

        repeatPassword_TextField = signUpPage_VisualElement.Q<TextField>("RepeatPassword_TextField");
        repeatPassword_TextField_Label = (Label)repeatPassword_TextField.Query<TextElement>().ToList()[0];
        repeatPassword_TextField_TextElement = repeatPassword_TextField.Query<TextElement>().ToList()[1];

        goToLogInButton_TemplateContainer =
            signUpPage_VisualElement.Q<VisualElement>("GoToLogInButton_TemplateContainer");
        goToLogInButton_Label = goToLogInButton_TemplateContainer.Q<Label>();
        signUpButton_TemplateContainer =
            signUpPage_VisualElement.Q<VisualElement>("SignUpButton_TemplateContainer");
        signUpButton_Label = signUpButton_TemplateContainer.Q<Label>();
        problems_Label = signUpPage_VisualElement.Q<Label>("Problems_Label");
        howTo_Label = signUpPage_VisualElement.Q<Label>("HowTo_Label");


        InitializeUI();
    }

    private void InitializeUI()
    {
        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();
    }


    #region Functionality

    private void AddFunctionality()
    {
        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;

        username_TextField.RegisterValueChangedCallback(OnUsernameValueChanged);
        password_TextField.RegisterValueChangedCallback(OnPasswordValueChanged);
        repeatPassword_TextField.RegisterValueChangedCallback(OnRepeatPasswordValueChanged);

        goToLogInButton_TemplateContainer.RegisterCallback<ClickEvent>(OnGoToLogInButtonSelected);
        signUpButton_TemplateContainer.RegisterCallback<ClickEvent>(OnSignUpButtonSelected);
    }

    private void RemoveFunctionality()
    {
        username_TextField.UnregisterValueChangedCallback(OnUsernameValueChanged);
        password_TextField.UnregisterValueChangedCallback(OnPasswordValueChanged);
        repeatPassword_TextField.UnregisterValueChangedCallback(OnRepeatPasswordValueChanged);

        goToLogInButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnGoToLogInButtonSelected);
        signUpButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnSignUpButtonSelected);

        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }


    private void OnUsernameValueChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue == "")
        {
            enteredUsername = changeEvent.newValue;
            return;
        }

        if (AccountData.isUsable(changeEvent.newValue[changeEvent.newValue.Length - 1]))
        {
            enteredUsername = changeEvent.newValue;
        }
        else
        {
            username_TextField.value = changeEvent.previousValue;
            problems_Label.text = LanguageTextsData.wrongCharacter[SettingsData.currentLanguageIndex];
        }

    }

    private void OnPasswordValueChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue == "")
        {
            enteredPassword = changeEvent.newValue;
            return;
        }

        if (AccountData.isUsable(changeEvent.newValue[changeEvent.newValue.Length - 1]))
        {
            enteredPassword = changeEvent.newValue;
        }
        else
        {
            password_TextField.value = changeEvent.previousValue;
            problems_Label.text = LanguageTextsData.wrongCharacter[SettingsData.currentLanguageIndex];
        }
    }

    private void OnRepeatPasswordValueChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue == "")
        {
            enteredRepeatPassword = changeEvent.newValue;
            return;
        }

        if (AccountData.isUsable(changeEvent.newValue[changeEvent.newValue.Length - 1]))
        {
            enteredRepeatPassword = changeEvent.newValue;
        }
        else
        {
            repeatPassword_TextField.value = changeEvent.previousValue;
            problems_Label.text = LanguageTextsData.wrongCharacter[SettingsData.currentLanguageIndex];
        }
    }


    private void OnGoToLogInButtonSelected(ClickEvent clickEvent)
    {
        accountPage.SetPageActive(accountPage.logInPage_VisualElement);
    }

    private async void OnSignUpButtonSelected(ClickEvent clickEvent)
    {
        if (enteredUsername.Length < 8)
        {
            problems_Label.text = LanguageTextsData.usernameLength[SettingsData.currentLanguageIndex];
            return;
        }
        else if (enteredPassword.Length < 8)
        {
            problems_Label.text = LanguageTextsData.passwordLength[SettingsData.currentLanguageIndex];
            return;
        }
        else if (enteredPassword != enteredRepeatPassword)
        {
            problems_Label.text = LanguageTextsData.passwordConfirmationPassword[SettingsData.currentLanguageIndex];
            return;
        }

        await FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(enteredUsername, enteredPassword)
           .ContinueWithOnMainThread(task =>
           {
               if (task.IsCanceled)
               {
                   //signUpFail_Event?.Invoke();
                   return;
               }
               if (task.IsFaulted)
               {
                   //signUpFail_Event?.Invoke();

                   foreach (var e in task.Exception.Flatten().InnerExceptions)
                   {
                       FirebaseException firebaseEx = e as FirebaseException;
                       if (firebaseEx != null)
                       {
                           var errorCode = (AuthError)firebaseEx.ErrorCode;

                           switch (errorCode)
                           {
                               case AuthError.InvalidEmail:
                                   Debug.Log("InValid Email");
                                   //signUpInvalidEmail_Event?.Invoke();
                                   break;
                               case AuthError.WeakPassword:
                                   Debug.Log("Weak Password");
                                   //signUpWeakPassword_Event?.Invoke();
                                   break;
                               case AuthError.EmailAlreadyInUse:
                                   Debug.Log("Email Already In Use");
                                   //signUpEmailAlreadyInUse_Event?.Invoke();
                                   break;
                               default:
                                   break;
                           }
                       }
                   }

                   return;
               }

               EventsManager.InvokeOnLoggedIn();
               accountPage.SetPageActive(accountPage.logOutPage_VisualElement);
           });
    }

    #endregion




    #region Events Manager

    private void OnLanguageChanged()
    {
        #region SignUp Label
        signUp_Label.text = LanguageTextsData.signUp[SettingsData.currentLanguageIndex];
        signUp_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        signUp_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region username_TextField_Label
        username_TextField.label = LanguageTextsData.enterUsername[SettingsData.currentLanguageIndex];
        username_TextField_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        username_TextField_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region username_TextField_TextElement
        username_TextField.value = "";
        username_TextField_TextElement.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        username_TextField_TextElement.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion


        #region password_TextField_Label
        password_TextField.label = LanguageTextsData.enterPassword[SettingsData.currentLanguageIndex];
        password_TextField_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        password_TextField_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region password_TextField_TextElement
        password_TextField.value = "";
        password_TextField_TextElement.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        password_TextField_TextElement.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region repeatPassword_TextField_Label
        repeatPassword_TextField.label =
            LanguageTextsData.enterRepeatPassword[SettingsData.currentLanguageIndex];
        repeatPassword_TextField_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        repeatPassword_TextField_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region repeatPassword_TextField_TextElement
        repeatPassword_TextField.value = "";
        repeatPassword_TextField_TextElement.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        repeatPassword_TextField_TextElement.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion


        #region goToLogInButton_Label
        goToLogInButton_Label.text = LanguageTextsData.logInPage[SettingsData.currentLanguageIndex];
        goToLogInButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        goToLogInButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion


        #region signUpButton_Label
        signUpButton_Label.text = LanguageTextsData.signUp[SettingsData.currentLanguageIndex];
        signUpButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        signUpButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion



        #region Problems Label
        problems_Label.text = "";
        problems_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        problems_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region HowTo Label
        howTo_Label.text = LanguageTextsData.howTo[SettingsData.currentLanguageIndex] +
            "\n" + AccountData.usableCharacters;
        howTo_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        howTo_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

    }

    private void OnFontSizeChanged()
    {
        #region Sign Up Label
        signUp_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryBig[SettingsData.currentFontSizeIndex];
        #endregion

        #region username_TextField_Label
        username_TextField_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region username_TextField_TextElement
        username_TextField_TextElement.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region password_TextField_Label
        password_TextField_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region password_TextField_TextElement
        password_TextField_TextElement.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region repeatPassword_TextField_Label
        repeatPassword_TextField_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region repeatPassword_TextField_TextElement
        repeatPassword_TextField_TextElement.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region goToLogInButton_Label
        goToLogInButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region signUpButton_Label
        signUpButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion


        #region Problems Label
        problems_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region HowTo Label
        howTo_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

    }

    #endregion

}

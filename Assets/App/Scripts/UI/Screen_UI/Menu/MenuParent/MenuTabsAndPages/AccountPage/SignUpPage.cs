using System;
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
    TextField password_TextField;
    TextField repeatPassword_TextField;
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

        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;
    }

    private void OnDisable()
    {
        RemoveFunctionality();
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);

        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }



    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        signUpPage_VisualElement = root.Q<VisualElement>("SignUpPage_VisualElement");

        signUp_Label = signUpPage_VisualElement.Q<Label>("SignUp_Label");
        username_TextField = signUpPage_VisualElement.Q<TextField>("Username_TextField");
        password_TextField = signUpPage_VisualElement.Q<TextField>("Password_TextField");
        repeatPassword_TextField = signUpPage_VisualElement.Q<TextField>("RepeatPassword_TextField");
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
    }


    private void OnUsernameValueChanged(ChangeEvent<string> changeEvent)
    {
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

    private void OnSignUpButtonSelected(ClickEvent clickEvent)
    {
        //Change To Fire Base Later
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

        AccountData.currentUsername = enteredUsername;
        AccountData.currentPassword = enteredPassword;

        Account_SaveSystem.Save_Account();

        accountPage.SetPageActive(accountPage.logOutPage_VisualElement);
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

        //Texts in text fields


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
            AccountData.usableCharacters;
        howTo_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        howTo_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

    }

    private void OnFontSizeChanged()
    {
        #region Log In Label
        signUp_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        //Texts in text fields

        #region goToLogInButton_Label
        goToLogInButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region signUpButton_Label
        signUpButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion


        #region Problems Label
        problems_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region HowTo Label
        howTo_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

    }

    #endregion

}

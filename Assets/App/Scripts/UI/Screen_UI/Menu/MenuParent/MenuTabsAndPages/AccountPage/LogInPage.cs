using System;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UIElements;



public class LogInPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    AccountPage accountPage;

    VisualElement logInPage_VisualElement;


    #region LogInPage Parts
    Label logIn_Label;
    TextField username_TextField;
    Label username_TextField_Label;
    TextElement username_TextField_TextElement;
    TextField password_TextField;
    Label password_TextField_Label;
    TextElement password_TextField_TextElement;
    VisualElement goToSignUpButton_TemplateContainer;
    Label goToSignUpButton_Label;
    VisualElement logInButton_TemplateContainer;
    Label logInButton_Label;
    Label problems_Label;
    #endregion


    #region TextFields
    string enteredUsername = "";
    string enteredPassword = "";
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
        logInPage_VisualElement = root.Q<VisualElement>("LogInPage_VisualElement");

        logIn_Label = logInPage_VisualElement.Q<Label>("LogIn_Label");

        username_TextField = logInPage_VisualElement.Q<TextField>("Username_TextField");
        username_TextField_Label = (Label)username_TextField.Query<TextElement>().ToList()[0];
        username_TextField_TextElement = username_TextField.Query<TextElement>().ToList()[1];

        password_TextField = logInPage_VisualElement.Q<TextField>("Password_TextField");
        password_TextField_Label = (Label)password_TextField.Query<TextElement>().ToList()[0];
        password_TextField_TextElement = password_TextField.Query<TextElement>().ToList()[1];

        goToSignUpButton_TemplateContainer =
            logInPage_VisualElement.Q<VisualElement>("GoToSignUpButton_TemplateContainer");
        goToSignUpButton_Label = goToSignUpButton_TemplateContainer.Q<Label>();
        logInButton_TemplateContainer =
            logInPage_VisualElement.Q<VisualElement>("LogInButton_TemplateContainer");
        logInButton_Label = logInButton_TemplateContainer.Q<Label>();
        problems_Label = logInPage_VisualElement.Q<Label>("Problems_Label");


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

        goToSignUpButton_TemplateContainer.RegisterCallback<ClickEvent>(OnGoToSignUpButtonSelected);
        logInButton_TemplateContainer.RegisterCallback<ClickEvent>(OnLogInButtonSelected);
    }

    private void RemoveFunctionality()
    {
        username_TextField.UnregisterValueChangedCallback(OnUsernameValueChanged);
        password_TextField.UnregisterValueChangedCallback(OnPasswordValueChanged);

        goToSignUpButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnGoToSignUpButtonSelected);
        logInButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnLogInButtonSelected);

        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }


    private void OnUsernameValueChanged(ChangeEvent<string> changeEvent)
    {
        enteredUsername = changeEvent.newValue;
    }

    private void OnPasswordValueChanged(ChangeEvent<string> changeEvent)
    {
        enteredPassword = changeEvent.newValue;
    }


    private void OnGoToSignUpButtonSelected(ClickEvent clickEvent)
    {
        accountPage.SetPageActive(accountPage.signUpPage_VisualElement);
    }

    private void OnLogInButtonSelected(ClickEvent clickEvent)
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(enteredUsername, enteredPassword)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    problems_Label.text =
                        LanguageTextsData.wrongUsernameOrPassword[SettingsData.currentLanguageIndex];
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
        #region Log In Label
        logIn_Label.text = LanguageTextsData.logIn[SettingsData.currentLanguageIndex];
        logIn_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        logIn_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region username_TextField_Label
        username_TextField_Label.text = LanguageTextsData.enterUsername[SettingsData.currentLanguageIndex];
        username_TextField_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        username_TextField_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region username_TextField_TextElement
        username_TextField_TextElement.text = "";
        username_TextField_TextElement.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        username_TextField_TextElement.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion


        #region password_TextField_Label
        password_TextField_Label.text = LanguageTextsData.enterPassword[SettingsData.currentLanguageIndex];
        password_TextField_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        password_TextField_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region password_TextField_TextElement
        password_TextField_TextElement.text = "";
        password_TextField_TextElement.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        password_TextField_TextElement.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region goToSignUpButton_Label
        goToSignUpButton_Label.text = LanguageTextsData.signUpPage[SettingsData.currentLanguageIndex];
        goToSignUpButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        goToSignUpButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region logInButton_Label
        logInButton_Label.text = LanguageTextsData.logIn[SettingsData.currentLanguageIndex];
        logInButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        logInButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Problems Label
        problems_Label.text = "";
        problems_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        problems_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region Log In Label
        logIn_Label.style.fontSize =
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


        //Texts in text fields

        #region goToSignUpButton_Label
        goToSignUpButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region logInButton_Label
        logInButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Problems Label
        problems_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion




}

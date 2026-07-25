using System;
using System.Collections.Generic;
using Firebase;
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
    TextField email_TextField;
    Label email_TextField_Label;
    TextElement email_TextField_TextElement;
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
    string enteredEmail = "";
    string enteredPassword = "";
    #endregion



    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        accountPage = GetComponent<AccountPage>();

        ConnectEvents();
    }

    private void OnDisable()
    {
        DisconnectEvents();

        RemoveFunctionality();

        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        logInPage_VisualElement = root.Q<VisualElement>("LogInPage_VisualElement");

        logIn_Label = logInPage_VisualElement.Q<Label>("LogIn_Label");

        email_TextField = logInPage_VisualElement.Q<TextField>("Email_TextField");
        email_TextField_Label = (Label)email_TextField.Query<TextElement>().ToList()[0];
        email_TextField_TextElement = email_TextField.Query<TextElement>().ToList()[1];

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


        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();
    }


    #region Functionality

    private void AddFunctionality()
    {
        email_TextField.RegisterValueChangedCallback(OnEmailValueChanged);
        password_TextField.RegisterValueChangedCallback(OnPasswordValueChanged);

        goToSignUpButton_TemplateContainer.RegisterCallback<ClickEvent>(OnGoToSignUpButtonSelected);
        logInButton_TemplateContainer.RegisterCallback<ClickEvent>(OnLogInButtonSelected);
    }

    private void RemoveFunctionality()
    {
        email_TextField.UnregisterValueChangedCallback(OnEmailValueChanged);
        password_TextField.UnregisterValueChangedCallback(OnPasswordValueChanged);

        goToSignUpButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnGoToSignUpButtonSelected);
        logInButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnLogInButtonSelected);
    }


    private void OnEmailValueChanged(ChangeEvent<string> changeEvent)
    {
        enteredEmail = changeEvent.newValue;
    }

    private void OnPasswordValueChanged(ChangeEvent<string> changeEvent)
    {
        enteredPassword = changeEvent.newValue;
    }

    private void OnGoToSignUpButtonSelected(ClickEvent clickEvent)
    {
        accountPage.SetPageActive(accountPage.signUpPage_VisualElement);
    }

    private async void OnLogInButtonSelected(ClickEvent clickEvent)
    {
        logInButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnLogInButtonSelected);

        try
        {
            await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(enteredEmail, enteredPassword);

            EventsManager.InvokeOnLoggedIn();
            accountPage.SetPageActive(accountPage.logOutPage_VisualElement);
        }
        catch (Exception ex)
        {
            FirebaseException firebaseException = ex.GetBaseException() as FirebaseException;

            if (firebaseException == null)
            {
                problems_Label.text = LanguageTextsData.wrongEmailOrPassword[SettingsData.currentLanguageIndex];
                return;
            }

            AuthError error = (AuthError)firebaseException.ErrorCode;

            switch (error)
            {
                case AuthError.UserNotFound:
                    problems_Label.text =
                        LanguageTextsData.wrongEmailOrPassword[SettingsData.currentLanguageIndex];
                    break;

                case AuthError.WrongPassword:
                    problems_Label.text =
                        LanguageTextsData.wrongEmailOrPassword[SettingsData.currentLanguageIndex];
                    break;

                case AuthError.InvalidEmail:
                    problems_Label.text =
                        LanguageTextsData.invalidEmail[SettingsData.currentLanguageIndex];
                    break;

                case AuthError.NetworkRequestFailed:
                    problems_Label.text =
                        LanguageTextsData.networkRequestFailed[SettingsData.currentLanguageIndex];
                    break;

                case AuthError.TooManyRequests:
                    problems_Label.text =
                        LanguageTextsData.tooManyRequests[SettingsData.currentLanguageIndex];
                    break;

                case AuthError.OperationNotAllowed:
                    problems_Label.text =
                        LanguageTextsData.operationNotAllowed[SettingsData.currentLanguageIndex];
                    break;

                default:
                    problems_Label.text =
                        LanguageTextsData.unknownError[SettingsData.currentLanguageIndex];
                    break;
            }
        }

        logInButton_TemplateContainer.RegisterCallback<ClickEvent>(OnLogInButtonSelected);
    }

    #endregion




    #region Events Manager

    private void ConnectEvents()
    {
        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;
    }

    private void DisconnectEvents()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }

    private void OnLanguageChanged()
    {
        #region Log In Label
        logIn_Label.text = LanguageTextsData.logIn[SettingsData.currentLanguageIndex];
        logIn_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        logIn_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region email_TextField_Label
        email_TextField_Label.text = LanguageTextsData.enterEmail[SettingsData.currentLanguageIndex];
        email_TextField_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        email_TextField_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region email_TextField_TextElement
        email_TextField_TextElement.text = "";
        email_TextField_TextElement.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        email_TextField_TextElement.style.unityFont =
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

        #region email_TextField_Label
        email_TextField_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region email_TextField_TextElement
        email_TextField_TextElement.style.fontSize =
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

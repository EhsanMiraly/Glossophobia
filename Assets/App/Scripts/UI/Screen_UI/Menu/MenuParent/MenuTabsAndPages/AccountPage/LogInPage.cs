using System;
using UnityEngine;
using UnityEngine.UIElements;



public class LogInPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    AccountPage accountPage;

    VisualElement logInPage_VisualElement;


    /*
    string label = textField.label;
    textField.label = "Username";
    textField.textEdition.placeholder = "Enter your username";
    textField.textEdition.hidePlaceholderOnFocus = true;
    string value = textField.value;
    textField.value = "Ali";
    Label label = textField.Q<Label>();
    VisualElement input = textField.Q("unity-text-input");
    TextElement text = textField.Q<TextElement>();
    text.style.color = Color.green;
    input.style.backgroundColor = Color.black;
    */

    #region LogInPage Parts
    Label logIn_Label;
    TextField username_TextField;
    TextField password_TextField;
    VisualElement goToSignUpButton_TemplateContainer;
    VisualElement logInButton_TemplateContainer;
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
        logInPage_VisualElement = root.Q<VisualElement>("LogInPage_VisualElement");

        logIn_Label = logInPage_VisualElement.Q<Label>("LogIn_Label");
        username_TextField = logInPage_VisualElement.Q<TextField>("Username_TextField");
        password_TextField = logInPage_VisualElement.Q<TextField>("Password_TextField");
        goToSignUpButton_TemplateContainer =
            logInPage_VisualElement.Q<VisualElement>("GoToSignUpButton_TemplateContainer");
        logInButton_TemplateContainer =
            logInPage_VisualElement.Q<VisualElement>("LogInButton_TemplateContainer");
        problems_Label = logInPage_VisualElement.Q<Label>("Problems_Label");



        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();
    }


    #region Functionality

    private void AddFunctionality()
    {
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
        if (enteredUsername != "" && enteredPassword != "")
        {
            Debug.Log(enteredUsername + " - " + enteredPassword);
        }
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
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region Problems Label
        problems_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion


    }

    #endregion




}

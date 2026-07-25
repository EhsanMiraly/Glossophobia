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
    TextField email_TextField;
    Label email_TextField_Label;
    TextElement email_TextField_TextElement;
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
    #endregion


    #region TextFields
    string enteredEmail = "";
    string enteredPassword = "";
    string enteredRepeatPassword = "";
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
        signUpPage_VisualElement = root.Q<VisualElement>("SignUpPage_VisualElement");

        signUp_Label = signUpPage_VisualElement.Q<Label>("SignUp_Label");

        email_TextField = signUpPage_VisualElement.Q<TextField>("Email_TextField");
        email_TextField_Label = (Label)email_TextField.Query<TextElement>().ToList()[0];
        email_TextField_TextElement = email_TextField.Query<TextElement>().ToList()[1];

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


        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();
    }


    #region Functionality

    private void AddFunctionality()
    {
        email_TextField.RegisterValueChangedCallback(OnEmailValueChanged);
        password_TextField.RegisterValueChangedCallback(OnPasswordValueChanged);
        repeatPassword_TextField.RegisterValueChangedCallback(OnRepeatPasswordValueChanged);

        goToLogInButton_TemplateContainer.RegisterCallback<ClickEvent>(OnGoToLogInButtonSelected);
        signUpButton_TemplateContainer.RegisterCallback<ClickEvent>(OnSignUpButtonSelected);
    }

    private void RemoveFunctionality()
    {
        email_TextField.UnregisterValueChangedCallback(OnEmailValueChanged);
        password_TextField.UnregisterValueChangedCallback(OnPasswordValueChanged);
        repeatPassword_TextField.UnregisterValueChangedCallback(OnRepeatPasswordValueChanged);

        goToLogInButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnGoToLogInButtonSelected);
        signUpButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnSignUpButtonSelected);
    }


    private void OnEmailValueChanged(ChangeEvent<string> changeEvent)
    {
        enteredEmail = changeEvent.newValue;
    }

    private void OnPasswordValueChanged(ChangeEvent<string> changeEvent)
    {
        enteredPassword = changeEvent.newValue;
    }

    private void OnRepeatPasswordValueChanged(ChangeEvent<string> changeEvent)
    {
        enteredRepeatPassword = changeEvent.newValue;
    }


    private void OnGoToLogInButtonSelected(ClickEvent clickEvent)
    {
        accountPage.SetPageActive(accountPage.logInPage_VisualElement);
    }

    private async void OnSignUpButtonSelected(ClickEvent clickEvent)
    {
        signUpButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnSignUpButtonSelected);

        if (enteredPassword != enteredRepeatPassword)
        {
            problems_Label.text = LanguageTextsData.passwordConfirmationPassword[SettingsData.currentLanguageIndex];
            signUpButton_TemplateContainer.RegisterCallback<ClickEvent>(OnSignUpButtonSelected);
            return;
        }

        try
        {
            await FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(enteredEmail, enteredPassword);

            EventsManager.InvokeOnLoggedIn();
            accountPage.SetPageActive(accountPage.logOutPage_VisualElement);
        }
        catch (Exception ex)
        {
            FirebaseException firebaseException = ex.GetBaseException() as FirebaseException;

            if (firebaseException == null)
            {
                problems_Label.text = LanguageTextsData.unknownError[SettingsData.currentLanguageIndex];
                return;
            }

            AuthError error = (AuthError)firebaseException.ErrorCode;

            switch (error)
            {
                case AuthError.InvalidEmail:
                    problems_Label.text = LanguageTextsData.invalidEmail[SettingsData.currentLanguageIndex];
                    break;

                case AuthError.WeakPassword:
                    problems_Label.text = LanguageTextsData.weakPassword[SettingsData.currentLanguageIndex];
                    break;
                case AuthError.EmailAlreadyInUse:
                    problems_Label.text = LanguageTextsData.emailAlreadyInUse[SettingsData.currentLanguageIndex];
                    break;

                case AuthError.NetworkRequestFailed:
                    problems_Label.text = LanguageTextsData.networkRequestFailed[SettingsData.currentLanguageIndex];
                    break;

                case AuthError.TooManyRequests:
                    problems_Label.text = LanguageTextsData.tooManyRequests[SettingsData.currentLanguageIndex];
                    break;

                case AuthError.OperationNotAllowed:
                    problems_Label.text = LanguageTextsData.operationNotAllowed[SettingsData.currentLanguageIndex];
                    break;

                default:
                    problems_Label.text = LanguageTextsData.unknownError[SettingsData.currentLanguageIndex];
                    break;
            }
        }

        signUpButton_TemplateContainer.RegisterCallback<ClickEvent>(OnSignUpButtonSelected);
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
        #region SignUp Label
        signUp_Label.text = LanguageTextsData.signUp[SettingsData.currentLanguageIndex];
        signUp_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        signUp_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region email_TextField_Label
        email_TextField.label = LanguageTextsData.enterEmail[SettingsData.currentLanguageIndex];
        email_TextField_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        email_TextField_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region email_TextField_TextElement
        email_TextField.value = "";
        email_TextField_TextElement.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        email_TextField_TextElement.style.unityFont =
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
    }

    private void OnFontSizeChanged()
    {
        #region Sign Up Label
        signUp_Label.style.fontSize =
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
    }

    #endregion

}

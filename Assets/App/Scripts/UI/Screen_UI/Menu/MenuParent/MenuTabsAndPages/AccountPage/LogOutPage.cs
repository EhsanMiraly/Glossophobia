using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;


public class LogOutPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    AccountPage accountPage;

    VisualElement logOutPage_VisualElement;


    #region LogOutPage Parts
    Label youAreSignedInAs_Label;
    Label username_Label;
    VisualElement logOutButton_TemplateContainer;
    Label logOutButton_Label;
    #endregion

    private bool isUIReady = false;


    private void OnEnable()
    {
        ConnectEvents();

        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        accountPage = GetComponent<AccountPage>();
    }

    private void OnDisable()
    {
        RemoveFunctionality();
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);

        DisconnectEvents();
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        logOutPage_VisualElement = root.Q<VisualElement>("LogOutPage_VisualElement");

        youAreSignedInAs_Label = logOutPage_VisualElement.Q<Label>("YouAreSignedInAs_Label");
        username_Label = logOutPage_VisualElement.Q<Label>("Username_Label");
        logOutButton_TemplateContainer = logOutPage_VisualElement.
            Q<VisualElement>("LogOutButton_TemplateContainer");
        logOutButton_Label = logOutButton_TemplateContainer.Q<Label>();
        Fix_LogOutButton_Dimentions(logOutButton_TemplateContainer);

        InitializeUI();
    }

    private void InitializeUI()
    {
        isUIReady = true;

        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();
    }


    #region UI Utilities

    private void Fix_LogOutButton_Dimentions(VisualElement logOutButton)
    {
        logOutButton.style.width = Length.Percent(30);
        logOutButton.style.height = Screen.width / 25f;
    }

    #endregion


    #region Functionality

    private void AddFunctionality()
    {
        logOutButton_TemplateContainer.RegisterCallback<ClickEvent>(OnLogOutButtonSelected);
    }

    private void RemoveFunctionality()
    {
        logOutButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnLogOutButtonSelected);
    }

    private void OnLogOutButtonSelected(ClickEvent clickEvent)
    {
        EventsManager.InvokeOnLoggedOut();
        accountPage.SetPageActive(accountPage.logInPage_VisualElement);
    }

    #endregion



    #region Events Manager

    private void ConnectEvents()
    {
        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;

        EventsManager.OnLoggedIn_Event += OnLoggedIn;
    }

    private void DisconnectEvents()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;

        EventsManager.OnLoggedIn_Event -= OnLoggedIn;
    }

    private void OnLanguageChanged()
    {
        #region You Are Signed In As Label
        youAreSignedInAs_Label.text = LanguageTextsData.youAreSignedInAs[SettingsData.currentLanguageIndex];
        youAreSignedInAs_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        youAreSignedInAs_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region username_Label
        username_Label.text = AccountData.currentUsername;
        username_Label.languageDirection =
            LanguageTextsData.languages[0].languageDirection;
        username_Label.style.unityFont =
            LanguageTextsData.languages[0].font;
        #endregion

        #region logOutButton_Label
        logOutButton_Label.text = LanguageTextsData.logOut[SettingsData.currentLanguageIndex];
        logOutButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        logOutButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region You Are Signed In As Label
        youAreSignedInAs_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region username_Label
        username_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryBig[SettingsData.currentFontSizeIndex];
        #endregion

        #region logOutButton_Label
        logOutButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }


    private async void OnLoggedIn()
    {
        while (!isUIReady)
        {
            await Awaitable.WaitForSecondsAsync(0.1f);
        }

        username_Label.text = AccountData.currentUsername;
    }

    #endregion




}

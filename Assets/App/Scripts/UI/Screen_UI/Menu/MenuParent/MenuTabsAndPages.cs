using System;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(AccountPage), typeof(DemographicsPage), typeof(BaselinePRPSAPage))]
[RequireComponent(typeof(SettingsPage), typeof(StartPlayingPage), typeof(PostTestPRPSAPage))]
public class MenuTabsAndPages : MonoBehaviour
{
    PanelRenderer panelRenderer;
    MenuParent menuParent;

    VisualElement menuTabsAndPages_VisualElement;
    VisualElement tabsHolder_VisualElement;
    VisualElement pagesHolder_VisualElement;


    #region Tabs
    Label account_Label;
    Label demographics_Label;
    Label baselinePRPSA_Label;
    Label startPlaying_Label;
    Label settings_Label;
    Label postTestPRPSA_Label;

    Label currentTabSelected;
    #endregion


    #region Pages
    [System.NonSerialized] public VisualElement accountPage_VisualElement;
    [System.NonSerialized] public VisualElement demographicsPage_VisualElement;
    [System.NonSerialized] public VisualElement baselinePRPSAPage_VisualElement;
    [System.NonSerialized] public VisualElement startPlayingPage_VisualElement;
    [System.NonSerialized] public VisualElement settingsPage_VisualElement;
    [System.NonSerialized] public VisualElement postTestPRPSAPage_VisualElement;
    #endregion

    private bool isUIReady = false;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        menuParent = GetComponent<MenuParent>();

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
        menuTabsAndPages_VisualElement = root.Q<VisualElement>("MenuTabsAndPages_VisualElement");
        tabsHolder_VisualElement = menuTabsAndPages_VisualElement.Q<VisualElement>("TabsHolder_VisualElement");
        pagesHolder_VisualElement = menuTabsAndPages_VisualElement.Q<VisualElement>("PagesHolder_VisualElement");

        #region Tabs
        account_Label = tabsHolder_VisualElement.Q<Label>("Account_Label");
        demographics_Label = tabsHolder_VisualElement.Q<Label>("Demographics_Label"); //Blur Tab
        baselinePRPSA_Label = tabsHolder_VisualElement.Q<Label>("BaselinePRPSA_Label");
        startPlaying_Label = tabsHolder_VisualElement.Q<Label>("StartPlaying_Label");
        settings_Label = tabsHolder_VisualElement.Q<Label>("Settings_Label");
        postTestPRPSA_Label = tabsHolder_VisualElement.Q<Label>("PostTestPRPSA_Label");
        #endregion

        #region Pages
        accountPage_VisualElement = pagesHolder_VisualElement.Q<VisualElement>("AccountPage_VisualElement");
        demographicsPage_VisualElement =
            pagesHolder_VisualElement.Q<VisualElement>("DemographicsPage_VisualElement");
        baselinePRPSAPage_VisualElement =
            pagesHolder_VisualElement.Q<VisualElement>("BaselinePRPSAPage_VisualElement");
        startPlayingPage_VisualElement =
            pagesHolder_VisualElement.Q<VisualElement>("StartPlayingPage_VisualElement");
        settingsPage_VisualElement = pagesHolder_VisualElement.Q<VisualElement>("SettingsPage_VisualElement");
        postTestPRPSAPage_VisualElement =
            pagesHolder_VisualElement.Q<VisualElement>("PostTestPRPSAPage_VisualElement");
        #endregion

        isUIReady = true;


        InitializeUI();
    }

    private void InitializeUI()
    {
        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();

        currentTabSelected = account_Label;
        SetTabActive(accountPage_VisualElement);
        account_Label.RemoveFromClassList("TabNotSelected");
        account_Label.AddToClassList("TabSelected");
    }



    #region Functionality

    private void AddFunctionality()
    {
        account_Label.RegisterCallback<ClickEvent>(OnAccountTabSelected);

        settings_Label.RegisterCallback<ClickEvent>(OnSettingsTabSelected);
    }

    private void RemoveFunctionality()
    {
        account_Label.UnregisterCallback<ClickEvent>(OnAccountTabSelected);

        settings_Label.UnregisterCallback<ClickEvent>(OnSettingsTabSelected);
    }


    private void OnAccountTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(accountPage_VisualElement);
        account_Label.RemoveFromClassList("TabNotSelected");
        account_Label.AddToClassList("TabSelected");
        currentTabSelected = account_Label;
    }

    private void OnDemographicsTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(demographicsPage_VisualElement);
        demographics_Label.RemoveFromClassList("TabNotSelected");
        demographics_Label.AddToClassList("TabSelected");
        currentTabSelected = demographics_Label;
    }

    private void OnBaselinePRPSATabSelected(ClickEvent clickEvent)
    {
        SetTabActive(baselinePRPSAPage_VisualElement);
        baselinePRPSA_Label.RemoveFromClassList("TabNotSelected");
        baselinePRPSA_Label.AddToClassList("TabSelected");
        currentTabSelected = baselinePRPSA_Label;
    }

    private void OnSettingsTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(settingsPage_VisualElement);
        settings_Label.RemoveFromClassList("TabNotSelected");
        settings_Label.AddToClassList("TabSelected");
        currentTabSelected = settings_Label;

    }

    private void OnStartPlayingTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(startPlayingPage_VisualElement);
        startPlaying_Label.RemoveFromClassList("TabNotSelected");
        startPlaying_Label.AddToClassList("TabSelected");
        currentTabSelected = startPlaying_Label;
    }

    private void OnPostTestPRPSATabSelected(ClickEvent clickEvent)
    {
        SetTabActive(postTestPRPSAPage_VisualElement);
        postTestPRPSA_Label.RemoveFromClassList("TabNotSelected");
        postTestPRPSA_Label.AddToClassList("TabSelected");
        currentTabSelected = postTestPRPSA_Label;
    }

    #endregion


    #region Events Manager

    private void ConnectEvents()
    {
        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;

        EventsManager.OnLoggedIn_Event += OnLoggedIn;
        EventsManager.OnLoggedOut_Event += OnLoggedOut;

        EventsManager.OnSetDemographics_Event += OnSetDemographics;
        EventsManager.OnChangeDemographics_Event += OnChangedDemographics;

        EventsManager.OnSetPRPSA_Before_Event += OnSetBaselinePRPSA;
        EventsManager.OnChangePRPSA_Before_Event += OnChangedBaselinePRPSA;

        EventsManager.OnSimulationEnded_Event += OnSimulationEnded;

        EventsManager.OnFinishedPostTestPRPSA_Event += OnFinishedPostTestPRPSA;
    }

    private void DisconnectEvents()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;

        EventsManager.OnLoggedIn_Event -= OnLoggedIn;
        EventsManager.OnLoggedOut_Event -= OnLoggedOut;

        EventsManager.OnSetDemographics_Event -= OnSetDemographics;
        EventsManager.OnChangeDemographics_Event -= OnChangedDemographics;

        EventsManager.OnSetPRPSA_Before_Event -= OnSetBaselinePRPSA;
        EventsManager.OnChangePRPSA_Before_Event -= OnChangedBaselinePRPSA;

        EventsManager.OnSimulationEnded_Event -= OnSimulationEnded;

        EventsManager.OnFinishedPostTestPRPSA_Event -= OnFinishedPostTestPRPSA;
    }


    private void OnLanguageChanged()
    {
        #region Account_Label
        account_Label.text = LanguageTextsData.account[SettingsData.currentLanguageIndex];
        account_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        account_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region demographics_Label
        demographics_Label.text = LanguageTextsData.demographics[SettingsData.currentLanguageIndex];
        demographics_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        demographics_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region PRPSA_Label
        baselinePRPSA_Label.text = LanguageTextsData.baselinePRPSA[SettingsData.currentLanguageIndex];
        baselinePRPSA_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        baselinePRPSA_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region startPlaying_Label
        startPlaying_Label.text = LanguageTextsData.startPlaying[SettingsData.currentLanguageIndex];
        startPlaying_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        startPlaying_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region postTestPRPSA_Label
        postTestPRPSA_Label.text = LanguageTextsData.postTestPRPSA[SettingsData.currentLanguageIndex];
        postTestPRPSA_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        postTestPRPSA_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Settings_Label
        settings_Label.text = LanguageTextsData.settings[SettingsData.currentLanguageIndex];
        settings_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        settings_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion


    }

    private void OnFontSizeChanged()
    {
        #region Account_Label
        account_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region demographics_Label
        demographics_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region PRPSA_Label
        baselinePRPSA_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region startPlaying_Label
        startPlaying_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region postTestPRPSA_Label
        postTestPRPSA_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Settings_Label
        settings_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion


    }


    #region LogIn/LogOut
    private async void OnLoggedIn()
    {
        while (!isUIReady)
        {
            await Awaitable.EndOfFrameAsync();
        }

        demographics_Label.RegisterCallback<ClickEvent>(OnDemographicsTabSelected);
    }

    private void OnLoggedOut()
    {
        demographics_Label.UnregisterCallback<ClickEvent>(OnDemographicsTabSelected);
        baselinePRPSA_Label.UnregisterCallback<ClickEvent>(OnBaselinePRPSATabSelected);
        startPlaying_Label.UnregisterCallback<ClickEvent>(OnStartPlayingTabSelected);
        postTestPRPSA_Label.UnregisterCallback<ClickEvent>(OnPostTestPRPSATabSelected);
    }
    #endregion


    #region Demographics
    private async void OnSetDemographics()
    {
        while (!isUIReady)
        {
            await Awaitable.EndOfFrameAsync();
        }

        baselinePRPSA_Label.RegisterCallback<ClickEvent>(OnBaselinePRPSATabSelected);
    }

    private void OnChangedDemographics()
    {
        baselinePRPSA_Label.UnregisterCallback<ClickEvent>(OnBaselinePRPSATabSelected);
        startPlaying_Label.UnregisterCallback<ClickEvent>(OnStartPlayingTabSelected);
        postTestPRPSA_Label.UnregisterCallback<ClickEvent>(OnPostTestPRPSATabSelected);
    }
    #endregion


    #region BaselinePRPSA
    private async void OnSetBaselinePRPSA()
    {
        while (!isUIReady)
        {
            await Awaitable.EndOfFrameAsync();
        }

        startPlaying_Label.RegisterCallback<ClickEvent>(OnStartPlayingTabSelected);
    }

    private void OnChangedBaselinePRPSA()
    {
        startPlaying_Label.UnregisterCallback<ClickEvent>(OnStartPlayingTabSelected);
        postTestPRPSA_Label.UnregisterCallback<ClickEvent>(OnPostTestPRPSATabSelected);
    }
    #endregion


    #region Simulation

    private async void OnSimulationEnded()
    {
        while (!isUIReady)
        {
            await Awaitable.EndOfFrameAsync();
        }

        postTestPRPSA_Label.RegisterCallback<ClickEvent>(OnPostTestPRPSATabSelected);
        OnPostTestPRPSATabSelected(new ClickEvent());
    }

    #endregion


    #region PostTestPRPSA

    private async void OnFinishedPostTestPRPSA()
    {
        while (!isUIReady)
        {
            await Awaitable.EndOfFrameAsync();
        }

        postTestPRPSA_Label.UnregisterCallback<ClickEvent>(OnPostTestPRPSATabSelected);
    }

    #endregion


    #endregion


    public void SetTabActive(VisualElement visualElement)
    {
        currentTabSelected.RemoveFromClassList("TabSelected");
        currentTabSelected.AddToClassList("TabNotSelected");

        accountPage_VisualElement.style.display = DisplayStyle.None;
        demographicsPage_VisualElement.style.display = DisplayStyle.None;
        baselinePRPSAPage_VisualElement.style.display = DisplayStyle.None;
        startPlayingPage_VisualElement.style.display = DisplayStyle.None;
        settingsPage_VisualElement.style.display = DisplayStyle.None;
        postTestPRPSAPage_VisualElement.style.display = DisplayStyle.None;

        visualElement.style.display = DisplayStyle.Flex;
    }

}

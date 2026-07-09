using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(AccountPage), typeof(DemographicsPage), typeof(PRPSAPage))]
[RequireComponent(typeof(StartPlayingPage), typeof(SettingsPage))]
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
    Label PRPSA_Label;
    Label startPlaying_Label;
    Label settings_Label;

    Label currentTabSelected;
    #endregion


    #region Pages
    [System.NonSerialized] public VisualElement accountPage_VisualElement;
    [System.NonSerialized] public VisualElement demographicsPage_VisualElement;
    [System.NonSerialized] public VisualElement PRPSAPage_VisualElement;
    [System.NonSerialized] public VisualElement startPlayingPage_VisualElement;
    [System.NonSerialized] public VisualElement settingsPage_VisualElement;
    #endregion



    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        menuParent = GetComponent<MenuParent>();

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
        menuTabsAndPages_VisualElement = root.Q<VisualElement>("MenuTabsAndPages_VisualElement");
        tabsHolder_VisualElement = menuTabsAndPages_VisualElement.Q<VisualElement>("TabsHolder_VisualElement");
        pagesHolder_VisualElement = menuTabsAndPages_VisualElement.Q<VisualElement>("PagesHolder_VisualElement");

        #region Tabs
        account_Label = tabsHolder_VisualElement.Q<Label>("Account_Label");
        demographics_Label = tabsHolder_VisualElement.Q<Label>("Demographics_Label");
        PRPSA_Label = tabsHolder_VisualElement.Q<Label>("PRPSA_Label");
        startPlaying_Label = tabsHolder_VisualElement.Q<Label>("StartPlaying_Label");
        settings_Label = tabsHolder_VisualElement.Q<Label>("Settings_Label");
        #endregion

        #region Pages
        accountPage_VisualElement = pagesHolder_VisualElement.Q<VisualElement>("AccountPage_VisualElement");
        demographicsPage_VisualElement =
            pagesHolder_VisualElement.Q<VisualElement>("DemographicsPage_VisualElement");
        PRPSAPage_VisualElement = pagesHolder_VisualElement.Q<VisualElement>("PRPSAPage_VisualElement");
        startPlayingPage_VisualElement =
            pagesHolder_VisualElement.Q<VisualElement>("StartPlayingPage_VisualElement");
        settingsPage_VisualElement = pagesHolder_VisualElement.Q<VisualElement>("SettingsPage_VisualElement");
        #endregion


        InitializeUI();
    }

    private void InitializeUI()
    {
        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();

        currentTabSelected = account_Label;
        SetTabActive(accountPage_VisualElement);
        account_Label.AddToClassList("TabSelected");
    }



    #region Functionality

    private void AddFunctionality()
    {
        account_Label.RegisterCallback<ClickEvent>(OnAccountTabSelected);
        demographics_Label.RegisterCallback<ClickEvent>(OnDemographicsTabSelected);
        PRPSA_Label.RegisterCallback<ClickEvent>(OnPRPSATabSelected);
        startPlaying_Label.RegisterCallback<ClickEvent>(OnStartPlayingTabSelected);
        settings_Label.RegisterCallback<ClickEvent>(OnSettingsTabSelected);
    }

    private void RemoveFunctionality()
    {
        account_Label.UnregisterCallback<ClickEvent>(OnAccountTabSelected);
        demographics_Label.UnregisterCallback<ClickEvent>(OnDemographicsTabSelected);
        PRPSA_Label.UnregisterCallback<ClickEvent>(OnPRPSATabSelected);
        startPlaying_Label.UnregisterCallback<ClickEvent>(OnStartPlayingTabSelected);
        settings_Label.UnregisterCallback<ClickEvent>(OnSettingsTabSelected);
    }


    private void OnAccountTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(accountPage_VisualElement);
        account_Label.AddToClassList("TabSelected");
        currentTabSelected = account_Label;
    }

    private void OnDemographicsTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(demographicsPage_VisualElement);
        demographics_Label.AddToClassList("TabSelected");
        currentTabSelected = demographics_Label;
    }

    private void OnPRPSATabSelected(ClickEvent clickEvent)
    {
        SetTabActive(PRPSAPage_VisualElement);
        PRPSA_Label.AddToClassList("TabSelected");
        currentTabSelected = PRPSA_Label;
    }

    private void OnStartPlayingTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(startPlayingPage_VisualElement);
        startPlaying_Label.AddToClassList("TabSelected");
        currentTabSelected = startPlaying_Label;
    }

    private void OnSettingsTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(settingsPage_VisualElement);
        settings_Label.AddToClassList("TabSelected");
        currentTabSelected = settings_Label;

    }

    #endregion


    #region Events Manager

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
        PRPSA_Label.text = LanguageTextsData.PRPSA[SettingsData.currentLanguageIndex];
        PRPSA_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        PRPSA_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region startPlaying_Label
        startPlaying_Label.text = LanguageTextsData.startPlaying[SettingsData.currentLanguageIndex];
        startPlaying_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        startPlaying_Label.style.unityFont =
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
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region demographics_Label
        demographics_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region PRPSA_Label
        PRPSA_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region startPlaying_Label
        startPlaying_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region Settings_Label
        settings_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion


    }

    #endregion


    public void SetTabActive(VisualElement visualElement)
    {
        currentTabSelected.RemoveFromClassList("TabSelected");

        accountPage_VisualElement.style.display = DisplayStyle.None;
        demographicsPage_VisualElement.style.display = DisplayStyle.None;
        PRPSAPage_VisualElement.style.display = DisplayStyle.None;
        startPlayingPage_VisualElement.style.display = DisplayStyle.None;
        settingsPage_VisualElement.style.display = DisplayStyle.None;

        visualElement.style.display = DisplayStyle.Flex;
    }

}

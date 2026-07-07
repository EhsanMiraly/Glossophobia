using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(AccountPage), typeof(SettingsPage), typeof(StartPlayingPage))]
public class MenuTabsAndPages : MonoBehaviour
{
    PanelRenderer panelRenderer;
    MenuParent menuParent;

    VisualElement menuTabsAndPages_VisualElement;
    VisualElement tabsHolder_VisualElement;
    VisualElement pagesHolder_VisualElement;


    #region Tabs
    Label account_Label;
    Label startPlaying_Label;
    //3 Tab Remains
    Label settings_Label;

    Label currentTabSelected;
    #endregion


    #region Pages
    [System.NonSerialized] public VisualElement accountPage_VisualElement;
    [System.NonSerialized] public VisualElement startPlayingPage_VisualElement;
    //3 Page Remains
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
        startPlaying_Label = tabsHolder_VisualElement.Q<Label>("StartPlaying_Label");
        //3 Tab Remains
        settings_Label = tabsHolder_VisualElement.Q<Label>("Settings_Label");
        #endregion

        #region Pages
        accountPage_VisualElement = pagesHolder_VisualElement.Q<VisualElement>("AccountPage_VisualElement");
        startPlayingPage_VisualElement =
            pagesHolder_VisualElement.Q<VisualElement>("StartPlayingPage_VisualElement");
        //3 Page Remains
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
        startPlaying_Label.RegisterCallback<ClickEvent>(OnStartPlayingTabSelected);
        //3 Tab Remains
        settings_Label.RegisterCallback<ClickEvent>(OnSettingsTabSelected);
    }

    private void RemoveFunctionality()
    {
        account_Label.UnregisterCallback<ClickEvent>(OnAccountTabSelected);
        startPlaying_Label.UnregisterCallback<ClickEvent>(OnStartPlayingTabSelected);
        //3 Tab Remains
        settings_Label.UnregisterCallback<ClickEvent>(OnSettingsTabSelected);
    }


    private void OnAccountTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(accountPage_VisualElement);
        account_Label.AddToClassList("TabSelected");
        currentTabSelected = account_Label;
    }

    private void OnStartPlayingTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(startPlayingPage_VisualElement);
        startPlaying_Label.AddToClassList("TabSelected");
        currentTabSelected = startPlaying_Label;
    }

    //3 Tab Remains

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
        startPlayingPage_VisualElement.style.display = DisplayStyle.None;
        //3
        settingsPage_VisualElement.style.display = DisplayStyle.None;

        visualElement.style.display = DisplayStyle.Flex;
    }

}

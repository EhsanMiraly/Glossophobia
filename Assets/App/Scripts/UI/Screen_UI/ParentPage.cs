using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ParentPage : MonoBehaviour
{
    PanelRenderer panelRenderer;
    UIConnector uiConnector;

    [System.NonSerialized] public VisualElement parentPage_VisualElement;
    Label account_Label;
    Label R1_Label;
    Label R2_Label;
    Label R3_Label;
    Label settings_Label;

    Label currentTabSelected;

    VisualElement accountPage_VisualElement;
    //3
    VisualElement settingsPage_VisualElement;



    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        uiConnector = GetComponent<UIConnector>();

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
        parentPage_VisualElement = root.Q<VisualElement>("ParentPage_VisualElement");
        account_Label = parentPage_VisualElement.Q<Label>("Account_Label");
        //R1_Label;
        //R2_Label;
        //R3_Label;
        settings_Label = parentPage_VisualElement.Q<Label>("Settings_Label");

        accountPage_VisualElement = parentPage_VisualElement.Q<VisualElement>("AccountPage_VisualElement");
        //3
        settingsPage_VisualElement = parentPage_VisualElement.Q<VisualElement>("SettingsPage_VisualElement");

        AddFunctionality();

        InitializeUI();
    }

    private void InitializeUI()
    {
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
        //3
        settings_Label.RegisterCallback<ClickEvent>(OnSettingsTabSelected);

    }

    private void OnAccountTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(accountPage_VisualElement);
        account_Label.AddToClassList("TabSelected");
        currentTabSelected = account_Label;

    }

    //3

    private void OnSettingsTabSelected(ClickEvent clickEvent)
    {
        SetTabActive(settingsPage_VisualElement);
        settings_Label.AddToClassList("TabSelected");
        currentTabSelected = settings_Label;

    }

    private void RemoveFunctionality()
    {
        account_Label.UnregisterCallback<ClickEvent>(OnAccountTabSelected);
        //3
        settings_Label.UnregisterCallback<ClickEvent>(OnSettingsTabSelected);
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
        settingsPage_VisualElement.style.display = DisplayStyle.None;

        visualElement.style.display = DisplayStyle.Flex;
    }

}

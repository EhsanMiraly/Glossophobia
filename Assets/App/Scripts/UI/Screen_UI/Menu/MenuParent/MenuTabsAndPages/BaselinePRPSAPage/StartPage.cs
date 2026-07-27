using System;
using UnityEngine;
using UnityEngine.UIElements;

public class StartPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    BaselinePRPSAPage baselinePRPSAPage;

    VisualElement startPage_VisualElement;

    Label explain_Label;
    VisualElement startButton_TemplateContainer;
    Label startButton_Label;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        baselinePRPSAPage = GetComponent<BaselinePRPSAPage>();

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
        startPage_VisualElement = root.Q<VisualElement>("StartPage_VisualElement");

        explain_Label = startPage_VisualElement.Q<Label>("Explain_Label");
        startButton_TemplateContainer = startPage_VisualElement.Q<VisualElement>("StartButton_TemplateContainer");
        startButton_Label = startButton_TemplateContainer.Q<Label>("Text_Label");

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
        startButton_TemplateContainer.RegisterCallback<ClickEvent>(OnStartButtonSelected);
    }

    private void RemoveFunctionality()
    {
        startButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnStartButtonSelected);
    }

    private void OnStartButtonSelected(ClickEvent clickEvent)
    {
        baselinePRPSAPage.SetPageActive(baselinePRPSAPage.questionsPage_VisualElement);
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
        #region explain_Label
        explain_Label.text = LanguageTextsData.explainPRPSA[SettingsData.currentLanguageIndex];
        explain_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        explain_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region startButton_Label
        startButton_Label.text = LanguageTextsData.start[SettingsData.currentLanguageIndex];
        startButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        startButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region explain_Label
        explain_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region startButton_Label
        startButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion




}

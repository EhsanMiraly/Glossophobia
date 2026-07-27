using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangePostTestPRPSAPage : MonoBehaviour
{
    //Check And Change All

    PanelRenderer panelRenderer;

    BaselinePRPSAPage baselinePRPSAPage;

    VisualElement changePage_VisualElement;

    VisualElement changePRPSAButton_TemplateContainer;
    Label changePRPSAButton_Label;


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
        changePage_VisualElement = root.Q<VisualElement>("ChangePage_VisualElement");

        changePRPSAButton_TemplateContainer =
            changePage_VisualElement.Q<VisualElement>("ChangePRPSAButton_TemplateContainer");
        changePRPSAButton_Label = changePRPSAButton_TemplateContainer.Q<Label>("Text_Label");

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
        //changePRPSAButton_TemplateContainer
        changePRPSAButton_TemplateContainer.RegisterCallback<ClickEvent>(OnchangePRPSAButtonSelected);
    }

    private void RemoveFunctionality()
    {
        //changePRPSAButton_TemplateContainer
        changePRPSAButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnchangePRPSAButtonSelected);
    }

    private void OnchangePRPSAButtonSelected(ClickEvent clickEvent)
    {
        EventsManager.InvokeOnChangePRPSA_Before();
        baselinePRPSAPage.SetPageActive(baselinePRPSAPage.startPage_VisualElement);
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
        #region changePRPSAButton_Label
        changePRPSAButton_Label.text =
            LanguageTextsData.changePRPSA[SettingsData.currentLanguageIndex];
        changePRPSAButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        changePRPSAButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

    }

    private void OnFontSizeChanged()
    {
        #region changePRPSAButton_Label
        changePRPSAButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion


}

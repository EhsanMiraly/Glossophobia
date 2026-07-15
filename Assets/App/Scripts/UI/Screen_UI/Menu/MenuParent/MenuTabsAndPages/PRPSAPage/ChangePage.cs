using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangePage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    PRPSAPage PRPSAPage;

    VisualElement changePage_VisualElement;

    VisualElement changePRPSAButton_TemplateContainer;
    Label changePRPSAButton_Label;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        PRPSAPage = GetComponent<PRPSAPage>();

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
        PRPSAPage.SetPageActive(PRPSAPage.startPage_VisualElement);
    }

    #endregion




    #region Events Manager

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
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion


}

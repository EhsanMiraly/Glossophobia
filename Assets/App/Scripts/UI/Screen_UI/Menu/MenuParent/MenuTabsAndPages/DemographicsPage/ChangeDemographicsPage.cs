using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeDemographicsPage : MonoBehaviour
{
    PanelRenderer panelRenderer;
    DemographicsPage demographicsPage;

    VisualElement changeDemographicsPage_VisualElement;
    VisualElement changeDemographicsButton_TemplateContainer;
    Label changeDemographicsButton_Label;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        demographicsPage = GetComponent<DemographicsPage>();

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
        changeDemographicsPage_VisualElement = root.Q<VisualElement>("ChangeDemographicsPage_VisualElement");
        changeDemographicsButton_TemplateContainer =
            changeDemographicsPage_VisualElement.Q<VisualElement>("ChangeDemographicsButton_TemplateContainer");
        changeDemographicsButton_Label = changeDemographicsButton_TemplateContainer.Q<Label>("Text_Label");

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
        //Change Button
        changeDemographicsButton_TemplateContainer.
            RegisterCallback<ClickEvent>(OnChangeDemograsphicsButtonSelected);
    }

    private void RemoveFunctionality()
    {
        //Change Button
        changeDemographicsButton_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnChangeDemograsphicsButtonSelected);
    }


    private void OnChangeDemograsphicsButtonSelected(ClickEvent clickEvent)
    {
        demographicsPage.SetPageActive(demographicsPage.giveDemographicsPage_VisualElement);
    }

    #endregion


    #region Events Manager

    private void OnLanguageChanged()
    {
        #region changeDemograsphicsButton_Label
        changeDemographicsButton_Label.text =
            LanguageTextsData.changeDemographics[SettingsData.currentLanguageIndex];
        changeDemographicsButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        changeDemographicsButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

    }

    private void OnFontSizeChanged()
    {
        #region gender_Text_Label
        changeDemographicsButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion

}

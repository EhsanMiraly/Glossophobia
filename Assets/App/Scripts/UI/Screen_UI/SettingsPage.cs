using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPage : MonoBehaviour
{
    PanelRenderer panelRenderer;
    ParentPage parentPage;

    VisualElement settingsPage_VisualElement;
    ScrollView settings_ScrollView;

    VisualElement languagePreviousNextSelector_TemplateContainer;
    VisualElement language_ChevronLeft_TemplateContainer;
    Label language_Lable;
    VisualElement language_ChevronRight_TemplateContainer;

    VisualElement fontSizePreviousNextSelector_TemplateContainer;
    VisualElement fontSize_ChevronLeft_TemplateContainer;
    Label fontSize_Lable;
    VisualElement fontSize_ChevronRight_TemplateContainer;



    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        parentPage = GetComponent<ParentPage>();

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
        settingsPage_VisualElement = parentPage.parentPage_VisualElement.Q<VisualElement>();
        settings_ScrollView = settingsPage_VisualElement.Q<ScrollView>("Settings_ScrollView");
        UI_Utilities.Initialize_ScrollView(settings_ScrollView);

        //Language
        languagePreviousNextSelector_TemplateContainer = settings_ScrollView.
            Q<VisualElement>("LanguagePreviousNextSelector_TemplateContainer");
        language_ChevronLeft_TemplateContainer = languagePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronLeft_TemplateContainer");
        language_Lable = languagePreviousNextSelector_TemplateContainer.Q<Label>("Text_Label");
        language_ChevronRight_TemplateContainer = languagePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronRight_TemplateContainer");
        FixPreviousNextSelectorDimentions(languagePreviousNextSelector_TemplateContainer);

        //FontSize
        fontSizePreviousNextSelector_TemplateContainer = settings_ScrollView.
            Q<VisualElement>("FontSizePreviousNextSelector_TemplateContainer");
        fontSize_ChevronLeft_TemplateContainer = fontSizePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronLeft_TemplateContainer");
        fontSize_Lable = fontSizePreviousNextSelector_TemplateContainer.Q<Label>("Text_Label");
        fontSize_ChevronRight_TemplateContainer = fontSizePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronRight_TemplateContainer");
        FixPreviousNextSelectorDimentions(fontSizePreviousNextSelector_TemplateContainer);

        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();
    }




    private void FixPreviousNextSelectorDimentions(VisualElement previousNextSelector)
    {
        previousNextSelector.style.width = Length.Percent(100);
        previousNextSelector.style.height = Screen.width / 10;
        previousNextSelector.style.marginBottom = Length.Percent(1);

        VisualElement chevronLeft_TemplateContainer =
            previousNextSelector.Q<VisualElement>("ChevronLeft_TemplateContainer");
        VisualElement chevronRight_TemplateContainer =
            previousNextSelector.Q<VisualElement>("ChevronRight_TemplateContainer");

        chevronLeft_TemplateContainer.style.width = Screen.width / 15;
        chevronLeft_TemplateContainer.style.height = Screen.width / 15;

        chevronRight_TemplateContainer.style.width = Screen.width / 15;
        chevronRight_TemplateContainer.style.height = Screen.width / 15;
    }


    private void AddFunctionality()
    {
        //Language
        language_ChevronLeft_TemplateContainer.RegisterCallback<ClickEvent>(OnLanguage_ChevronLeftSelected);
        language_ChevronRight_TemplateContainer.RegisterCallback<ClickEvent>(OnLanguage_ChevronRightSelected);

        //Font Size
        fontSize_ChevronLeft_TemplateContainer.RegisterCallback<ClickEvent>(OnFontSize_ChevronLeftSelected);
        fontSize_ChevronRight_TemplateContainer.RegisterCallback<ClickEvent>(OnFontSize_ChevronRightSelected);
    }

    private void RemoveFunctionality()
    {
        //Language
        language_ChevronLeft_TemplateContainer.UnregisterCallback<ClickEvent>(OnLanguage_ChevronLeftSelected);
        language_ChevronRight_TemplateContainer.UnregisterCallback<ClickEvent>(OnLanguage_ChevronRightSelected);

        //Font Size
        fontSize_ChevronLeft_TemplateContainer.UnregisterCallback<ClickEvent>(OnFontSize_ChevronLeftSelected);
        fontSize_ChevronRight_TemplateContainer.UnregisterCallback<ClickEvent>(OnFontSize_ChevronRightSelected);
    }

    #region Language
    private void OnLanguage_ChevronLeftSelected(ClickEvent clickEvent)
    {
        SettingsData.currentLanguageIndex--;
        if (SettingsData.currentLanguageIndex < 0)
        {
            SettingsData.currentLanguageIndex = LanguageTextsData.languages.Count - 1;
        }
        EventsManager.InvokeOnLanguageChanged();
    }

    private void OnLanguage_ChevronRightSelected(ClickEvent clickEvent)
    {
        SettingsData.currentLanguageIndex++;
        if (SettingsData.currentLanguageIndex >= LanguageTextsData.languages.Count)
        {
            SettingsData.currentLanguageIndex = 0;
        }
        EventsManager.InvokeOnLanguageChanged();
    }
    #endregion


    private void OnFontSize_ChevronLeftSelected(ClickEvent evt)
    {
        SettingsData.currentFontSizeIndex--;
        if (SettingsData.currentFontSizeIndex < 0)
        {
            SettingsData.currentFontSizeIndex = LanguageTextsData.fontSize_Text.Count - 1;
        }
        fontSizePreviousNextSelector_TemplateContainer.Q<Label>().text =
            LanguageTextsData.fontSize_Text[SettingsData.currentFontSizeIndex].
            FontSizeLanguage[SettingsData.currentLanguageIndex];
        EventsManager.InvokeOnFontSizeChanged();
    }

    private void OnFontSize_ChevronRightSelected(ClickEvent evt)
    {
        SettingsData.currentFontSizeIndex++;
        if (SettingsData.currentFontSizeIndex >= LanguageTextsData.fontSize_Text.Count)
        {
            SettingsData.currentFontSizeIndex = 0;
        }
        fontSizePreviousNextSelector_TemplateContainer.Q<Label>().text =
            LanguageTextsData.fontSize_Text[SettingsData.currentFontSizeIndex].
            FontSizeLanguage[SettingsData.currentLanguageIndex];
        EventsManager.InvokeOnFontSizeChanged();
    }







    #region Events Manager

    private void OnLanguageChanged()
    {
        #region Language Label
        Label language_Label = languagePreviousNextSelector_TemplateContainer.Q<Label>();
        language_Label.text = LanguageTextsData.languages[SettingsData.currentLanguageIndex].language;
        language_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        language_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region FontSize Label
        Label fontSize_Label = fontSizePreviousNextSelector_TemplateContainer.Q<Label>();
        fontSize_Label.text = LanguageTextsData.fontSize_Text[SettingsData.currentFontSizeIndex].
                                FontSizeLanguage[SettingsData.currentLanguageIndex];
        fontSize_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        fontSize_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region Language Label
        Label language_Label = languagePreviousNextSelector_TemplateContainer.Q<Label>();
        language_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region FontSize Label
        Label fontSize_Label = fontSizePreviousNextSelector_TemplateContainer.Q<Label>();
        fontSize_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion


}

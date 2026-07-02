using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPage : MonoBehaviour
{
    PanelRenderer panelRenderer;
    ParentPage parentPage;

    VisualElement settingsPage_VisualElement;
    ScrollView settings_ScrollView;

    VisualElement languagePreviousNextSelector_TemplateContainer;
    VisualElement fontSizePreviousNextSelector_TemplateContainer;


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
        //RemoveFunctionality();
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);

        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }

    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        settingsPage_VisualElement = parentPage.parentPage_VisualElement.Q<VisualElement>();
        settings_ScrollView = settingsPage_VisualElement.Q<ScrollView>("Settings_ScrollView");
        UI_Utilities.Initialize_ScrollView(settings_ScrollView);

        languagePreviousNextSelector_TemplateContainer =
            settings_ScrollView.Q<VisualElement>("LanguagePreviousNextSelector_TemplateContainer");
        FixPreviousNextSelectorDimentions(languagePreviousNextSelector_TemplateContainer);

        fontSizePreviousNextSelector_TemplateContainer =
            settings_ScrollView.Q<VisualElement>("FontSizePreviousNextSelector_TemplateContainer");
        FixPreviousNextSelectorDimentions(fontSizePreviousNextSelector_TemplateContainer);

        OnLanguageChanged();
        OnFontSizeChanged();
    }




    private void FixPreviousNextSelectorDimentions(VisualElement previousNextSelector)
    {
        previousNextSelector.style.width = Length.Percent(100);
        previousNextSelector.style.height = Screen.width / 10;

        VisualElement chevronLeft_TemplateContainer =
            previousNextSelector.Q<VisualElement>("ChevronLeft_TemplateContainer");
        VisualElement chevronRight_TemplateContainer =
            previousNextSelector.Q<VisualElement>("ChevronRight_TemplateContainer");

        chevronLeft_TemplateContainer.style.width = Screen.width / 15;
        chevronLeft_TemplateContainer.style.height = Screen.width / 15;

        chevronRight_TemplateContainer.style.width = Screen.width / 15;
        chevronRight_TemplateContainer.style.height = Screen.width / 15;
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

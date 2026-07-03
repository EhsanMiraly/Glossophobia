using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPage : MonoBehaviour
{
    PanelRenderer panelRenderer;
    ParentPage parentPage;

    VisualElement settingsPage_VisualElement;
    ScrollView settings_ScrollView;

    #region Language
    VisualElement languagePreviousNextSelector_TemplateContainer;
    VisualElement language_ChevronLeft_TemplateContainer;
    Label language_Text_Label;
    VisualElement language_ChevronRight_TemplateContainer;
    #endregion

    #region Font Size
    VisualElement fontSizePreviousNextSelector_TemplateContainer;
    VisualElement fontSize_ChevronLeft_TemplateContainer;
    Label fontSize_Text_Label;
    VisualElement fontSize_ChevronRight_TemplateContainer;
    #endregion

    #region Sound Volume
    VisualElement soundVolume_LabeledSliderInt_TemplateContainer;
    Label soundVolume_Text_Label;
    VisualElement soundVolume_MinusButton_TemplateContainer;
    VisualElement soundVolume_InvisibleForeground_VisualElement;
    VisualElement soundVolume_PlusButton_TemplateContainer;
    #endregion



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

        #region Language
        languagePreviousNextSelector_TemplateContainer = settings_ScrollView.
            Q<VisualElement>("LanguagePreviousNextSelector_TemplateContainer");
        language_ChevronLeft_TemplateContainer = languagePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronLeft_TemplateContainer");
        language_Text_Label = languagePreviousNextSelector_TemplateContainer.Q<Label>("Text_Label");
        language_ChevronRight_TemplateContainer = languagePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronRight_TemplateContainer");
        Fix_PreviousNextSelector_Dimentions(languagePreviousNextSelector_TemplateContainer);
        #endregion

        #region Font Size
        fontSizePreviousNextSelector_TemplateContainer = settings_ScrollView.
            Q<VisualElement>("FontSizePreviousNextSelector_TemplateContainer");
        fontSize_ChevronLeft_TemplateContainer = fontSizePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronLeft_TemplateContainer");
        fontSize_Text_Label = fontSizePreviousNextSelector_TemplateContainer.Q<Label>("Text_Label");
        fontSize_ChevronRight_TemplateContainer = fontSizePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronRight_TemplateContainer");
        Fix_PreviousNextSelector_Dimentions(fontSizePreviousNextSelector_TemplateContainer);
        #endregion

        #region Sound Volume
        soundVolume_LabeledSliderInt_TemplateContainer = settings_ScrollView.
            Q<VisualElement>("SoundVolume_LabeledSliderInt_TemplateContainer");
        soundVolume_Text_Label = soundVolume_LabeledSliderInt_TemplateContainer.
            Q<Label>("Text_Label");
        soundVolume_MinusButton_TemplateContainer = soundVolume_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("MinusButton_TemplateContainer");
        soundVolume_InvisibleForeground_VisualElement = soundVolume_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("InvisibleForeground_VisualElement");
        soundVolume_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentSoundVolume * 100);
        soundVolume_PlusButton_TemplateContainer = soundVolume_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("PlusButton_TemplateContainer");
        Fix_LabeledSliderInt_Dimentions(soundVolume_LabeledSliderInt_TemplateContainer);
        #endregion

        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();
    }



    #region UI Utilities

    private void Fix_PreviousNextSelector_Dimentions(VisualElement previousNextSelector)
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

    private void Fix_LabeledSliderInt_Dimentions(VisualElement labeledSliderInt)
    {
        labeledSliderInt.style.width = Length.Percent(100);
        labeledSliderInt.style.height = Screen.width / 5;
        labeledSliderInt.style.marginBottom = Length.Percent(1);

        VisualElement minusButton_TemplateContainer = labeledSliderInt.
            Q<VisualElement>("MinusButton_TemplateContainer");
        VisualElement plusButton_TemplateContainer = labeledSliderInt.
            Q<VisualElement>("PlusButton_TemplateContainer");

        minusButton_TemplateContainer.style.width = Screen.width / 15;
        minusButton_TemplateContainer.style.height = Screen.width / 15;

        plusButton_TemplateContainer.style.width = Screen.width / 15;
        plusButton_TemplateContainer.style.height = Screen.width / 15;
    }

    #endregion


    private void AddFunctionality()
    {
        //Language
        language_ChevronLeft_TemplateContainer.RegisterCallback<ClickEvent>(OnLanguage_ChevronLeftSelected);
        language_ChevronRight_TemplateContainer.RegisterCallback<ClickEvent>(OnLanguage_ChevronRightSelected);

        //Font Size
        fontSize_ChevronLeft_TemplateContainer.RegisterCallback<ClickEvent>(OnFontSize_ChevronLeftSelected);
        fontSize_ChevronRight_TemplateContainer.RegisterCallback<ClickEvent>(OnFontSize_ChevronRightSelected);

        //Sound Volume
        soundVolume_MinusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnSoundVolume_MinusButtonSelected);
        soundVolume_PlusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnSoundVolume_PlusButtonSelected);
    }

    private void RemoveFunctionality()
    {
        //Language
        language_ChevronLeft_TemplateContainer.UnregisterCallback<ClickEvent>(OnLanguage_ChevronLeftSelected);
        language_ChevronRight_TemplateContainer.UnregisterCallback<ClickEvent>(OnLanguage_ChevronRightSelected);

        //Font Size
        fontSize_ChevronLeft_TemplateContainer.UnregisterCallback<ClickEvent>(OnFontSize_ChevronLeftSelected);
        fontSize_ChevronRight_TemplateContainer.UnregisterCallback<ClickEvent>(OnFontSize_ChevronRightSelected);

        //Sound Volume
        soundVolume_MinusButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnSoundVolume_MinusButtonSelected);
        soundVolume_PlusButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnSoundVolume_PlusButtonSelected);

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

    #region Font Size
    private void OnFontSize_ChevronLeftSelected(ClickEvent evt)
    {
        SettingsData.currentFontSizeIndex--;
        if (SettingsData.currentFontSizeIndex < 0)
        {
            SettingsData.currentFontSizeIndex = LanguageTextsData.fontSize_Text.Count - 1;
        }
        fontSize_Text_Label.text = LanguageTextsData.fontSize_Text[SettingsData.currentFontSizeIndex].
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
        fontSize_Text_Label.text = LanguageTextsData.fontSize_Text[SettingsData.currentFontSizeIndex].
                                    FontSizeLanguage[SettingsData.currentLanguageIndex];
        EventsManager.InvokeOnFontSizeChanged();
    }
    #endregion

    #region Sound Volume

    private void OnSoundVolume_MinusButtonSelected(ClickEvent clickEvent)
    {
        SettingsData.currentSoundVolume -= 0.1f;
        if (SettingsData.currentSoundVolume < 0.1f)
        {
            SettingsData.currentSoundVolume = 0.1f;
        }
        soundVolume_Text_Label.text = LanguageTextsData.soundVolume[SettingsData.currentLanguageIndex] +
            Mathf.RoundToInt(SettingsData.currentSoundVolume * 10);
        soundVolume_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentSoundVolume * 100);
        EventsManager.InvokeOnSoundVolumeChanged();
    }

    private void OnSoundVolume_PlusButtonSelected(ClickEvent clickEvent)
    {
        SettingsData.currentSoundVolume += 0.1f;
        if (SettingsData.currentSoundVolume > 1)
        {
            SettingsData.currentSoundVolume = 1;
        }
        soundVolume_Text_Label.text = LanguageTextsData.soundVolume[SettingsData.currentLanguageIndex] +
            Mathf.RoundToInt(SettingsData.currentSoundVolume * 10);
        soundVolume_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentSoundVolume * 100);
        EventsManager.InvokeOnSoundVolumeChanged();
    }

    #endregion



    #region Events Manager

    private void OnLanguageChanged()
    {
        #region Language Label
        language_Text_Label.text = LanguageTextsData.languages[SettingsData.currentLanguageIndex].language;
        language_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        language_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region FontSize Label
        fontSize_Text_Label.text = LanguageTextsData.fontSize_Text[SettingsData.currentFontSizeIndex].
            FontSizeLanguage[SettingsData.currentLanguageIndex];
        fontSize_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        fontSize_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region SoundVolume Label
        soundVolume_Text_Label.text = LanguageTextsData.soundVolume[SettingsData.currentLanguageIndex] +
            (SettingsData.currentSoundVolume * 10);
        soundVolume_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        soundVolume_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region Language Label
        language_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region FontSize Label
        fontSize_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region SoundVolume Label
        soundVolume_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion


}

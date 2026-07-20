using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

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

    #region Target Frame Rate
    VisualElement targetFrameRatePreviousNextSelector_TemplateContainer;
    VisualElement targetFrameRate_ChevronLeft_TemplateContainer;
    Label targetFrameRate_Text_Label;
    VisualElement targetFrameRate_ChevronRight_TemplateContainer;
    #endregion

    #region Field Of View
    VisualElement fieldOfViewPreviousNextSelector_TemplateContainer;
    VisualElement fieldOfView_ChevronLeft_TemplateContainer;
    Label fieldOfView_Text_Label;
    VisualElement fieldOfView_ChevronRight_TemplateContainer;
    #endregion

    #region Move Speed
    VisualElement moveSpeed_LabeledSliderInt_TemplateContainer;
    Label moveSpeed_Text_Label;
    VisualElement moveSpeed_MinusButton_TemplateContainer;
    VisualElement moveSpeed_InvisibleForeground_VisualElement;
    VisualElement moveSpeed_PlusButton_TemplateContainer;
    #endregion

    #region Horizontal Sensitivity
    VisualElement horizontalSensitivity_LabeledSliderInt_TemplateContainer;
    Label horizontalSensitivity_Text_Label;
    VisualElement horizontalSensitivity_MinusButton_TemplateContainer;
    VisualElement horizontalSensitivity_InvisibleForeground_VisualElement;
    VisualElement horizontalSensitivity_PlusButton_TemplateContainer;
    #endregion

    #region Vertical Sensitivity
    VisualElement verticalSensitivity_LabeledSliderInt_TemplateContainer;
    Label verticalSensitivity_Text_Label;
    VisualElement verticalSensitivity_MinusButton_TemplateContainer;
    VisualElement verticalSensitivity_InvisibleForeground_VisualElement;
    VisualElement verticalSensitivity_PlusButton_TemplateContainer;
    #endregion


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

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
        settingsPage_VisualElement = root.Q<VisualElement>("SettingsPage_VisualElement");
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
        UI_Utilities.Fix_PreviousNextSelector_Dimentions(languagePreviousNextSelector_TemplateContainer);
        #endregion

        #region Font Size
        fontSizePreviousNextSelector_TemplateContainer = settings_ScrollView.
            Q<VisualElement>("FontSizePreviousNextSelector_TemplateContainer");
        fontSize_ChevronLeft_TemplateContainer = fontSizePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronLeft_TemplateContainer");
        fontSize_Text_Label = fontSizePreviousNextSelector_TemplateContainer.Q<Label>("Text_Label");
        fontSize_ChevronRight_TemplateContainer = fontSizePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronRight_TemplateContainer");
        UI_Utilities.Fix_PreviousNextSelector_Dimentions(fontSizePreviousNextSelector_TemplateContainer);
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
            Length.Percent(SettingsData.currentSoundVolume * 10);
        soundVolume_PlusButton_TemplateContainer = soundVolume_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("PlusButton_TemplateContainer");
        UI_Utilities.Fix_LabeledSliderInt_Dimentions(soundVolume_LabeledSliderInt_TemplateContainer);
        #endregion

        #region Target Frame Rate
        targetFrameRatePreviousNextSelector_TemplateContainer = settings_ScrollView.
            Q<VisualElement>("TargetFrameRatePreviousNextSelector_TemplateContainer");
        targetFrameRate_ChevronLeft_TemplateContainer = targetFrameRatePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronLeft_TemplateContainer");
        targetFrameRate_Text_Label = targetFrameRatePreviousNextSelector_TemplateContainer.Q<Label>("Text_Label");
        targetFrameRate_ChevronRight_TemplateContainer = targetFrameRatePreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronRight_TemplateContainer");
        UI_Utilities.Fix_PreviousNextSelector_Dimentions(targetFrameRatePreviousNextSelector_TemplateContainer);
        Application.targetFrameRate = LanguageTextsData.frameRates[SettingsData.currentTargetFrameRateIndex];
        #endregion

        #region Field Of View
        fieldOfViewPreviousNextSelector_TemplateContainer = settings_ScrollView.
            Q<VisualElement>("FieldOfViewPreviousNextSelector_TemplateContainer");
        fieldOfView_ChevronLeft_TemplateContainer = fieldOfViewPreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronLeft_TemplateContainer");
        fieldOfView_Text_Label = fieldOfViewPreviousNextSelector_TemplateContainer.Q<Label>("Text_Label");
        fieldOfView_ChevronRight_TemplateContainer = fieldOfViewPreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronRight_TemplateContainer");
        UI_Utilities.Fix_PreviousNextSelector_Dimentions(fieldOfViewPreviousNextSelector_TemplateContainer);
        Camera.main.fieldOfView = LanguageTextsData.fieldOfViews[SettingsData.currentFieldOfViewIndex];
        #endregion

        #region Move Speed
        moveSpeed_LabeledSliderInt_TemplateContainer = settings_ScrollView.
            Q<VisualElement>("MoveSpeed_LabeledSliderInt_TemplateContainer");
        moveSpeed_Text_Label = moveSpeed_LabeledSliderInt_TemplateContainer.
            Q<Label>("Text_Label");
        moveSpeed_MinusButton_TemplateContainer = moveSpeed_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("MinusButton_TemplateContainer");
        moveSpeed_InvisibleForeground_VisualElement = moveSpeed_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("InvisibleForeground_VisualElement");
        moveSpeed_PlusButton_TemplateContainer = moveSpeed_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("PlusButton_TemplateContainer");
        UI_Utilities.Fix_LabeledSliderInt_Dimentions(moveSpeed_LabeledSliderInt_TemplateContainer);
        moveSpeed_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentMoveSpeed * 10);
        #endregion

        #region Horizontal Sensitivity
        horizontalSensitivity_LabeledSliderInt_TemplateContainer = settings_ScrollView.
            Q<VisualElement>("HorizontalSensitivity_LabeledSliderInt_TemplateContainer");
        horizontalSensitivity_Text_Label = horizontalSensitivity_LabeledSliderInt_TemplateContainer.
            Q<Label>("Text_Label");
        horizontalSensitivity_MinusButton_TemplateContainer =
            horizontalSensitivity_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("MinusButton_TemplateContainer");
        horizontalSensitivity_InvisibleForeground_VisualElement =
            horizontalSensitivity_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("InvisibleForeground_VisualElement");
        horizontalSensitivity_PlusButton_TemplateContainer =
            horizontalSensitivity_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("PlusButton_TemplateContainer");
        UI_Utilities.Fix_LabeledSliderInt_Dimentions(horizontalSensitivity_LabeledSliderInt_TemplateContainer);
        horizontalSensitivity_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentHorizontalSensitivity * 2);
        #endregion

        #region Vertical Sensitivity
        verticalSensitivity_LabeledSliderInt_TemplateContainer = settings_ScrollView.
            Q<VisualElement>("VerticalSensitivity_LabeledSliderInt_TemplateContainer");
        verticalSensitivity_Text_Label = verticalSensitivity_LabeledSliderInt_TemplateContainer.
            Q<Label>("Text_Label");
        verticalSensitivity_MinusButton_TemplateContainer =
            verticalSensitivity_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("MinusButton_TemplateContainer");
        verticalSensitivity_InvisibleForeground_VisualElement =
            verticalSensitivity_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("InvisibleForeground_VisualElement");
        verticalSensitivity_PlusButton_TemplateContainer =
            verticalSensitivity_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("PlusButton_TemplateContainer");
        UI_Utilities.Fix_LabeledSliderInt_Dimentions(verticalSensitivity_LabeledSliderInt_TemplateContainer);
        verticalSensitivity_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentVerticalSensitivity * 2);
        #endregion

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
        //Language
        language_ChevronLeft_TemplateContainer.RegisterCallback<ClickEvent>(OnLanguage_ChevronLeftSelected);
        language_ChevronRight_TemplateContainer.RegisterCallback<ClickEvent>(OnLanguage_ChevronRightSelected);

        //Font Size
        fontSize_ChevronLeft_TemplateContainer.RegisterCallback<ClickEvent>(OnFontSize_ChevronLeftSelected);
        fontSize_ChevronRight_TemplateContainer.RegisterCallback<ClickEvent>(OnFontSize_ChevronRightSelected);

        //Sound Volume
        soundVolume_MinusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnSoundVolume_MinusButtonSelected);
        soundVolume_PlusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnSoundVolume_PlusButtonSelected);

        //Target Frame Rate
        targetFrameRate_ChevronLeft_TemplateContainer.
            RegisterCallback<ClickEvent>(OnTargetFrameRate_ChevronLeftSelected);
        targetFrameRate_ChevronRight_TemplateContainer.
            RegisterCallback<ClickEvent>(OnTargetFrameRate_ChevronRightSelected);

        //Field Of View
        fieldOfView_ChevronLeft_TemplateContainer.
            RegisterCallback<ClickEvent>(OnFieldOfView_ChevronLeftSelected);
        fieldOfView_ChevronRight_TemplateContainer.
            RegisterCallback<ClickEvent>(OnFieldOfView_ChevronRightSelected);

        //Move Speed
        moveSpeed_MinusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnMoveSpeed_MinusButtonSelected);
        moveSpeed_PlusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnMoveSpeed_PlusButtonSelected);

        //Horizontal Sensitivity
        horizontalSensitivity_MinusButton_TemplateContainer.
            RegisterCallback<ClickEvent>(OnHorizontalSensitivity_MinusButtonSelected);
        horizontalSensitivity_PlusButton_TemplateContainer.
            RegisterCallback<ClickEvent>(OnHorizontalSensitivity_PlusButtonSelected);

        //Vertical Sensitivity
        verticalSensitivity_MinusButton_TemplateContainer.
            RegisterCallback<ClickEvent>(OnVerticalSensitivity_MinusButtonSelected);
        verticalSensitivity_PlusButton_TemplateContainer.
            RegisterCallback<ClickEvent>(OnVerticalSensitivity_PlusButtonSelected);
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

        //Target Frame Rate
        targetFrameRate_ChevronLeft_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnTargetFrameRate_ChevronLeftSelected);
        targetFrameRate_ChevronRight_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnTargetFrameRate_ChevronRightSelected);

        //Field Of View
        fieldOfView_ChevronLeft_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnFieldOfView_ChevronLeftSelected);
        fieldOfView_ChevronRight_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnFieldOfView_ChevronRightSelected);

        //Move Speed
        moveSpeed_MinusButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnMoveSpeed_MinusButtonSelected);
        moveSpeed_PlusButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnMoveSpeed_PlusButtonSelected);

        //Horizontal Sensitivity
        horizontalSensitivity_MinusButton_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnHorizontalSensitivity_MinusButtonSelected);
        horizontalSensitivity_PlusButton_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnHorizontalSensitivity_PlusButtonSelected);

        //Vertical Sensitivity
        verticalSensitivity_MinusButton_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnVerticalSensitivity_MinusButtonSelected);
        verticalSensitivity_PlusButton_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnVerticalSensitivity_PlusButtonSelected);

    }

    #region Language
    private void OnLanguage_ChevronLeftSelected(ClickEvent clickEvent)
    {
        SettingsData.currentLanguageIndex--;
        if (SettingsData.currentLanguageIndex < 0)
        {
            SettingsData.currentLanguageIndex = LanguageTextsData.languages.Count - 1;
        }
        Settings_SaveSystem.Save_Settings();
        EventsManager.InvokeOnLanguageChanged();
    }

    private void OnLanguage_ChevronRightSelected(ClickEvent clickEvent)
    {
        SettingsData.currentLanguageIndex++;
        if (SettingsData.currentLanguageIndex >= LanguageTextsData.languages.Count)
        {
            SettingsData.currentLanguageIndex = 0;
        }
        Settings_SaveSystem.Save_Settings();
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
        Settings_SaveSystem.Save_Settings();
        fontSize_Text_Label.text = LanguageTextsData.fontSize_Text[SettingsData.currentFontSizeIndex].
                                    ListString[SettingsData.currentLanguageIndex];
        EventsManager.InvokeOnFontSizeChanged();
    }

    private void OnFontSize_ChevronRightSelected(ClickEvent evt)
    {
        SettingsData.currentFontSizeIndex++;
        if (SettingsData.currentFontSizeIndex >= LanguageTextsData.fontSize_Text.Count)
        {
            SettingsData.currentFontSizeIndex = 0;
        }
        Settings_SaveSystem.Save_Settings();
        fontSize_Text_Label.text = LanguageTextsData.fontSize_Text[SettingsData.currentFontSizeIndex].
                                    ListString[SettingsData.currentLanguageIndex];
        EventsManager.InvokeOnFontSizeChanged();
    }
    #endregion

    #region Sound Volume

    private void OnSoundVolume_MinusButtonSelected(ClickEvent clickEvent)
    {
        SettingsData.currentSoundVolume -= 1;
        if (SettingsData.currentSoundVolume < 1)
        {
            SettingsData.currentSoundVolume = 1;
        }
        Settings_SaveSystem.Save_Settings();
        soundVolume_Text_Label.text = LanguageTextsData.soundVolume[SettingsData.currentLanguageIndex] +
            SettingsData.currentSoundVolume;
        soundVolume_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentSoundVolume * 10);
        EventsManager.InvokeOnSoundVolumeChanged();
    }

    private void OnSoundVolume_PlusButtonSelected(ClickEvent clickEvent)
    {
        SettingsData.currentSoundVolume += 1;
        if (SettingsData.currentSoundVolume > 10)
        {
            SettingsData.currentSoundVolume = 10;
        }
        Settings_SaveSystem.Save_Settings();
        soundVolume_Text_Label.text = LanguageTextsData.soundVolume[SettingsData.currentLanguageIndex] +
            SettingsData.currentSoundVolume;
        soundVolume_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentSoundVolume * 10);
        EventsManager.InvokeOnSoundVolumeChanged();
    }

    #endregion

    #region Target Frame Rate
    private void OnTargetFrameRate_ChevronLeftSelected(ClickEvent clickEvent)
    {
        SettingsData.currentTargetFrameRateIndex--;
        if (SettingsData.currentTargetFrameRateIndex < 0)
        {
            SettingsData.currentTargetFrameRateIndex = LanguageTextsData.frameRates.Count - 1;
        }
        Settings_SaveSystem.Save_Settings();
        targetFrameRate_Text_Label.text = LanguageTextsData.frameRate[SettingsData.currentLanguageIndex]
             + LanguageTextsData.frameRates[SettingsData.currentTargetFrameRateIndex];
        Application.targetFrameRate = LanguageTextsData.frameRates[SettingsData.currentTargetFrameRateIndex];
    }

    private void OnTargetFrameRate_ChevronRightSelected(ClickEvent clickEvent)
    {
        SettingsData.currentTargetFrameRateIndex++;
        if (SettingsData.currentTargetFrameRateIndex >= LanguageTextsData.frameRates.Count)
        {
            SettingsData.currentTargetFrameRateIndex = 0;
        }
        Settings_SaveSystem.Save_Settings();
        targetFrameRate_Text_Label.text = LanguageTextsData.frameRate[SettingsData.currentLanguageIndex]
             + LanguageTextsData.frameRates[SettingsData.currentTargetFrameRateIndex];
        Application.targetFrameRate = LanguageTextsData.frameRates[SettingsData.currentTargetFrameRateIndex];
    }
    #endregion

    #region Field Of View
    private void OnFieldOfView_ChevronLeftSelected(ClickEvent clickEvent)
    {
        SettingsData.currentFieldOfViewIndex--;
        if (SettingsData.currentFieldOfViewIndex < 0)
        {
            SettingsData.currentFieldOfViewIndex = LanguageTextsData.fieldOfViews.Count - 1;
        }
        Settings_SaveSystem.Save_Settings();
        fieldOfView_Text_Label.text = LanguageTextsData.fieldOfView[SettingsData.currentLanguageIndex]
             + LanguageTextsData.fieldOfViews[SettingsData.currentFieldOfViewIndex];
        Camera.main.fieldOfView = LanguageTextsData.fieldOfViews[SettingsData.currentFieldOfViewIndex];
    }

    private void OnFieldOfView_ChevronRightSelected(ClickEvent clickEvent)
    {
        SettingsData.currentFieldOfViewIndex++;
        if (SettingsData.currentFieldOfViewIndex >= LanguageTextsData.fieldOfViews.Count)
        {
            SettingsData.currentFieldOfViewIndex = 0;
        }
        Settings_SaveSystem.Save_Settings();
        fieldOfView_Text_Label.text = LanguageTextsData.fieldOfView[SettingsData.currentLanguageIndex]
             + LanguageTextsData.fieldOfViews[SettingsData.currentFieldOfViewIndex];
        Camera.main.fieldOfView = LanguageTextsData.fieldOfViews[SettingsData.currentFieldOfViewIndex];
    }
    #endregion

    #region Move Speed

    private void OnMoveSpeed_MinusButtonSelected(ClickEvent clickEvent)
    {
        SettingsData.currentMoveSpeed -= 1;
        if (SettingsData.currentMoveSpeed < 1)
        {
            SettingsData.currentMoveSpeed = 1;
        }
        Settings_SaveSystem.Save_Settings();
        moveSpeed_Text_Label.text = LanguageTextsData.moveSpeed[SettingsData.currentLanguageIndex] +
                SettingsData.currentMoveSpeed;
        moveSpeed_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentMoveSpeed * 10);
    }

    private void OnMoveSpeed_PlusButtonSelected(ClickEvent clickEvent)
    {
        SettingsData.currentMoveSpeed += 1;
        if (SettingsData.currentMoveSpeed > 10)
        {
            SettingsData.currentMoveSpeed = 10;
        }
        Settings_SaveSystem.Save_Settings();
        moveSpeed_Text_Label.text = LanguageTextsData.moveSpeed[SettingsData.currentLanguageIndex] +
                SettingsData.currentMoveSpeed;
        moveSpeed_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentMoveSpeed * 10);
    }

    #endregion

    #region Horizontal Sensitivity

    private void OnHorizontalSensitivity_MinusButtonSelected(ClickEvent clickEvent)
    {
        SettingsData.currentHorizontalSensitivity -= 1;
        if (SettingsData.currentHorizontalSensitivity < 1)
        {
            SettingsData.currentHorizontalSensitivity = 1;
        }
        Settings_SaveSystem.Save_Settings();
        horizontalSensitivity_Text_Label.text =
            LanguageTextsData.horizontalSensitivity[SettingsData.currentLanguageIndex] +
            SettingsData.currentHorizontalSensitivity;
        horizontalSensitivity_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentHorizontalSensitivity * 2);
    }

    private void OnHorizontalSensitivity_PlusButtonSelected(ClickEvent clickEvent)
    {
        SettingsData.currentHorizontalSensitivity += 1;
        if (SettingsData.currentHorizontalSensitivity > 50)
        {
            SettingsData.currentHorizontalSensitivity = 50;
        }
        Settings_SaveSystem.Save_Settings();
        horizontalSensitivity_Text_Label.text =
            LanguageTextsData.horizontalSensitivity[SettingsData.currentLanguageIndex] +
            SettingsData.currentHorizontalSensitivity;
        horizontalSensitivity_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentHorizontalSensitivity * 2);
    }

    #endregion

    #region Vertical Sensitivity

    private void OnVerticalSensitivity_MinusButtonSelected(ClickEvent clickEvent)
    {
        SettingsData.currentVerticalSensitivity -= 1;
        if (SettingsData.currentVerticalSensitivity < 1)
        {
            SettingsData.currentVerticalSensitivity = 1;
        }
        Settings_SaveSystem.Save_Settings();
        verticalSensitivity_Text_Label.text =
            LanguageTextsData.verticalSensitivity[SettingsData.currentLanguageIndex] +
            SettingsData.currentVerticalSensitivity;
        verticalSensitivity_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentVerticalSensitivity * 2);
    }

    private void OnVerticalSensitivity_PlusButtonSelected(ClickEvent clickEvent)
    {
        SettingsData.currentVerticalSensitivity += 1;
        if (SettingsData.currentVerticalSensitivity > 50)
        {
            SettingsData.currentVerticalSensitivity = 50;
        }
        Settings_SaveSystem.Save_Settings();
        verticalSensitivity_Text_Label.text =
            LanguageTextsData.verticalSensitivity[SettingsData.currentLanguageIndex] +
            SettingsData.currentVerticalSensitivity;
        verticalSensitivity_InvisibleForeground_VisualElement.style.width =
            Length.Percent(SettingsData.currentVerticalSensitivity * 2);
    }

    #endregion

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
            ListString[SettingsData.currentLanguageIndex];
        fontSize_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        fontSize_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region SoundVolume Label
        soundVolume_Text_Label.text = LanguageTextsData.soundVolume[SettingsData.currentLanguageIndex] +
           SettingsData.currentSoundVolume;
        soundVolume_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        soundVolume_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Target Frame Rate
        targetFrameRate_Text_Label.text = LanguageTextsData.frameRate[SettingsData.currentLanguageIndex]
                + LanguageTextsData.frameRates[SettingsData.currentTargetFrameRateIndex];
        targetFrameRate_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        targetFrameRate_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Field Of View
        fieldOfView_Text_Label.text = LanguageTextsData.fieldOfView[SettingsData.currentLanguageIndex]
                + LanguageTextsData.fieldOfViews[SettingsData.currentFieldOfViewIndex];
        fieldOfView_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        fieldOfView_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Move Speed
        moveSpeed_Text_Label.text = LanguageTextsData.moveSpeed[SettingsData.currentLanguageIndex] +
            SettingsData.currentMoveSpeed;
        moveSpeed_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        moveSpeed_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Horizontal Sensitivity
        horizontalSensitivity_Text_Label.text =
            LanguageTextsData.horizontalSensitivity[SettingsData.currentLanguageIndex] +
            SettingsData.currentHorizontalSensitivity;
        horizontalSensitivity_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        horizontalSensitivity_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Vertical Sensitivity
        verticalSensitivity_Text_Label.text =
            LanguageTextsData.verticalSensitivity[SettingsData.currentLanguageIndex] +
            SettingsData.currentVerticalSensitivity;
        verticalSensitivity_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        verticalSensitivity_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

    }

    private void OnFontSizeChanged()
    {
        #region Language Label
        language_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region FontSize Label
        fontSize_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region SoundVolume Label
        soundVolume_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Target Frame Rate
        targetFrameRate_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Field Of View
        fieldOfView_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Move Speed
        moveSpeed_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Horizontal Sensitivity
        horizontalSensitivity_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Vertical Sensitivity
        verticalSensitivity_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion


}

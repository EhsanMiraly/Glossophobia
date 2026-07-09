using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class DemographicsPage : MonoBehaviour
{
    PanelRenderer panelRenderer;


    VisualElement demographicsPage_VisualElement;
    ScrollView demographics_ScrollView;


    #region Gender
    VisualElement gender_PreviousNextSelector_TemplateContainer;
    VisualElement gender_ChevronLeft_TemplateContainer;
    Label gender_Text_Label;
    VisualElement gender_ChevronRight_TemplateContainer;
    #endregion


    #region Age
    VisualElement age_LabeledSliderInt_TemplateContainer;
    Label age_Text_Label;
    VisualElement age_MinusButton_TemplateContainer;
    VisualElement age_InvisibleForeground_VisualElement;
    VisualElement age_PlusButton_TemplateContainer;
    #endregion

    #region EducationLevel
    VisualElement educationLevel_SingleSelection_TemplateContainer;
    VisualElement educationLevel_Parent_VisualElement;
    Label educationLevel_WhatAmI_Label;

    List<VisualElement> educationLevel_OptionsChackMarks;
    #endregion


    VisualElement saveButton_TemplateContainer;
    Label saveButton_Label;


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
        demographicsPage_VisualElement = root.Q<VisualElement>("DemographicsPage_VisualElement");
        demographics_ScrollView = demographicsPage_VisualElement.Q<ScrollView>("Demographics_ScrollView");
        UI_Utilities.Initialize_ScrollView(demographics_ScrollView);

        #region Gender
        gender_PreviousNextSelector_TemplateContainer = demographics_ScrollView.
            Q<VisualElement>("Gender_PreviousNextSelector_TemplateContainer");
        gender_ChevronLeft_TemplateContainer = gender_PreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronLeft_TemplateContainer");
        gender_Text_Label = gender_PreviousNextSelector_TemplateContainer.Q<Label>("Text_Label");
        gender_ChevronRight_TemplateContainer = gender_PreviousNextSelector_TemplateContainer.
            Q<VisualElement>("ChevronRight_TemplateContainer");
        UI_Utilities.Fix_PreviousNextSelector_Dimentions(gender_PreviousNextSelector_TemplateContainer);
        #endregion

        #region Age
        age_LabeledSliderInt_TemplateContainer = demographics_ScrollView.
            Q<VisualElement>("Age_LabeledSliderInt_TemplateContainer");
        age_Text_Label = age_LabeledSliderInt_TemplateContainer.
            Q<Label>("Text_Label");
        age_MinusButton_TemplateContainer = age_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("MinusButton_TemplateContainer");
        age_InvisibleForeground_VisualElement = age_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("InvisibleForeground_VisualElement");
        age_InvisibleForeground_VisualElement.style.width =
            Length.Percent(DemographicsData.currentAge);
        age_PlusButton_TemplateContainer = age_LabeledSliderInt_TemplateContainer.
            Q<VisualElement>("PlusButton_TemplateContainer");
        UI_Utilities.Fix_LabeledSliderInt_Dimentions(age_LabeledSliderInt_TemplateContainer);
        #endregion

        #region EducationLevel
        educationLevel_SingleSelection_TemplateContainer =
            demographics_ScrollView.Q<VisualElement>("EducationLevel_SingleSelection_TemplateContainer");
        educationLevel_Parent_VisualElement =
            educationLevel_SingleSelection_TemplateContainer.Q<VisualElement>("Parent_VisualElement");
        educationLevel_WhatAmI_Label = educationLevel_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");
        educationLevel_OptionsChackMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(educationLevel_SingleSelection_TemplateContainer,
            LanguageTextsData.educationLevelList.Count);
        UI_Utilities.Fill_SingleSelection(educationLevel_SingleSelection_TemplateContainer,
            LanguageTextsData.educationLevelList, educationLevel_OptionsChackMarks);

        educationLevel_OptionsChackMarks[DemographicsData.currentEducationLevelIndex]
            .Q<VisualElement>("Foreground_VisualElement").style.display = DisplayStyle.Flex;
        #endregion


        #region SaveButton
        saveButton_TemplateContainer = demographics_ScrollView.Q<VisualElement>("SaveButton_TemplateContainer");
        saveButton_Label = saveButton_TemplateContainer.Q<Label>();
        Fix_SaveButton_Dimentions(saveButton_TemplateContainer);
        #endregion

        InitializeUI();
    }


    private void InitializeUI()
    {
        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();
    }


    #region UI Utilities

    private void Fix_SaveButton_Dimentions(VisualElement saveButton)
    {
        saveButton.style.width = Length.Percent(20);
        saveButton.style.height = Screen.width / 25f;
    }

    #endregion


    #region Functionality

    private void AddFunctionality()
    {
        //Gender
        gender_ChevronLeft_TemplateContainer.RegisterCallback<ClickEvent>(OnGender_ChevronLeftSelected);
        gender_ChevronRight_TemplateContainer.RegisterCallback<ClickEvent>(OnGender_ChevronRightSelected);

        //Age
        age_MinusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnAge_MinusButtonSelected);
        age_PlusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnAge_PlusButtonSelected);

        //SaveButton
        saveButton_TemplateContainer.RegisterCallback<ClickEvent>(OnSaveButtonSelcted);

        //EducationLevel
        for (int i = 0; i < educationLevel_OptionsChackMarks.Count; i++)
        {
            educationLevel_OptionsChackMarks[i].RegisterCallback<ClickEvent>(OnEducationLevelSelected);
        }
    }

    private void RemoveFunctionality()
    {
        //Gender
        gender_ChevronLeft_TemplateContainer.UnregisterCallback<ClickEvent>(OnGender_ChevronLeftSelected);
        gender_ChevronRight_TemplateContainer.UnregisterCallback<ClickEvent>(OnGender_ChevronRightSelected);

        //Age
        age_MinusButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnAge_MinusButtonSelected);
        age_PlusButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnAge_PlusButtonSelected);

        //SaveButton
        saveButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnSaveButtonSelcted);

        //EducationLevel
        for (int i = 0; i < educationLevel_OptionsChackMarks.Count; i++)
        {
            educationLevel_OptionsChackMarks[i].UnregisterCallback<ClickEvent>(OnEducationLevelSelected);
        }
    }

    #region Gender
    private void OnGender_ChevronLeftSelected(ClickEvent clickEvent)
    {
        DemographicsData.currentGenderIndex--;
        if (DemographicsData.currentGenderIndex < 0)
        {
            DemographicsData.currentGenderIndex = LanguageTextsData.genderList.Count - 1;
        }
        gender_Text_Label.text = LanguageTextsData.genderList[DemographicsData.currentGenderIndex]
            .ListString[SettingsData.currentLanguageIndex];
    }

    private void OnGender_ChevronRightSelected(ClickEvent clickEvent)
    {
        DemographicsData.currentGenderIndex++;
        if (DemographicsData.currentGenderIndex >= LanguageTextsData.genderList.Count)
        {
            DemographicsData.currentGenderIndex = 0;
        }
        gender_Text_Label.text = LanguageTextsData.genderList[DemographicsData.currentGenderIndex]
            .ListString[SettingsData.currentLanguageIndex];
    }
    #endregion

    #region Age

    private void OnAge_MinusButtonSelected(ClickEvent clickEvent)
    {
        DemographicsData.currentAge -= 1;
        if (DemographicsData.currentAge < 1)
        {
            DemographicsData.currentAge = 1;
        }
        age_Text_Label.text = LanguageTextsData.age[SettingsData.currentLanguageIndex] +
            DemographicsData.currentAge;
        age_InvisibleForeground_VisualElement.style.width =
            Length.Percent(DemographicsData.currentAge);
    }

    private void OnAge_PlusButtonSelected(ClickEvent clickEvent)
    {
        DemographicsData.currentAge += 1;
        if (DemographicsData.currentAge > 100)
        {
            DemographicsData.currentAge = 100;
        }
        age_Text_Label.text = LanguageTextsData.age[SettingsData.currentLanguageIndex] +
            DemographicsData.currentAge;
        age_InvisibleForeground_VisualElement.style.width =
            Length.Percent(DemographicsData.currentAge);
    }

    #endregion

    #region SaveButton
    private void OnSaveButtonSelcted(ClickEvent clickEvent)
    {
        Demographics_SaveSystem.Save_Demographics();
    }
    #endregion

    #region EducationLevel
    private void OnEducationLevelSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < educationLevel_OptionsChackMarks.Count; i++)
        {
            educationLevel_OptionsChackMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        DemographicsData.currentEducationLevelIndex = int.Parse(visualElement.name);
        Debug.Log(DemographicsData.currentEducationLevelIndex);//////////////////////////Here
    }
    #endregion

    #endregion


    #region Events Manager

    private void OnLanguageChanged()
    {
        #region gender_Text_Label
        gender_Text_Label.text = LanguageTextsData.genderList[DemographicsData.currentGenderIndex]
            .ListString[SettingsData.currentLanguageIndex];
        gender_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        gender_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region age_Text_Label
        age_Text_Label.text = age_Text_Label.text = LanguageTextsData.age[SettingsData.currentLanguageIndex] +
            DemographicsData.currentAge;
        age_Text_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        age_Text_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region educationLevel_WhatAmI_Label
        educationLevel_WhatAmI_Label.text = LanguageTextsData.educationLevel[SettingsData.currentLanguageIndex];
        educationLevel_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        educationLevel_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion



        #region saveButton_Label
        saveButton_Label.text = LanguageTextsData.save[SettingsData.currentLanguageIndex];
        saveButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        saveButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region gender_Text_Label
        gender_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region age_Text_Label
        age_Text_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region educationLevel_WhatAmI_Label
        educationLevel_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion


        #region saveButton_Label
        saveButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion


}

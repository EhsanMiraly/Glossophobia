using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class GiveDemographicsPage : MonoBehaviour
{
    PanelRenderer panelRenderer;
    DemographicsPage demographicsPage;

    VisualElement giveDemographicsPage_VisualElement;
    ScrollView giveDemographics_ScrollView;


    #region Gender
    VisualElement gender_SingleSelection_TemplateContainer;
    Label gender_WhatAmI_Label;

    List<VisualElement> gender_OptionsLabels;
    List<VisualElement> gender_OptionsCheckMarks;
    #endregion

    #region AgeGroup
    VisualElement ageGroup_SingleSelection_TemplateContainer;
    Label ageGroup_WhatAmI_Label;

    List<VisualElement> ageGroup_OptionsLabels;
    List<VisualElement> ageGroup_OptionsCheckMarks;
    #endregion


    #region EducationLevel
    VisualElement educationLevel_SingleSelection_TemplateContainer;
    Label educationLevel_WhatAmI_Label;

    List<VisualElement> educationLevel_OptionsLabels;
    List<VisualElement> educationLevel_OptionsCheckMarks;
    #endregion

    #region FieldOfStudy
    VisualElement fieldOfStudy_SingleSelection_TemplateContainer;
    Label fieldOfStudy_WhatAmI_Label;

    List<VisualElement> fieldOfStudy_OptionsLabels;
    List<VisualElement> fieldOfStudy_OptionsCheckMarks;
    #endregion

    #region Job
    VisualElement job_SingleSelection_TemplateContainer;
    Label job_WhatAmI_Label;

    List<VisualElement> job_OptionsLabels;
    List<VisualElement> job_OptionsCheckMarks;
    #endregion

    #region LevelOfExperience
    VisualElement levelOfExperience_SingleSelection_TemplateContainer;
    Label levelOfExperience_WhatAmI_Label;

    List<VisualElement> levelOfExperience_OptionsLabels;
    List<VisualElement> levelOfExperience_OptionsCheckMarks;
    #endregion

    #region LevelOfNeed
    VisualElement levelOfNeed_SingleSelection_TemplateContainer;
    Label levelOfNeed_WhatAmI_Label;

    List<VisualElement> levelOfNeed_OptionsLabels;
    List<VisualElement> levelOfNeed_OptionsCheckMarks;
    #endregion

    #region LevelOfAnxiety
    VisualElement levelOfAnxiety_SingleSelection_TemplateContainer;
    Label levelOfAnxiety_WhatAmI_Label;

    List<VisualElement> levelOfAnxiety_OptionsLabels;
    List<VisualElement> levelOfAnxiety_OptionsCheckMarks;
    #endregion

    #region FormalTraining
    VisualElement formalTraining_SingleSelection_TemplateContainer;
    Label formalTraining_WhatAmI_Label;

    List<VisualElement> formalTraining_OptionsLabels;
    List<VisualElement> formalTraining_OptionsCheckMarks;
    #endregion

    #region TakingMedication
    VisualElement takingMedication_SingleSelection_TemplateContainer;
    Label takingMedication_WhatAmI_Label;

    List<VisualElement> takingMedication_OptionsLabels;
    List<VisualElement> takingMedication_OptionsCheckMarks;
    #endregion

    #region Games3D
    VisualElement games3D_SingleSelection_TemplateContainer;
    Label games3D_WhatAmI_Label;

    List<VisualElement> games3D_OptionsLabels;
    List<VisualElement> games3D_OptionsCheckMarks;
    #endregion

    #region SimulationGames
    VisualElement simulationGames_SingleSelection_TemplateContainer;
    Label simulationGames_WhatAmI_Label;

    List<VisualElement> simulationGames_OptionsLabels;
    List<VisualElement> simulationGames_OptionsCheckMarks;
    #endregion



    VisualElement saveButton_TemplateContainer;
    Label saveButton_Label;


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
        giveDemographicsPage_VisualElement = root.Q<VisualElement>("GiveDemographicsPage_VisualElement");
        giveDemographics_ScrollView =
            giveDemographicsPage_VisualElement.Q<ScrollView>("GiveDemographics_ScrollView");
        UI_Utilities.Initialize_ScrollView(giveDemographics_ScrollView);


        #region Gender
        gender_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("Gender_SingleSelection_TemplateContainer");
        gender_WhatAmI_Label = gender_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        gender_OptionsLabels = new List<VisualElement>();
        gender_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(gender_SingleSelection_TemplateContainer,
            LanguageTextsData.genderList.Count);
        UI_Utilities.Fill_SingleSelection(gender_SingleSelection_TemplateContainer,
            LanguageTextsData.genderList, gender_OptionsLabels, gender_OptionsCheckMarks);
        #endregion

        #region AgeGroup
        ageGroup_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("AgeGroup_SingleSelection_TemplateContainer");
        ageGroup_WhatAmI_Label = ageGroup_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        ageGroup_OptionsLabels = new List<VisualElement>();
        ageGroup_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(ageGroup_SingleSelection_TemplateContainer,
            LanguageTextsData.ageGroupList.Count);
        UI_Utilities.Fill_SingleSelection(ageGroup_SingleSelection_TemplateContainer,
            LanguageTextsData.ageGroupList, ageGroup_OptionsLabels, ageGroup_OptionsCheckMarks);
        #endregion


        #region EducationLevel
        educationLevel_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("EducationLevel_SingleSelection_TemplateContainer");
        educationLevel_WhatAmI_Label = educationLevel_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        educationLevel_OptionsLabels = new List<VisualElement>();
        educationLevel_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(educationLevel_SingleSelection_TemplateContainer,
            LanguageTextsData.educationLevelList.Count);
        UI_Utilities.Fill_SingleSelection(educationLevel_SingleSelection_TemplateContainer,
            LanguageTextsData.educationLevelList, educationLevel_OptionsLabels, educationLevel_OptionsCheckMarks);
        #endregion

        #region FieldOfStudy
        fieldOfStudy_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("FieldOfStudy_SingleSelection_TemplateContainer");
        fieldOfStudy_WhatAmI_Label = fieldOfStudy_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        fieldOfStudy_OptionsLabels = new List<VisualElement>();
        fieldOfStudy_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(fieldOfStudy_SingleSelection_TemplateContainer,
            LanguageTextsData.fieldOfStudyList.Count);
        UI_Utilities.Fill_SingleSelection(fieldOfStudy_SingleSelection_TemplateContainer,
            LanguageTextsData.fieldOfStudyList, fieldOfStudy_OptionsLabels, fieldOfStudy_OptionsCheckMarks);
        #endregion

        #region Job
        job_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("Job_SingleSelection_TemplateContainer");
        job_WhatAmI_Label = job_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        job_OptionsLabels = new List<VisualElement>();
        job_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(job_SingleSelection_TemplateContainer,
            LanguageTextsData.jobList.Count);
        UI_Utilities.Fill_SingleSelection(job_SingleSelection_TemplateContainer,
            LanguageTextsData.jobList, job_OptionsLabels, job_OptionsCheckMarks);
        #endregion

        #region LevelOfExperience
        levelOfExperience_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("LevelOfExperience_SingleSelection_TemplateContainer");
        levelOfExperience_WhatAmI_Label =
            levelOfExperience_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        levelOfExperience_OptionsLabels = new List<VisualElement>();
        levelOfExperience_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(levelOfExperience_SingleSelection_TemplateContainer,
            LanguageTextsData.veryLowToVeryHigh.Count);
        UI_Utilities.Fill_SingleSelection(levelOfExperience_SingleSelection_TemplateContainer,
            LanguageTextsData.veryLowToVeryHigh, levelOfExperience_OptionsLabels,
            levelOfExperience_OptionsCheckMarks);
        #endregion

        #region LevelOfNeed
        levelOfNeed_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("LevelOfNeed_SingleSelection_TemplateContainer");
        levelOfNeed_WhatAmI_Label = levelOfNeed_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        levelOfNeed_OptionsLabels = new List<VisualElement>();
        levelOfNeed_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(levelOfNeed_SingleSelection_TemplateContainer,
            LanguageTextsData.veryLowToVeryHigh.Count);
        UI_Utilities.Fill_SingleSelection(levelOfNeed_SingleSelection_TemplateContainer,
            LanguageTextsData.veryLowToVeryHigh, levelOfNeed_OptionsLabels, levelOfNeed_OptionsCheckMarks);
        #endregion

        #region LevelOfAnxiety
        levelOfAnxiety_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("LevelOfAnxiety_SingleSelection_TemplateContainer");
        levelOfAnxiety_WhatAmI_Label = levelOfAnxiety_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        levelOfAnxiety_OptionsLabels = new List<VisualElement>();
        levelOfAnxiety_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(levelOfAnxiety_SingleSelection_TemplateContainer,
            LanguageTextsData.veryLowToVeryHigh.Count);
        UI_Utilities.Fill_SingleSelection(levelOfAnxiety_SingleSelection_TemplateContainer,
            LanguageTextsData.veryLowToVeryHigh, levelOfAnxiety_OptionsLabels, levelOfAnxiety_OptionsCheckMarks);
        #endregion

        #region FormalTraining
        formalTraining_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("FormalTraining_SingleSelection_TemplateContainer");
        formalTraining_WhatAmI_Label = formalTraining_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        formalTraining_OptionsLabels = new List<VisualElement>();
        formalTraining_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(formalTraining_SingleSelection_TemplateContainer,
            LanguageTextsData.yesNo.Count);
        UI_Utilities.Fill_SingleSelection(formalTraining_SingleSelection_TemplateContainer,
            LanguageTextsData.yesNo, formalTraining_OptionsLabels, formalTraining_OptionsCheckMarks);
        #endregion

        #region TakingMedication
        takingMedication_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("TakingMedication_SingleSelection_TemplateContainer");
        takingMedication_WhatAmI_Label =
            takingMedication_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        takingMedication_OptionsLabels = new List<VisualElement>();
        takingMedication_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(takingMedication_SingleSelection_TemplateContainer,
            LanguageTextsData.yesNo.Count);
        UI_Utilities.Fill_SingleSelection(takingMedication_SingleSelection_TemplateContainer,
            LanguageTextsData.yesNo, takingMedication_OptionsLabels, takingMedication_OptionsCheckMarks);
        #endregion

        #region Games3D
        games3D_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("Games3D_SingleSelection_TemplateContainer");
        games3D_WhatAmI_Label = games3D_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        games3D_OptionsLabels = new List<VisualElement>();
        games3D_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(games3D_SingleSelection_TemplateContainer,
            LanguageTextsData.veryLowToVeryHigh.Count);
        UI_Utilities.Fill_SingleSelection(games3D_SingleSelection_TemplateContainer,
            LanguageTextsData.veryLowToVeryHigh, games3D_OptionsLabels, games3D_OptionsCheckMarks);
        #endregion

        #region SimulationGames
        simulationGames_SingleSelection_TemplateContainer =
            giveDemographics_ScrollView.Q<VisualElement>("SimulationGames_SingleSelection_TemplateContainer");
        simulationGames_WhatAmI_Label = simulationGames_SingleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        simulationGames_OptionsLabels = new List<VisualElement>();
        simulationGames_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(simulationGames_SingleSelection_TemplateContainer,
            LanguageTextsData.veryLowToVeryHigh.Count);
        UI_Utilities.Fill_SingleSelection(simulationGames_SingleSelection_TemplateContainer,
            LanguageTextsData.veryLowToVeryHigh, simulationGames_OptionsLabels, simulationGames_OptionsCheckMarks);
        #endregion


        #region SaveButton
        saveButton_TemplateContainer = giveDemographics_ScrollView.Q<VisualElement>("SaveButton_TemplateContainer");
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
        for (int i = 0; i < gender_OptionsCheckMarks.Count; i++)
        {
            gender_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnGenderSelected);
        }

        //AgeGroup
        for (int i = 0; i < ageGroup_OptionsCheckMarks.Count; i++)
        {
            ageGroup_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnAgeGroupSelected);
        }

        //EducationLevel
        for (int i = 0; i < educationLevel_OptionsCheckMarks.Count; i++)
        {
            educationLevel_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnEducationLevelSelected);
        }

        //FieldOfStudy
        for (int i = 0; i < fieldOfStudy_OptionsCheckMarks.Count; i++)
        {
            fieldOfStudy_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnFieldOfStudySelected);
        }

        //Job
        for (int i = 0; i < job_OptionsCheckMarks.Count; i++)
        {
            job_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnJobSelected);
        }

        //LevelOfExperience
        for (int i = 0; i < levelOfExperience_OptionsCheckMarks.Count; i++)
        {
            levelOfExperience_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnLevelOfExperienceSelected);
        }

        //LevelOfNeed
        for (int i = 0; i < levelOfNeed_OptionsCheckMarks.Count; i++)
        {
            levelOfNeed_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnLevelOfNeedSelected);
        }

        //LevelOfAnxiety
        for (int i = 0; i < levelOfAnxiety_OptionsCheckMarks.Count; i++)
        {
            levelOfAnxiety_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnLevelOfAnxietySelected);
        }

        //FormalTraining
        for (int i = 0; i < formalTraining_OptionsCheckMarks.Count; i++)
        {
            formalTraining_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnFormalTrainingSelected);
        }

        //TakingMedication
        for (int i = 0; i < takingMedication_OptionsCheckMarks.Count; i++)
        {
            takingMedication_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnTakingMedicationSelected);
        }

        //Games3D
        for (int i = 0; i < games3D_OptionsCheckMarks.Count; i++)
        {
            games3D_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnGames3DSelected);
        }

        //SimulationGames
        for (int i = 0; i < simulationGames_OptionsCheckMarks.Count; i++)
        {
            simulationGames_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnSimulationGamesSelected);
        }

        //SaveButton
        saveButton_TemplateContainer.RegisterCallback<ClickEvent>(OnSaveButtonSelcted);
    }

    private void RemoveFunctionality()
    {
        //Gender
        for (int i = 0; i < gender_OptionsCheckMarks.Count; i++)
        {
            gender_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnGenderSelected);
        }

        //AgeGroup
        for (int i = 0; i < ageGroup_OptionsCheckMarks.Count; i++)
        {
            ageGroup_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnAgeGroupSelected);
        }

        //EducationLevel
        for (int i = 0; i < educationLevel_OptionsCheckMarks.Count; i++)
        {
            educationLevel_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnEducationLevelSelected);
        }

        //FieldOfStudy
        for (int i = 0; i < fieldOfStudy_OptionsCheckMarks.Count; i++)
        {
            fieldOfStudy_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnFieldOfStudySelected);
        }

        //Job
        for (int i = 0; i < job_OptionsCheckMarks.Count; i++)
        {
            job_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnJobSelected);
        }

        //LevelOfExperience
        for (int i = 0; i < levelOfExperience_OptionsCheckMarks.Count; i++)
        {
            levelOfExperience_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnLevelOfExperienceSelected);
        }

        //LevelOfNeed
        for (int i = 0; i < levelOfNeed_OptionsCheckMarks.Count; i++)
        {
            levelOfNeed_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnLevelOfNeedSelected);
        }

        //LevelOfAnxiety
        for (int i = 0; i < levelOfAnxiety_OptionsCheckMarks.Count; i++)
        {
            levelOfAnxiety_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnLevelOfAnxietySelected);
        }

        //FormalTraining
        for (int i = 0; i < formalTraining_OptionsCheckMarks.Count; i++)
        {
            formalTraining_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnFormalTrainingSelected);
        }

        //TakingMedication
        for (int i = 0; i < takingMedication_OptionsCheckMarks.Count; i++)
        {
            takingMedication_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnTakingMedicationSelected);
        }

        //Games3D
        for (int i = 0; i < games3D_OptionsCheckMarks.Count; i++)
        {
            games3D_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnGames3DSelected);
        }

        //SimulationGames
        for (int i = 0; i < simulationGames_OptionsCheckMarks.Count; i++)
        {
            simulationGames_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnSimulationGamesSelected);
        }

        //SaveButton
        saveButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnSaveButtonSelcted);
    }


    #region Gender
    private void OnGenderSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < gender_OptionsCheckMarks.Count; i++)
        {
            gender_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.genderIndex = int.Parse(visualElement.name);
    }
    #endregion


    #region AgeGroup
    private void OnAgeGroupSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < ageGroup_OptionsCheckMarks.Count; i++)
        {
            ageGroup_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.ageGroupIndex = int.Parse(visualElement.name);
    }
    #endregion

    #region EducationLevel
    private void OnEducationLevelSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < educationLevel_OptionsCheckMarks.Count; i++)
        {
            educationLevel_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.educationLevelIndex = int.Parse(visualElement.name);
    }
    #endregion


    #region FieldOfStudy
    private void OnFieldOfStudySelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < fieldOfStudy_OptionsCheckMarks.Count; i++)
        {
            fieldOfStudy_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.fieldOfStudyIndex = int.Parse(visualElement.name);
    }
    #endregion

    #region Job
    private void OnJobSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < job_OptionsCheckMarks.Count; i++)
        {
            job_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.jobIndex = int.Parse(visualElement.name);
    }
    #endregion

    #region LevelOfExperience
    private void OnLevelOfExperienceSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < levelOfExperience_OptionsCheckMarks.Count; i++)
        {
            levelOfExperience_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.levelOfExperienceIndex = int.Parse(visualElement.name);
    }
    #endregion

    #region LevelOfNeed
    private void OnLevelOfNeedSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < levelOfNeed_OptionsCheckMarks.Count; i++)
        {
            levelOfNeed_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.levelOfNeedIndex = int.Parse(visualElement.name);
    }
    #endregion

    #region LevelOfAnxiety
    private void OnLevelOfAnxietySelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < levelOfAnxiety_OptionsCheckMarks.Count; i++)
        {
            levelOfAnxiety_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.levelOfAnxietyIndex = int.Parse(visualElement.name);
    }
    #endregion

    #region FormalTraining
    private void OnFormalTrainingSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < formalTraining_OptionsCheckMarks.Count; i++)
        {
            formalTraining_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.formalTrainingIndex = int.Parse(visualElement.name);
    }
    #endregion

    #region TakingMedication
    private void OnTakingMedicationSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < takingMedication_OptionsCheckMarks.Count; i++)
        {
            takingMedication_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.takingMedicationIndex = int.Parse(visualElement.name);
    }
    #endregion

    #region Games3D
    private void OnGames3DSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < games3D_OptionsCheckMarks.Count; i++)
        {
            games3D_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.games3DIndex = int.Parse(visualElement.name);
    }
    #endregion

    #region SimulationGames
    private void OnSimulationGamesSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < simulationGames_OptionsCheckMarks.Count; i++)
        {
            simulationGames_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        demographicsPage.demographics.simulationGamesIndex = int.Parse(visualElement.name);
    }
    #endregion

    #region SaveButton
    private async void OnSaveButtonSelcted(ClickEvent clickEvent)
    {
        if (demographicsPage.demographics.IsEveryThingSet())
        {
            EventsManager.InvokeOnSetDemographics();
            await FireStoreManager.SaveDemographics(demographicsPage.demographics);
            demographicsPage.SetPageActive(demographicsPage.changeDemographicsPage_VisualElement);
        }
        else
        {
            AnswerEveryThingWindow_PopUp answerEveryThingWindow_PopUp =
                new AnswerEveryThingWindow_PopUp(new GameObject());
        }
    }
    #endregion

    #endregion



    #region Events Manager

    private void OnLanguageChanged()
    {
        #region Gender
        gender_WhatAmI_Label.text = LanguageTextsData.gender[SettingsData.currentLanguageIndex];
        gender_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        gender_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < gender_OptionsLabels.Count; i++)
        {
            Label label = gender_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.genderList[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion

        #region AgeGroup
        ageGroup_WhatAmI_Label.text = LanguageTextsData.ageGroup[SettingsData.currentLanguageIndex];
        ageGroup_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        ageGroup_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < ageGroup_OptionsLabels.Count; i++)
        {
            Label label = ageGroup_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.ageGroupList[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion


        #region EducationLevel
        educationLevel_WhatAmI_Label.text = LanguageTextsData.educationLevel[SettingsData.currentLanguageIndex];
        educationLevel_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        educationLevel_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < educationLevel_OptionsLabels.Count; i++)
        {
            Label label = educationLevel_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.educationLevelList[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion

        #region FieldOfStudy
        fieldOfStudy_WhatAmI_Label.text = LanguageTextsData.fieldOfStudy[SettingsData.currentLanguageIndex];
        fieldOfStudy_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        fieldOfStudy_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < fieldOfStudy_OptionsLabels.Count; i++)
        {
            Label label = fieldOfStudy_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.fieldOfStudyList[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion

        #region Job
        job_WhatAmI_Label.text = LanguageTextsData.job[SettingsData.currentLanguageIndex];
        job_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        job_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < job_OptionsLabels.Count; i++)
        {
            Label label = job_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.jobList[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion

        #region LevelOfExperience
        levelOfExperience_WhatAmI_Label.text =
            LanguageTextsData.levelOfExperience[SettingsData.currentLanguageIndex];
        levelOfExperience_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        levelOfExperience_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < levelOfExperience_OptionsLabels.Count; i++)
        {
            Label label = levelOfExperience_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.veryLowToVeryHigh[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion

        #region LevelOfNeed
        levelOfNeed_WhatAmI_Label.text = LanguageTextsData.levelOfNeed[SettingsData.currentLanguageIndex];
        levelOfNeed_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        levelOfNeed_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < levelOfNeed_OptionsLabels.Count; i++)
        {
            Label label = levelOfNeed_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.veryLowToVeryHigh[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion

        #region LevelOfAnxiety
        levelOfAnxiety_WhatAmI_Label.text = LanguageTextsData.levelOfAnxiety[SettingsData.currentLanguageIndex];
        levelOfAnxiety_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        levelOfAnxiety_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < levelOfAnxiety_OptionsLabels.Count; i++)
        {
            Label label = levelOfAnxiety_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.veryLowToVeryHigh[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion

        #region FormalTraining
        formalTraining_WhatAmI_Label.text = LanguageTextsData.formalTraining[SettingsData.currentLanguageIndex];
        formalTraining_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        formalTraining_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < formalTraining_OptionsLabels.Count; i++)
        {
            Label label = formalTraining_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.yesNo[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion

        #region TakingMedication
        takingMedication_WhatAmI_Label.text = LanguageTextsData.takingMedication[SettingsData.currentLanguageIndex];
        takingMedication_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        takingMedication_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < takingMedication_OptionsLabels.Count; i++)
        {
            Label label = takingMedication_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.yesNo[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion

        #region Games3D
        games3D_WhatAmI_Label.text = LanguageTextsData.games3D[SettingsData.currentLanguageIndex];
        games3D_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        games3D_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < games3D_OptionsLabels.Count; i++)
        {
            Label label = games3D_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.veryLowToVeryHigh[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion

        #region SimulationGames
        simulationGames_WhatAmI_Label.text = LanguageTextsData.simulationGames[SettingsData.currentLanguageIndex];
        simulationGames_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        simulationGames_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < simulationGames_OptionsLabels.Count; i++)
        {
            Label label = simulationGames_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.veryLowToVeryHigh[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
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
        #region Gender
        gender_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < gender_OptionsLabels.Count; i++)
        {
            Label label = gender_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

        #region AgeGroup
        ageGroup_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < ageGroup_OptionsLabels.Count; i++)
        {
            Label label = ageGroup_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

        #region EducationLevel
        educationLevel_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < educationLevel_OptionsLabels.Count; i++)
        {
            Label label = educationLevel_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

        #region FieldOfStudy
        fieldOfStudy_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < fieldOfStudy_OptionsLabels.Count; i++)
        {
            Label label = fieldOfStudy_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

        #region Job
        job_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < job_OptionsLabels.Count; i++)
        {
            Label label = job_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

        #region LevelOfExperience
        levelOfExperience_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < levelOfExperience_OptionsLabels.Count; i++)
        {
            Label label = levelOfExperience_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

        #region LevelOfNeed
        levelOfNeed_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < levelOfNeed_OptionsLabels.Count; i++)
        {
            Label label = levelOfNeed_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

        #region LevelOfAnxiety
        levelOfAnxiety_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < levelOfAnxiety_OptionsLabels.Count; i++)
        {
            Label label = levelOfAnxiety_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

        #region FormalTraining
        formalTraining_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < formalTraining_OptionsLabels.Count; i++)
        {
            Label label = formalTraining_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

        #region TakingMedication
        takingMedication_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < takingMedication_OptionsLabels.Count; i++)
        {
            Label label = takingMedication_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

        #region Games3D
        games3D_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < games3D_OptionsLabels.Count; i++)
        {
            Label label = games3D_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

        #region SimulationGames
        simulationGames_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < simulationGames_OptionsLabels.Count; i++)
        {
            Label label = simulationGames_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion


        #region saveButton_Label
        saveButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion

}

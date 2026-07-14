using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestionsPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    PRPSAPage PRPSAPage;

    VisualElement questionsPage_VisualElement;


    Label question_Label;

    VisualElement singleSelection_TemplateContainer;
    Label singleSelection_WhatAmI_Label;
    List<VisualElement> singleSelection_OptionsLabels;
    List<VisualElement> singleSelection_OptionsCheckMarks;

    VisualElement lastQuestionButton_TemplateContainer;
    Label lastQuestionButton_Label;
    VisualElement nextQuestionButton_TemplateContainer;
    Label nextQuestionButton_Label;


    int currentQuestionIndex;



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
        questionsPage_VisualElement = root.Q<VisualElement>("QuestionsPage_VisualElement");

        question_Label = questionsPage_VisualElement.Q<Label>("Question_Label");
        singleSelection_TemplateContainer =
            questionsPage_VisualElement.Q<VisualElement>("SingleSelection_TemplateContainer");
        singleSelection_WhatAmI_Label = singleSelection_TemplateContainer.Q<Label>("WhatAmI_Label");

        singleSelection_OptionsLabels = new List<VisualElement>();
        singleSelection_OptionsCheckMarks = new List<VisualElement>();

        UI_Utilities.Fix_SingleSelection_Dimentions(singleSelection_TemplateContainer,
            LanguageTextsData.stronglyDisagreeToStronglyAgree.Count);
        UI_Utilities.Fill_SingleSelection(singleSelection_TemplateContainer,
            LanguageTextsData.stronglyDisagreeToStronglyAgree, singleSelection_OptionsLabels,
                singleSelection_OptionsCheckMarks);

        lastQuestionButton_TemplateContainer =
            questionsPage_VisualElement.Q<VisualElement>("LastQuestionButton_TemplateContainer");
        lastQuestionButton_Label = lastQuestionButton_TemplateContainer.Q<Label>("Text_Label");
        nextQuestionButton_TemplateContainer =
            questionsPage_VisualElement.Q<VisualElement>("NextQuestionButton_TemplateContainer");
        nextQuestionButton_Label = nextQuestionButton_TemplateContainer.Q<Label>("Text_Label");


        InitializeUI();
    }

    private void InitializeUI()
    {
        currentQuestionIndex = 0;

        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();
    }


    #region Functionality

    private void AddFunctionality()
    {
        //singleSelection
        for (int i = 0; i < singleSelection_OptionsCheckMarks.Count; i++)
        {
            singleSelection_OptionsCheckMarks[i].RegisterCallback<ClickEvent>(OnSingleSelectionSelected);
        }

        //LastQuestionButton
        lastQuestionButton_TemplateContainer.RegisterCallback<ClickEvent>(OnLastQuestionButtonSelected);

        //NextQuestionButton
        nextQuestionButton_TemplateContainer.RegisterCallback<ClickEvent>(OnNextQuestionButtonSelected);
    }

    private void RemoveFunctionality()
    {
        //singleSelection
        for (int i = 0; i < singleSelection_OptionsCheckMarks.Count; i++)
        {
            singleSelection_OptionsCheckMarks[i].UnregisterCallback<ClickEvent>(OnSingleSelectionSelected);
        }

        //LastQuestionButton
        lastQuestionButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnLastQuestionButtonSelected);

        //NextQuestionButton
        nextQuestionButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnNextQuestionButtonSelected);
    }

    private void OnSingleSelectionSelected(ClickEvent clickEvent)
    {
        for (int i = 0; i < singleSelection_OptionsCheckMarks.Count; i++)
        {
            singleSelection_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        visualElement.Q<VisualElement>("Foreground_VisualElement")
            .style.display = DisplayStyle.Flex;
        PRPSA_BeforeData.currentAnswers[currentQuestionIndex] = int.Parse(visualElement.name);
    }

    private void OnLastQuestionButtonSelected(ClickEvent clickEvent)
    {
        currentQuestionIndex--;
        if (currentQuestionIndex < 0)
        {
            currentQuestionIndex = 0;
        }
        question_Label.text = LanguageTextsData.questions[currentQuestionIndex].
                                ListString[SettingsData.currentLanguageIndex];
        for (int i = 0; i < singleSelection_OptionsCheckMarks.Count; i++)
        {
            singleSelection_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        singleSelection_OptionsCheckMarks[PRPSA_BeforeData.currentAnswers[currentQuestionIndex]].
                Q<VisualElement>("Foreground_VisualElement").style.display = DisplayStyle.Flex;
    }

    private void OnNextQuestionButtonSelected(ClickEvent clickEvent)
    {
        if (PRPSA_BeforeData.currentAnswers[currentQuestionIndex] == -1)
        {
            return;
        }

        currentQuestionIndex++;
        if (currentQuestionIndex == LanguageTextsData.questions.Count - 1)
        {
            //Change to finish then if clicked show change page;
        }

        if (currentQuestionIndex > LanguageTextsData.questions.Count - 1)
        {
            currentQuestionIndex = LanguageTextsData.questions.Count - 1;
            //show change page
        }

        question_Label.text = LanguageTextsData.questions[currentQuestionIndex].
                        ListString[SettingsData.currentLanguageIndex];
        for (int i = 0; i < singleSelection_OptionsCheckMarks.Count; i++)
        {
            singleSelection_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }

        if (PRPSA_BeforeData.currentAnswers[currentQuestionIndex] != -1)
        {
            singleSelection_OptionsCheckMarks[PRPSA_BeforeData.currentAnswers[currentQuestionIndex]].
                Q<VisualElement>("Foreground_VisualElement").style.display = DisplayStyle.Flex;
        }

    }

    #endregion




    #region Events Manager

    private void OnLanguageChanged()
    {
        #region question_Label
        question_Label.text = LanguageTextsData.questions[currentQuestionIndex].
                                ListString[SettingsData.currentLanguageIndex];
        question_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        question_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion



        #region SingleSelection
        singleSelection_WhatAmI_Label.text = LanguageTextsData.yourChoice[SettingsData.currentLanguageIndex];
        singleSelection_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        singleSelection_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        for (int i = 0; i < singleSelection_OptionsLabels.Count; i++)
        {
            Label label = singleSelection_OptionsLabels[i].Q<Label>();

            label.text = LanguageTextsData.stronglyDisagreeToStronglyAgree[i].ListString[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region explain_Label
        question_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion


        #region SingleSelection
        singleSelection_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        for (int i = 0; i < singleSelection_OptionsLabels.Count; i++)
        {
            Label label = singleSelection_OptionsLabels[i].Q<Label>();

            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
        #endregion

    }

    #endregion





}

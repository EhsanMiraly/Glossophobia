using System;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestionsPostTestPRPSAPage : MonoBehaviour
{
    //GameData.gameSession.postTestPRPSAIndexes[0] = -1;
    //Check And Change All
    PanelRenderer panelRenderer;

    BaselinePRPSAPage baselinePRPSAPage;

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
        baselinePRPSAPage.baselinePRPSA.baselinePRPSAIndexes[currentQuestionIndex] =
            int.Parse(visualElement.name);
    }

    private void OnLastQuestionButtonSelected(ClickEvent clickEvent)
    {
        currentQuestionIndex--;
        if (currentQuestionIndex < 0)
        {
            currentQuestionIndex = 0;
        }
        question_Label.text = LanguageTextsData.baselinePRPSAQuestions[currentQuestionIndex].
                                ListString[SettingsData.currentLanguageIndex];
        nextQuestionButton_Label.text = LanguageTextsData.next[SettingsData.currentLanguageIndex];
        for (int i = 0; i < singleSelection_OptionsCheckMarks.Count; i++)
        {
            singleSelection_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }
        if (baselinePRPSAPage.baselinePRPSA.baselinePRPSAIndexes[currentQuestionIndex] != -1)
        {
            singleSelection_OptionsCheckMarks[baselinePRPSAPage.baselinePRPSA.baselinePRPSAIndexes[currentQuestionIndex]].
                    Q<VisualElement>("Foreground_VisualElement").style.display = DisplayStyle.Flex;
        }

    }

    private async void OnNextQuestionButtonSelected(ClickEvent clickEvent)
    {
        if (baselinePRPSAPage.baselinePRPSA.baselinePRPSAIndexes[currentQuestionIndex] == -1)
        {
            return;
        }

        currentQuestionIndex++;
        if (currentQuestionIndex == LanguageTextsData.baselinePRPSAQuestions.Count - 1)
        {
            nextQuestionButton_Label.text = LanguageTextsData.finish[SettingsData.currentLanguageIndex];
        }

        if (currentQuestionIndex > LanguageTextsData.baselinePRPSAQuestions.Count - 1)
        {
            currentQuestionIndex = LanguageTextsData.baselinePRPSAQuestions.Count - 1;

            nextQuestionButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnNextQuestionButtonSelected);

            if (baselinePRPSAPage.baselinePRPSA.IsEveryThingSet())
            {
                try
                {
                    if (FirebaseAuth.DefaultInstance.CurrentUser == null)
                    {
                        new MessageWindow_PopUp(new GameObject(),
                            LanguageTextsData.thereIsSomethingWrongWithYourAccount[SettingsData.currentLanguageIndex]);
                        nextQuestionButton_TemplateContainer.RegisterCallback<ClickEvent>(OnNextQuestionButtonSelected);
                        return;
                    }

                    DocumentReference playerDocument =
                        FirebaseFirestore.DefaultInstance.Collection(FireStoreNames.players_Collection).
                            Document(FirebaseAuth.DefaultInstance.CurrentUser.UserId);

                    Dictionary<string, object> baselinePRPSA_Dictionary = new Dictionary<string, object>()
                    {
                        {
                            FireStoreNames.baselinePRPSAIndexes,
                            new List<int>(baselinePRPSAPage.baselinePRPSA.baselinePRPSAIndexes)
                        }
                    };

                    Dictionary<string, object> update = new Dictionary<string, object>()
                    {
                        { FireStoreNames.baselinePRPSA_Map, baselinePRPSA_Dictionary }
                    };

                    await playerDocument.SetAsync(update, SetOptions.MergeAll);

                    baselinePRPSAPage.SetPageActive(baselinePRPSAPage.changeBaselinePRPSAPage_VisualElement);
                    EventsManager.InvokeOnSetPRPSA_Before();
                }
                catch (FirestoreException firestoreException)
                {
                    switch (firestoreException.ErrorCode)
                    {
                        case FirestoreError.Unavailable:
                            new MessageWindow_PopUp(new GameObject(),
                                LanguageTextsData.unavailable[SettingsData.currentLanguageIndex]);
                            break;
                        case FirestoreError.DeadlineExceeded:
                            new MessageWindow_PopUp(new GameObject(),
                                LanguageTextsData.deadlineExceeded[SettingsData.currentLanguageIndex]);
                            break;
                        case FirestoreError.Unauthenticated:
                            new MessageWindow_PopUp(new GameObject(),
                                LanguageTextsData.unauthenticated[SettingsData.currentLanguageIndex]);
                            break;
                        default:
                            new MessageWindow_PopUp(new GameObject(),
                                LanguageTextsData.thereIsSomethingWrong[SettingsData.currentLanguageIndex]);
                            break;
                    }
                }
                catch
                {
                    new MessageWindow_PopUp(new GameObject(),
                        LanguageTextsData.thereIsSomethingWrong[SettingsData.currentLanguageIndex]);
                }
            }
            else
            {
                new MessageWindow_PopUp(new GameObject(),
                    LanguageTextsData.answerEveryThing[SettingsData.currentLanguageIndex]);
            }

            nextQuestionButton_TemplateContainer.RegisterCallback<ClickEvent>(OnNextQuestionButtonSelected);
        }

        question_Label.text = LanguageTextsData.baselinePRPSAQuestions[currentQuestionIndex].
                        ListString[SettingsData.currentLanguageIndex];
        for (int i = 0; i < singleSelection_OptionsCheckMarks.Count; i++)
        {
            singleSelection_OptionsCheckMarks[i].Q<VisualElement>("Foreground_VisualElement")
                .style.display = DisplayStyle.None;
        }

        if (baselinePRPSAPage.baselinePRPSA.baselinePRPSAIndexes[currentQuestionIndex] != -1)
        {
            singleSelection_OptionsCheckMarks[baselinePRPSAPage.baselinePRPSA.baselinePRPSAIndexes[currentQuestionIndex]].
                Q<VisualElement>("Foreground_VisualElement").style.display = DisplayStyle.Flex;
        }

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
        #region question_Label
        question_Label.text = LanguageTextsData.baselinePRPSAQuestions[currentQuestionIndex].
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

        #region lastQuestionButton_Label
        lastQuestionButton_Label.text = LanguageTextsData.last[SettingsData.currentLanguageIndex];
        lastQuestionButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        lastQuestionButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region nextQuestionButton_Label
        nextQuestionButton_Label.text = LanguageTextsData.next[SettingsData.currentLanguageIndex];
        nextQuestionButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        nextQuestionButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region question_Label
        question_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryBig[SettingsData.currentFontSizeIndex];
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

        #region lastQuestionButton_Label
        lastQuestionButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region nextQuestionButton_Label
        nextQuestionButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

    }

    #endregion





}

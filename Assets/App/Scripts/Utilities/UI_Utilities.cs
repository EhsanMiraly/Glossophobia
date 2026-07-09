using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Utilities
{
    public static void MouseVisible(bool state)
    {
        UnityEngine.Cursor.visible = state;

        if (state)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }


    public static void Initialize_ScrollView(ScrollView scrollView)
    {
        scrollView.style.width = Length.Percent(100);
        scrollView.style.height = Length.Percent(100);

        scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
        scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

        scrollView.touchScrollBehavior = ScrollView.TouchScrollBehavior.Elastic;

        scrollView.style.marginTop = 0;
        scrollView.style.marginRight = 0;
        scrollView.style.marginBottom = 0;
        scrollView.style.marginLeft = 0;

        scrollView.style.paddingTop = 0;
        scrollView.style.paddingRight = 0;
        scrollView.style.paddingBottom = 0;
        scrollView.style.paddingLeft = 0;
    }

    public static void Initialize_ListView(ListView listView)
    {
        listView.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;

        listView.selectionType = SelectionType.None;

        listView.style.marginTop = 0;
        listView.style.marginRight = 0;
        listView.style.marginBottom = 0;
        listView.style.marginLeft = 0;

        listView.style.paddingTop = 0;
        listView.style.paddingRight = 0;
        listView.style.paddingBottom = 0;
        listView.style.paddingLeft = 0;

        Initialize_ScrollView(listView.Q<ScrollView>());
    }

    public static void Initialize_Foldout(Foldout foldout)
    {
        foldout.value = false;
    }


    public static void Fix_PreviousNextSelector_Dimentions(VisualElement previousNextSelector)
    {
        previousNextSelector.style.width = Length.Percent(100);
        previousNextSelector.style.height = Screen.width / 20f;

        previousNextSelector.style.marginBottom = Screen.width / 200f;

        VisualElement chevronLeft_TemplateContainer =
            previousNextSelector.Q<VisualElement>("ChevronLeft_TemplateContainer");
        VisualElement chevronRight_TemplateContainer =
            previousNextSelector.Q<VisualElement>("ChevronRight_TemplateContainer");

        chevronLeft_TemplateContainer.style.width = Screen.width / 25f;
        chevronLeft_TemplateContainer.style.height = Screen.width / 25f;

        chevronRight_TemplateContainer.style.width = Screen.width / 25f;
        chevronRight_TemplateContainer.style.height = Screen.width / 25f;
    }

    public static void Fix_LabeledSliderInt_Dimentions(VisualElement labeledSliderInt)
    {
        labeledSliderInt.style.width = Length.Percent(100);
        labeledSliderInt.style.height = Screen.width / 10f;

        labeledSliderInt.style.marginBottom = Screen.width / 200f;

        VisualElement minusButton_TemplateContainer = labeledSliderInt.
            Q<VisualElement>("MinusButton_TemplateContainer");
        VisualElement plusButton_TemplateContainer = labeledSliderInt.
            Q<VisualElement>("PlusButton_TemplateContainer");

        minusButton_TemplateContainer.style.width = Screen.width / 25f;
        minusButton_TemplateContainer.style.height = Screen.width / 25f;

        plusButton_TemplateContainer.style.width = Screen.width / 25f;
        plusButton_TemplateContainer.style.height = Screen.width / 25f;
    }


    #region SingleSelection

    public static void Fix_SingleSelection_Dimentions(VisualElement singleSelection, int optionsCount)
    {
        singleSelection.style.width = Length.Percent(100);
        singleSelection.style.height = (Screen.width / 20f) * (optionsCount + 1);
        singleSelection.style.flexGrow = 1f;

        singleSelection.style.marginBottom = Screen.width / 200f;

        Label whatAmI_Label = singleSelection.Q<Label>("WhatAmI_Label");
        Fix_SingleSelection_Label_Dimentions(whatAmI_Label);
    }

    private static void Fix_SingleSelection_Label_Dimentions(VisualElement visualElement)
    {
        visualElement.style.width = Length.Percent(50);
        visualElement.style.height = Screen.width / 20f;
    }

    private static void Fix_SingleSelection_Option_Dimentions(VisualElement visualElement)
    {
        visualElement.style.width = Length.Percent(100);
        visualElement.style.height = Screen.width / 20f;
    }

    private static void Fix_OptionCheckMark_Dimentions(VisualElement visualElement)
    {
        visualElement.style.width = Screen.width / 25f;
        visualElement.style.height = Screen.width / 25f;
    }

    public static void Fill_SingleSelection(VisualElement singleSelection, List<TwoStrings> twoStrings,
        List<VisualElement> labels, List<VisualElement> checkMarks)
    {
        VisualElement parent_VisualElement = singleSelection.Q<VisualElement>("Parent_VisualElement");
        VisualTreeAsset option_Template_VisualTreeAsset =
            Resources.Load<VisualTreeAsset>("UI/BasicElements/Option_Template");

        for (int i = 0; i < twoStrings.Count; i++)
        {
            VisualElement option = option_Template_VisualTreeAsset.Instantiate();
            option.AddToClassList("Option_TemplateContainer");
            Fix_SingleSelection_Option_Dimentions(option);

            Label option_Label = option.Q<Label>("Option_Label");
            labels.Add(option_Label);

            VisualElement chackMark_TemplateContainer = option.Q<VisualElement>("ChackMark_TemplateContainer");
            Fix_OptionCheckMark_Dimentions(chackMark_TemplateContainer);
            chackMark_TemplateContainer.name = "" + i;
            checkMarks.Add(chackMark_TemplateContainer);

            parent_VisualElement.Add(option);
        }
    }

    #endregion

}

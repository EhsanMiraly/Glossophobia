using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class AnswerEveryThingWindow_PopUp
{
    GameObject parent;

    VisualTreeAsset answerEveryThingWindow_Template;

    PanelRenderer panelRenderer;

    Label answerEveryThing_Label;

    VisualElement okButton_TemplateContainer;
    Label okButton_Label;


    public AnswerEveryThingWindow_PopUp(GameObject parent)
    {
        this.parent = parent;
        parent.name = "AnswerEveryThingWindow_PopUp";
        parent.layer = LayerMask.NameToLayer("UI");

        panelRenderer = parent.AddComponent<PanelRenderer>();
        panelRenderer.panelSettings = Resources.Load<PanelSettings>("UI/PanelSettings/Screen_UI_PanelSettings");
        answerEveryThingWindow_Template =
            Resources.Load<VisualTreeAsset>("UI/Screen_UI/PopUps/AnswerEveryThingWindow_Template");
        panelRenderer.visualTreeAsset = answerEveryThingWindow_Template;
        panelRenderer.sortingOrder = 100;

        panelRenderer.RegisterUIReloadCallback(UIReloadCallback);
    }

    private void UIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        answerEveryThing_Label = root.Q<Label>("AnswerEveryThing_Label");

        okButton_TemplateContainer = root.Q<VisualElement>("OkButton_TemplateContainer");
        okButton_Label = okButton_TemplateContainer.Q<Label>("Text_Label");

        answerEveryThing_Label.text =
            LanguageTextsData.answerEveryThing[SettingsData.currentLanguageIndex];
        answerEveryThing_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        answerEveryThing_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        answerEveryThing_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];


        okButton_TemplateContainer.RegisterCallback<ClickEvent>(OnOkButtonSelected);

        okButton_Label.text = LanguageTextsData.ok[SettingsData.currentLanguageIndex];
        okButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        okButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        okButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
    }

    private void OnOkButtonSelected(ClickEvent clickEvent)
    {
        if (parent != null)
        {
            UnityEngine.Object.Destroy(parent);
            parent = null;
            answerEveryThingWindow_Template = null;
            panelRenderer.UnregisterUIReloadCallback(UIReloadCallback);
            panelRenderer = null;
        }
    }

}

using UnityEngine;
using UnityEngine.UIElements;


public class MessageWindow_PopUp
{
    GameObject parent;
    string message;

    VisualTreeAsset messageWindow_Template;

    PanelRenderer panelRenderer;

    Label message_Label;

    VisualElement okButton_TemplateContainer;
    Label okButton_Label;


    public MessageWindow_PopUp(GameObject parent, string message)
    {
        this.parent = parent;
        this.message = message;

        parent.name = "MessageWindow_PopUp";
        parent.layer = LayerMask.NameToLayer("UI");

        panelRenderer = parent.AddComponent<PanelRenderer>();
        panelRenderer.panelSettings = Resources.Load<PanelSettings>("UI/PanelSettings/Screen_UI_PanelSettings");
        messageWindow_Template =
            Resources.Load<VisualTreeAsset>("UI/Screen_UI/PopUps/MessageWindow_Template");
        panelRenderer.visualTreeAsset = messageWindow_Template;
        panelRenderer.sortingOrder = 100;

        panelRenderer.RegisterUIReloadCallback(UIReloadCallback);
    }

    private void UIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        message_Label = root.Q<Label>("Message_Label");

        okButton_TemplateContainer = root.Q<VisualElement>("OkButton_TemplateContainer");
        okButton_Label = okButton_TemplateContainer.Q<Label>("Text_Label");

        message_Label.text = message;
        message_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        message_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        message_Label.style.fontSize =
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
            messageWindow_Template = null;
            panelRenderer.UnregisterUIReloadCallback(UIReloadCallback);
            panelRenderer = null;
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LaptopHint : MonoBehaviour
{
    PanelRenderer panelRenderer;

    VisualElement nothing_VisualElement;

    VisualElement hint_VisualElement;
    Label hint_Label;
    VisualElement okButton_VisualElement;
    Label okButton_Label;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;
    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);

        RemoveFunctionality();

        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        nothing_VisualElement = root.Q<VisualElement>("Nothing_VisualElement");

        hint_VisualElement = root.Q<VisualElement>("Hint_VisualElement");


        hint_Label = root.Q<Label>("Hint_Label");
        okButton_VisualElement = root.Q<VisualElement>("OkButton_VisualElement");
        okButton_Label = okButton_VisualElement.Q<Label>("OkButton_Label");

        InitializeUI();
    }

    private void InitializeUI()
    {
        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();

        if (SettingsData.currentSawLaptopHint)
        {
            SetPageActive(nothing_VisualElement);
        }
        else
        {
            SetPageActive(hint_VisualElement);
        }
    }


    #region Functionality

    private void AddFunctionality()
    {
        //okButton
        okButton_VisualElement.RegisterCallback<ClickEvent>(OnOkButtonSelected);

    }

    private void RemoveFunctionality()
    {
        //okButton
        okButton_VisualElement.UnregisterCallback<ClickEvent>(OnOkButtonSelected);

    }

    private void OnOkButtonSelected(ClickEvent clickEvent)
    {
        SettingsData.currentSawLaptopHint = true;
        Settings_SaveSystem.Save_Settings();

        SetPageActive(nothing_VisualElement);
    }

    #endregion




    #region Events Manager

    private void OnLanguageChanged()
    {
        #region hint_Label
        hint_Label.text = LanguageTextsData.laptopHint[SettingsData.currentLanguageIndex];
        hint_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        hint_Label.style.unityFont = LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region okButton_Label
        okButton_Label.text = LanguageTextsData.ok[SettingsData.currentLanguageIndex];
        okButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        okButton_Label.style.unityFont = LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region hint_Label
        hint_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySuperSmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region okButton_Label
        okButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySuperSmall[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion


    private void SetPageActive(VisualElement page)
    {
        nothing_VisualElement.style.display = DisplayStyle.None;
        hint_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }

}

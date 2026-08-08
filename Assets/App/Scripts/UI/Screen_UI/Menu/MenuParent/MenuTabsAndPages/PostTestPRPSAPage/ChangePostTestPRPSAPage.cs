using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangePostTestPRPSAPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    PostTestPRPSAPage postTestPRPSAPage;

    VisualElement changePostTestPRPSAPage_VisualElement;
    VisualElement changePage_VisualElement;

    Label thankYou_Label;

    bool isUIReady = false;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        postTestPRPSAPage = GetComponent<PostTestPRPSAPage>();

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
        changePostTestPRPSAPage_VisualElement = root.Q<VisualElement>("ChangePostTestPRPSAPage_VisualElement");
        changePage_VisualElement =
            changePostTestPRPSAPage_VisualElement.Q<VisualElement>("ChangePage_VisualElement");

        thankYou_Label = changePage_VisualElement.Q<Label>("ThankYou_Label");

        isUIReady = true;

        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();
    }


    #region Functionality

    private void AddFunctionality()
    {

    }

    private void RemoveFunctionality()
    {

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
        #region thankYou_Label
        thankYou_Label.text =
            LanguageTextsData.thankYou[SettingsData.currentLanguageIndex];
        thankYou_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        thankYou_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

    }

    private void OnFontSizeChanged()
    {
        #region thankYou_Label
        thankYou_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion


}

using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingWindow : IDisposable
{
    GameObject parent;

    VisualTreeAsset loadingWindow_Template;

    PanelRenderer panelRenderer;
    //UIDocument uIDocument;
    VisualElement root;
    Label loading_Label;

    VisualElement sliderForeground_VisualElement;



    public LoadingWindow(GameObject parent)
    {
        this.parent = parent;
        parent.name = "LoadingWindow";
        parent.layer = LayerMask.NameToLayer("UI");

        panelRenderer = parent.AddComponent<PanelRenderer>();
        panelRenderer.panelSettings = Resources.Load<PanelSettings>("UI/PanelSettings/Screen_PanelSettings");

        panelRenderer.RegisterUIReloadCallback(OnUIReload);

        loadingWindow_Template = Resources.Load<VisualTreeAsset>("UI/LoadingWindow/LoadingWindow_Template");
        panelRenderer.visualTreeAsset = loadingWindow_Template;

        panelRenderer.sortingOrder = 100;
    }

    private void OnUIReload(PanelRenderer panelRenderer, VisualElement root)
    {
        this.root = root;

        loading_Label = this.root.Q<Label>("Loading_Label");

        loading_Label.text =
            LanguageTextsData.loading[SettingsData.currentLanguageIndex];
        loading_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        loading_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        loading_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        sliderForeground_VisualElement = root.Q<VisualElement>("Foreground_VisualElement");

        SetProgress(0);
    }

    public void SetProgress(int progress)
    {
        sliderForeground_VisualElement.style.width = Length.Percent(progress);
    }


    public void Dispose()
    {
        if (parent != null)
        {
            UnityEngine.Object.Destroy(parent);
            parent = null;
            loadingWindow_Template = null;
            panelRenderer.UnregisterUIReloadCallback(OnUIReload);
            panelRenderer = null;
            root = null;
            sliderForeground_VisualElement = null;
        }
    }
}

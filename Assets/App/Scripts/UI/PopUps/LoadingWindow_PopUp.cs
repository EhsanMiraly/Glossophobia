using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingWindow_PopUp : IDisposable
{
    GameObject parent;

    VisualTreeAsset loadingWindow_Template;

    PanelRenderer panelRenderer;

    Label loading_Label;

    VisualElement sliderForeground_VisualElement;



    public LoadingWindow_PopUp(GameObject parent)
    {
        this.parent = parent;
        parent.name = "LoadingWindow_PopUp";
        parent.layer = LayerMask.NameToLayer("UI");

        panelRenderer = parent.AddComponent<PanelRenderer>();
        panelRenderer.panelSettings = Resources.Load<PanelSettings>("UI/PanelSettings/Screen_UI_PanelSettings");
        loadingWindow_Template = Resources.Load<VisualTreeAsset>("UI/Screen_UI/PopUps/LoadingWindow_Template");
        panelRenderer.visualTreeAsset = loadingWindow_Template;
        panelRenderer.sortingOrder = 100;

        panelRenderer.RegisterUIReloadCallback(UIReloadCallback);
    }

    private void UIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        loading_Label = root.Q<Label>("Loading_Label");

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

    public async void SetProgress(int progress)
    {
        while (sliderForeground_VisualElement == null)
        {
            await Awaitable.WaitForSecondsAsync(0.01f);
        }

        sliderForeground_VisualElement.style.width = Length.Percent(progress);
    }


    public void Dispose()
    {
        if (parent != null)
        {
            UnityEngine.Object.Destroy(parent);
            parent = null;
            loadingWindow_Template = null;
            panelRenderer.UnregisterUIReloadCallback(UIReloadCallback);
            panelRenderer = null;
            sliderForeground_VisualElement = null;
        }
    }
}

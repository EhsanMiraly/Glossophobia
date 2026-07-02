using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIConnector : MonoBehaviour
{
    PanelRenderer panelRenderer;


    #region Pages
    [System.NonSerialized] public VisualElement nothingPage_VisualElement;
    [System.NonSerialized] public VisualElement welcomePage_VisualElement;
    [System.NonSerialized] public VisualElement parentPage_VisualElement;

    #endregion


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();

        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);
    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }

    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        nothingPage_VisualElement = root.Q<VisualElement>("NothingPage_VisualElement");
        welcomePage_VisualElement = root.Q<VisualElement>("WelcomePage_VisualElement");
        parentPage_VisualElement = root.Q<VisualElement>("ParentPage_VisualElement");

        InitializeUI();
    }

    private void InitializeUI()
    {
        //First Time?
        SetPageActive(welcomePage_VisualElement);
        //Else
        //SetPageActive();
    }


    public void SetPageActive(VisualElement page)
    {
        nothingPage_VisualElement.style.display = DisplayStyle.None;
        welcomePage_VisualElement.style.display = DisplayStyle.None;
        parentPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }

    public void SetTabActive(VisualElement tab)
    {
        //nothingPage_VisualElement.style.display = DisplayStyle.None;


        tab.style.display = DisplayStyle.Flex;
    }

}

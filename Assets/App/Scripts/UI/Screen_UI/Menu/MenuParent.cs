using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(WelcomePage), typeof(MenuTabsAndPages), typeof(NothingPage))] //Delete NothingPage?
public class MenuParent : MonoBehaviour
{
    PanelRenderer panelRenderer;


    #region Pages
    [System.NonSerialized] public VisualElement nothingPage_VisualElement;
    [System.NonSerialized] public VisualElement welcomePage_VisualElement;
    [System.NonSerialized] public VisualElement menuTabsAndPages_VisualElement;
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
        menuTabsAndPages_VisualElement = root.Q<VisualElement>("MenuTabsAndPages_VisualElement");


        InitializeUI();
    }

    private void InitializeUI()
    {
        if (SettingsData.currentSawWelcome)
        {
            SetPageActive(menuTabsAndPages_VisualElement);
        }
        else
        {
            SetPageActive(welcomePage_VisualElement);
        }
    }


    public void SetPageActive(VisualElement page)
    {
        nothingPage_VisualElement.style.display = DisplayStyle.None;
        welcomePage_VisualElement.style.display = DisplayStyle.None;
        menuTabsAndPages_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }

}

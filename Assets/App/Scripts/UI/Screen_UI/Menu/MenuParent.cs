using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(WelcomePage), typeof(MenuTabsAndPages), typeof(NothingPage))]
public class MenuParent : MonoBehaviour
{
    PanelRenderer panelRenderer;


    #region Pages
    [System.NonSerialized] public VisualElement nothingPage_VisualElement;
    [System.NonSerialized] public VisualElement welcomePage_VisualElement;
    [System.NonSerialized] public VisualElement menuTabsAndPages_VisualElement;

    private VisualElement currentActivePage;
    #endregion

    PlayerInput playerInput;
    PlayerInput.UIActions uI;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        playerInput = new PlayerInput();
        uI = playerInput.UI;
        uI.Enable();
        uI.Escape.performed += context => { OnEscapeSelected(); };
    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);

        uI.Escape.performed -= context => { OnEscapeSelected(); };
        uI.Disable();
    }

    private void OnEscapeSelected()
    {
        if (!GameData.isSimulating)
        {
            return;
        }

        if (currentActivePage == nothingPage_VisualElement)
        {
            SetPageActive(menuTabsAndPages_VisualElement);
            UI_Utilities.MouseVisible(true);
        }
        else if (currentActivePage == menuTabsAndPages_VisualElement)
        {
            SetPageActive(nothingPage_VisualElement);
            UI_Utilities.MouseVisible(false);
        }
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

        currentActivePage = page;
        page.style.display = DisplayStyle.Flex;
    }

}

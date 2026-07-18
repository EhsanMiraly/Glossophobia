using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(StartPage), typeof(QuestionsPage), typeof(ChangePage))]
public class PRPSAPage : MonoBehaviour
{
    private bool isPRPSA_Before = true;


    PanelRenderer panelRenderer;

    VisualElement PRPSAPage_VisualElement;


    #region Pages
    [System.NonSerialized] public VisualElement startPage_VisualElement;
    [System.NonSerialized] public VisualElement questionsPage_VisualElement;
    [System.NonSerialized] public VisualElement changePage_VisualElement;
    #endregion


    private bool isUIReady = false;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        AddFunctionality();
    }

    private void OnDisable()
    {
        RemoveFunctionality();

        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }

    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        PRPSAPage_VisualElement = root.Q<VisualElement>("PRPSAPage_VisualElement");

        startPage_VisualElement = PRPSAPage_VisualElement.Q<VisualElement>("StartPage_VisualElement");
        questionsPage_VisualElement = PRPSAPage_VisualElement.Q<VisualElement>("QuestionsPage_VisualElement");
        changePage_VisualElement = PRPSAPage_VisualElement.Q<VisualElement>("ChangePage_VisualElement");

        isUIReady = true;
    }


    #region Functionality
    private void AddFunctionality()
    {
        EventsManager.OnLoggedIn_Event += OnLoggedIn;
        EventsManager.OnLoggedOut_Event += OnLoggedOut;
    }

    private void RemoveFunctionality()
    {
        EventsManager.OnLoggedIn_Event -= OnLoggedIn;
        EventsManager.OnLoggedOut_Event -= OnLoggedOut;
    }
    #endregion


    public void SetPageActive(VisualElement page)
    {
        startPage_VisualElement.style.display = DisplayStyle.None;
        questionsPage_VisualElement.style.display = DisplayStyle.None;
        changePage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }


    #region Events Manager

    #region LoggedIn/LoggedOut
    private async void OnLoggedIn()
    {
        while (!isUIReady)
        {
            await Awaitable.WaitForSecondsAsync(0.1f);
        }

        PRPSA_Before_SaveSystem.Load_PRPSA_Before();

        if (!PRPSA_BeforeData.IsAllAnswersGiven())
        {
            PRPSA_BeforeData.InitializeAnswers();
            SetPageActive(startPage_VisualElement);
        }
        else
        {
            SetPageActive(changePage_VisualElement);

            EventsManager.InvokeOnSetPRPSA_Before();
        }
    }

    private void OnLoggedOut()
    {
        PRPSA_BeforeData.InitializeAnswers();
    }
    #endregion

    #region SetDemographics/ChangeDemographics
    private void OnSetDemographics()
    {
        //Nothing happens
    }

    private void OnChangeDemographics()
    {
        //Nothing happens
    }
    #endregion

    #endregion
}

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(GiveDemographicsPage), typeof(ChangeDemographicsPage))]
public class DemographicsPage : MonoBehaviour
{
    public Demographics demographics = new Demographics();

    PanelRenderer panelRenderer;

    VisualElement demographicsPage_VisualElement;


    #region Pages
    [System.NonSerialized] public VisualElement giveDemographicsPage_VisualElement;
    [System.NonSerialized] public VisualElement changeDemographicsPage_VisualElement;
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
        demographicsPage_VisualElement = root.Q<VisualElement>("DemographicsPage_VisualElement");

        giveDemographicsPage_VisualElement =
            demographicsPage_VisualElement.Q<VisualElement>("GiveDemographicsPage_VisualElement");
        changeDemographicsPage_VisualElement =
            demographicsPage_VisualElement.Q<VisualElement>("ChangeDemographicsPage_VisualElement");

        isUIReady = true;
    }


    #region Functionality
    private void AddFunctionality()
    {
        EventsManager.OnLoggedIn_Event += OnLoggedIn;
    }

    private void RemoveFunctionality()
    {
        EventsManager.OnLoggedIn_Event -= OnLoggedIn;
    }
    #endregion


    public void SetPageActive(VisualElement page)
    {
        giveDemographicsPage_VisualElement.style.display = DisplayStyle.None;
        changeDemographicsPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }


    #region Events Manager

    #region LoggedIn/LoggedOut
    private async void OnLoggedIn()
    {
        while (!isUIReady)
        {
            await Awaitable.EndOfFrameAsync();
        }

        demographics = await FireStoreManager.LoadDemographics();

        if (demographics.IsEveryThingSet())
        {
            SetPageActive(changeDemographicsPage_VisualElement);
            EventsManager.InvokeOnSetDemographics();
        }
        else
        {
            SetPageActive(giveDemographicsPage_VisualElement);
        }
    }

    private void OnLoggedOut()
    {
        //Nothing happens
    }
    #endregion

    #region SetPRPSA_Before/ChangePRPSA_Before
    private void OnSetPRPSA_Before()
    {
        //Nothing happens
    }

    private void OnChangePRPSA_Before()
    {
        //Nothing happens
    }
    #endregion

    #endregion

}

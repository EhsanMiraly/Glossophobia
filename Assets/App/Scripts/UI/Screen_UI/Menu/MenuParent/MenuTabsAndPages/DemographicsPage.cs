using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.ProjectWindowCallback;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(GiveDemographicsPage), typeof(ChangeDemographicsPage))]
public class DemographicsPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    VisualElement demographicsPage_VisualElement;


    #region Pages
    [System.NonSerialized] public VisualElement giveDemographicsPage_VisualElement;
    [System.NonSerialized] public VisualElement changeDemographicsPage_VisualElement;
    #endregion



    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);


    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);

        EventsManager.OnLoggedIn_Event -= OnLoggedIn;
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        demographicsPage_VisualElement = root.Q<VisualElement>("DemographicsPage_VisualElement");

        giveDemographicsPage_VisualElement =
            demographicsPage_VisualElement.Q<VisualElement>("GiveDemographicsPage_VisualElement");
        changeDemographicsPage_VisualElement =
            demographicsPage_VisualElement.Q<VisualElement>("ChangeDemographicsPage_VisualElement");

        EventsManager.OnLoggedIn_Event += OnLoggedIn;
        //InitializeUI();
    }

    private void InitializeUI()
    {
        Demographics_SaveSystem.Load_Demographics();

        if (DemographicsData.IsEveryThingSet())
        {
            EventsManager.InvokeOnSetDemographics();
            SetPageActive(changeDemographicsPage_VisualElement);
        }
        else
        {
            SetPageActive(giveDemographicsPage_VisualElement);
        }
    }

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
        Demographics_SaveSystem.Load_Demographics();

        if (DemographicsData.IsEveryThingSet())
        {
            SetPageActive(changeDemographicsPage_VisualElement);

            await Awaitable.WaitForSecondsAsync(3f);
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

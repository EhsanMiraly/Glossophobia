using System;
using System.Collections.Generic;
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
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        demographicsPage_VisualElement = root.Q<VisualElement>("DemographicsPage_VisualElement");

        giveDemographicsPage_VisualElement =
            demographicsPage_VisualElement.Q<VisualElement>("GiveDemographicsPage_VisualElement");
        changeDemographicsPage_VisualElement =
            demographicsPage_VisualElement.Q<VisualElement>("ChangeDemographicsPage_VisualElement");

        InitializeUI();
    }

    private void InitializeUI()
    {
        if (DemographicsData.IsEveryThingSet())
        {
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

}

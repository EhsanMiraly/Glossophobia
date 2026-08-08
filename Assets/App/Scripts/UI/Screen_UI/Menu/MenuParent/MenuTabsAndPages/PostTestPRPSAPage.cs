using System;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(StartPostTestPRPSAPage), typeof(QuestionsPostTestPRPSAPage),
                    typeof(ChangePostTestPRPSAPage))]
public class PostTestPRPSAPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    VisualElement postTestPRPSAPage_VisualElement;

    #region Pages
    [System.NonSerialized] public VisualElement startPostTestPage_VisualElement;
    [System.NonSerialized] public VisualElement questionsPostTestPage_VisualElement;
    [System.NonSerialized] public VisualElement changePostTestPage_VisualElement;
    #endregion

    private bool isUIReady = false;



    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);
    }

    private void OnDisable()
    {
        //RemoveFunctionality();

        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }

    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        postTestPRPSAPage_VisualElement = root.Q<VisualElement>("PostTestPRPSAPage_VisualElement");

        startPostTestPage_VisualElement =
            postTestPRPSAPage_VisualElement.Q<VisualElement>("StartPostTestPage_VisualElement");
        questionsPostTestPage_VisualElement =
            postTestPRPSAPage_VisualElement.Q<VisualElement>("QuestionsPostTestPage_VisualElement");
        changePostTestPage_VisualElement =
            postTestPRPSAPage_VisualElement.Q<VisualElement>("ChangePostTestPage_VisualElement");

        //AddFunctionality();

        isUIReady = true;

        InitializeUI();
    }


    private void InitializeUI()
    {
        SetPageActive(startPostTestPage_VisualElement);
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

    }

    private void DisconnectEvents()
    {

    }
    #endregion


    public void SetPageActive(VisualElement page)
    {
        startPostTestPage_VisualElement.style.display = DisplayStyle.None;
        questionsPostTestPage_VisualElement.style.display = DisplayStyle.None;
        changePostTestPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }

}

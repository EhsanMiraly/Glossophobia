using System;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(StartBaselinePRPSAPage), typeof(QuestionsBaselinePRPSAPage), typeof(ChangeBaselinePRPSAPage))]
public class BaselinePRPSAPage : MonoBehaviour
{
    public BaselinePRPSA baselinePRPSA = new BaselinePRPSA();

    PanelRenderer panelRenderer;

    VisualElement baselinePRPSAPage_VisualElement;


    #region Pages
    [System.NonSerialized] public VisualElement startBaselinePRPSAPage_VisualElement;
    [System.NonSerialized] public VisualElement questionsBaselinePRPSAPage_VisualElement;
    [System.NonSerialized] public VisualElement changeBaselinePRPSAPage_VisualElement;
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
        baselinePRPSAPage_VisualElement = root.Q<VisualElement>("BaselinePRPSAPage_VisualElement");

        startBaselinePRPSAPage_VisualElement =
            baselinePRPSAPage_VisualElement.Q<VisualElement>("StartBaselinePRPSAPage_VisualElement");
        questionsBaselinePRPSAPage_VisualElement =
            baselinePRPSAPage_VisualElement.Q<VisualElement>("QuestionsBaselinePRPSAPage_VisualElement");
        changeBaselinePRPSAPage_VisualElement =
            baselinePRPSAPage_VisualElement.Q<VisualElement>("ChangeBaselinePRPSAPage_VisualElement");

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
        startBaselinePRPSAPage_VisualElement.style.display = DisplayStyle.None;
        questionsBaselinePRPSAPage_VisualElement.style.display = DisplayStyle.None;
        changeBaselinePRPSAPage_VisualElement.style.display = DisplayStyle.None;

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

        try
        {
            if (FirebaseAuth.DefaultInstance.CurrentUser == null)
            {
                new MessageWindow_PopUp(new GameObject(),
                    LanguageTextsData.thereIsSomethingWrongWithYourAccount[SettingsData.currentLanguageIndex]);
                return;
            }

            DocumentReference playerDocument =
                FirebaseFirestore.DefaultInstance.Collection(FireStoreNames.players_Collection).
                    Document(FirebaseAuth.DefaultInstance.CurrentUser.UserId);

            DocumentSnapshot snapshot = await playerDocument.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                baselinePRPSA = new BaselinePRPSA();
                InitializeUI();
                return;
            }

            Dictionary<string, object> data = snapshot.ToDictionary();

            if (!data.ContainsKey(FireStoreNames.baselinePRPSA_Map))
            {
                baselinePRPSA = new BaselinePRPSA();
                InitializeUI();
                return;
            }

            Dictionary<string, object> baselinePRPSA_Dictionary =
                data[FireStoreNames.baselinePRPSA_Map] as Dictionary<string, object>;

            if (baselinePRPSA_Dictionary == null)
            {
                baselinePRPSA = new BaselinePRPSA();
                InitializeUI();
                return;
            }

            if (!baselinePRPSA_Dictionary.ContainsKey(FireStoreNames.baselinePRPSAIndexes))
            {
                baselinePRPSA = new BaselinePRPSA();
                InitializeUI();
                return;
            }

            List<object> indexes_Object =
                baselinePRPSA_Dictionary[FireStoreNames.baselinePRPSAIndexes]
                    as List<object>;

            if (indexes_Object == null)
            {
                baselinePRPSA = new BaselinePRPSA();
                InitializeUI();
                return;
            }

            int[] indexes = new int[indexes_Object.Count];

            for (int i = 0; i < indexes_Object.Count; i++)
            {
                indexes[i] = Convert.ToInt32(indexes_Object[i]);
            }

            baselinePRPSA = new BaselinePRPSA(indexes);

            InitializeUI();
            return;
        }
        catch (FirestoreException firestoreException)
        {
            switch (firestoreException.ErrorCode)
            {
                case FirestoreError.Unavailable:
                    new MessageWindow_PopUp(new GameObject(),
                        LanguageTextsData.unavailable[SettingsData.currentLanguageIndex]);
                    break;
                case FirestoreError.DeadlineExceeded:
                    new MessageWindow_PopUp(new GameObject(),
                        LanguageTextsData.deadlineExceeded[SettingsData.currentLanguageIndex]);
                    break;
                case FirestoreError.Unauthenticated:
                    new MessageWindow_PopUp(new GameObject(),
                        LanguageTextsData.unauthenticated[SettingsData.currentLanguageIndex]);
                    break;
                default:
                    new MessageWindow_PopUp(new GameObject(),
                        LanguageTextsData.thereIsSomethingWrong[SettingsData.currentLanguageIndex]);
                    break;
            }
        }
        catch
        {
            new MessageWindow_PopUp(new GameObject(),
                LanguageTextsData.thereIsSomethingWrong[SettingsData.currentLanguageIndex]);
        }
    }

    private void InitializeUI()
    {
        if (baselinePRPSA.IsEveryThingSet())
        {
            SetPageActive(changeBaselinePRPSAPage_VisualElement);
            EventsManager.InvokeOnSetPRPSA_Before();
        }
        else
        {
            SetPageActive(startBaselinePRPSAPage_VisualElement);
        }
    }

    private void OnLoggedOut()
    {

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

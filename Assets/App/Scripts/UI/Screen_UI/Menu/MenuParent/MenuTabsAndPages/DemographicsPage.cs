using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Firestore;
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
                demographics = new Demographics();
                InitializeUI();
                return;
            }

            Dictionary<string, object> data = snapshot.ToDictionary();

            if (!data.ContainsKey(FireStoreNames.demographics_Map))
            {
                demographics = new Demographics();
                InitializeUI();
                return;
            }

            Dictionary<string, object> demographics_Dictionary =
                data[FireStoreNames.demographics_Map] as Dictionary<string, object>;

            demographics = new Demographics
                (
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.genderIndex]),
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.ageGroupIndex]),
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.educationLevelIndex]),
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.fieldOfStudyIndex]),
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.jobIndex]),
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.levelOfExperienceIndex]),
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.levelOfNeedIndex]),
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.levelOfAnxietyIndex]),
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.formalTrainingIndex]),
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.takingMedicationIndex]),
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.games3DIndex]),
                    Convert.ToInt32(demographics_Dictionary[FireStoreNames.simulationGamesIndex])
                );
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

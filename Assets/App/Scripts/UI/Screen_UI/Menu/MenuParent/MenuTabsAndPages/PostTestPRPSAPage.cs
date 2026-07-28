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

        AddFunctionality();
    }

    private void OnDisable()
    {
        RemoveFunctionality();

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


    public void SetPageActive(VisualElement page)
    {
        startPostTestPage_VisualElement.style.display = DisplayStyle.None;
        questionsPostTestPage_VisualElement.style.display = DisplayStyle.None;
        changePostTestPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }



    /*




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
                SetPageActive(changePage_VisualElement);
                EventsManager.InvokeOnSetPRPSA_Before();
            }
            else
            {
                SetPageActive(startPage_VisualElement);
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
    */
}

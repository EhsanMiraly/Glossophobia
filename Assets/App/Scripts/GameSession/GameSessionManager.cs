using System;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{

    private void OnEnable()
    {
        ConnectEvents();

        GameData.gameSession = new GameSession();
    }

    private void OnDisable()
    {
        DisconnectEvents();
    }



    #region Events Manager

    private void ConnectEvents()
    {
        EventsManager.OnSimulationStarted_Event += OnSimulationStarted;
        EventsManager.OnClockStarted_Event += OnClockStarted;
        EventsManager.OnClockEnded_Event += OnClockEnded;
        EventsManager.OnSimulationEnded_Event += OnSimulationEnded;
        EventsManager.OnFinishedPostTestPRPSA_Event += OnFinishedPostTestPRPSA;
    }

    private void DisconnectEvents()
    {
        EventsManager.OnSimulationStarted_Event -= OnSimulationStarted;
        EventsManager.OnClockStarted_Event -= OnClockStarted;
        EventsManager.OnClockEnded_Event -= OnClockEnded;
        EventsManager.OnSimulationEnded_Event -= OnSimulationEnded;
        EventsManager.OnFinishedPostTestPRPSA_Event -= OnFinishedPostTestPRPSA;
    }


    private async void OnSimulationStarted()
    {
        GameData.gameSession = new GameSession();

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
                GameData.gameSession.numberOfCurrentGameSession = 1;
                return;
            }

            if (!snapshot.ContainsField(FireStoreNames.numberOfGameSessions))
            {
                Debug.Log("numberOfCurrentGameSession Should Exist");
                GameData.gameSession.numberOfCurrentGameSession = 1;
                return;
            }

            int numberOfGameSessions = snapshot.GetValue<int>(FireStoreNames.numberOfGameSessions);
            GameData.gameSession.numberOfCurrentGameSession = numberOfGameSessions + 1;
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

        Debug.Log("NumberOfCurrentGameSession: " + GameData.gameSession.numberOfCurrentGameSession);
    }


    private void OnClockStarted()
    {
        //Clock started
        //create game session locally - gather data locally -
    }


    private void OnClockEnded()
    {
        //In GameData save finished game so you can save its data - if not dont save data
        //Clock ended
        //save game session data - prosses data - show data to player - 
    }


    private void OnSimulationEnded()
    {
        //simulation ended
        //show postPRPSA
    }


    private void OnFinishedPostTestPRPSA()
    {
        //postEnded
        //save postPRPSA data in game session


        //await playerDocument.UpdateAsync("numberOfGameSessions", FieldValue.Increment(1));
    }

    #endregion

}
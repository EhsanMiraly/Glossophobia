using System;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{

    private void OnEnable()
    {
        ConnectEvents();
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


    private void OnSimulationStarted()
    {
        //if internet problem unload scene
        //simulation started
        //find game session number - 
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
    }

    #endregion

}

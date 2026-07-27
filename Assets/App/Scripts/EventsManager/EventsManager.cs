using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnNotify();

public class EventsManager
{

    #region UI Events

    public static event OnNotify OnLanguageChanged_Event;
    public static void InvokeOnLanguageChanged()
    {
        OnLanguageChanged_Event?.Invoke();
    }

    public static event OnNotify OnFontSizeChanged_Event;
    public static void InvokeOnFontSizeChanged()
    {
        OnFontSizeChanged_Event?.Invoke();
    }

    public static event OnNotify OnSoundVolumeChanged_Event;
    public static void InvokeOnSoundVolumeChanged()
    {
        OnSoundVolumeChanged_Event?.Invoke();
    }


    #endregion


    #region Account
    public static event OnNotify OnLoggedIn_Event;
    public static void InvokeOnLoggedIn()
    {
        OnLoggedIn_Event?.Invoke();
    }

    public static event OnNotify OnLoggedOut_Event;
    public static void InvokeOnLoggedOut()
    {
        OnLoggedOut_Event?.Invoke();
    }
    #endregion


    #region Demographics
    public static event OnNotify OnSetDemographics_Event;
    public static void InvokeOnSetDemographics()
    {
        OnSetDemographics_Event?.Invoke();
    }

    public static event OnNotify OnChangeDemographics_Event;
    public static void InvokeOnChangeDemographics()
    {
        OnChangeDemographics_Event?.Invoke();
    }
    #endregion


    #region PRPSA_Before
    public static event OnNotify OnSetPRPSA_Before_Event;
    public static void InvokeOnSetPRPSA_Before()
    {
        OnSetPRPSA_Before_Event?.Invoke();
    }

    public static event OnNotify OnChangePRPSA_Before_Event;
    public static void InvokeOnChangePRPSA_Before()
    {
        OnChangePRPSA_Before_Event?.Invoke();
    }

    public static event OnNotify OnFinishedPostTestPRPSA_Event;
    public static void InvokeOnFinishedPostTestPRPSA()
    {
        OnFinishedPostTestPRPSA_Event?.Invoke();
    }
    #endregion


    #region GameLoop Events

    public static event OnNotify OnSimulationStarted_Event;
    public static void InvokeOnSimulationStarted()
    {
        OnSimulationStarted_Event?.Invoke();
    }

    public static event OnNotify OnSimulationPaused_Event;
    public static void InvokeOnSimulationPaused()
    {
        OnSimulationPaused_Event?.Invoke();
    }

    public static event OnNotify OnSimulationResumed_Event;
    public static void InvokeOnSimulationResumed()
    {
        OnSimulationResumed_Event?.Invoke();
    }

    public static event OnNotify OnFinishedSimulation_Event;
    public static void InvokeOnSimulationEnded()
    {
        OnFinishedSimulation_Event?.Invoke();
    }

    public static event OnNotify OnDoorOpen_Event;
    public static void InvokeOnDoorOpen()
    {
        OnDoorOpen_Event?.Invoke();
    }

    public static event OnNotify OnDoorClose_Event;
    public static void InvokeOnDoorClose()
    {
        OnDoorClose_Event?.Invoke();
    }

    public static event OnNotify OnClockStarted_Event;
    public static void InvokeOnClockStarted()
    {
        OnClockStarted_Event?.Invoke();
    }

    public static event OnNotify OnClockEnded_Event;
    public static void InvokeOnClockEnded()
    {
        OnClockEnded_Event?.Invoke();
    }

    #endregion


    #region Brain Events

    public static event OnNotify OnAddNPC_Event;
    public static void InvokeOnAddNPC()
    {
        OnAddNPC_Event?.Invoke();
    }

    public static event OnNotify OnRemoveNPC_Event;
    public static void InvokeOnRemoveNPC()
    {
        OnRemoveNPC_Event?.Invoke();
    }

    #endregion
}

public class Timer
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }

    public Timer(int hours, int minutes, int seconds)
    {
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
    }

    public bool HasTime()
    {
        if (Hours > 0 || Minutes > 0 || Seconds > 0)
        {
            return true;
        }
        return false;
    }

    public void DecreaseOneSecond()
    {
        Seconds--;
        if (Seconds < 0)
        {
            if (Minutes > 0)
            {
                Minutes--;
                Seconds = 59;
            }
            else if (Hours > 0)
            {
                Hours--;
                Minutes = 59;
                Seconds = 59;
            }
        }
    }

    public void IncreaseOneSecond()
    {
        Seconds++;
        if (Seconds > 59)
        {
            Seconds = 0;
            Minutes++;
            if (Minutes > 59)
            {
                Minutes = 0;
                Hours++;
            }
        }
    }

}

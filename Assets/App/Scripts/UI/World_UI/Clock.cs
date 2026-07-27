using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Clock : MonoBehaviour
{
    PanelRenderer panelRenderer;

    Label clock_Label;

    bool isPlaying = false;



    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        EventsManager.OnClockStarted_Event += OnClockStarted;
        EventsManager.OnClockEnded_Event += OnClockEnded;
    }


    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        clock_Label = root.Q<Label>("Clock_Label");

        clock_Label.RemoveFromClassList("ExtraTimer");
        clock_Label.AddToClassList("RemainingTimer");
        clock_Label.text = "00 : 00 : 00";
    }

    #region EventsManager

    private void OnClockStarted()
    {
        isPlaying = true;
        clock_Label.RemoveFromClassList("ExtraTimer");
        clock_Label.AddToClassList("RemainingTimer");

        UpdateTimer();
    }

    private void OnClockEnded()
    {
        isPlaying = false;
    }

    #endregion

    private async void UpdateTimer()
    {
        clock_Label.text = GameData.remainingTimer.Hours + " : " + GameData.remainingTimer.Minutes
                    + " : " + GameData.remainingTimer.Seconds;

        while (isPlaying && GameData.remainingTimer.HasTime())
        {
            await Awaitable.WaitForSecondsAsync(1f);
            GameData.remainingTimer.DecreaseOneSecond();
            clock_Label.text = GameData.remainingTimer.Hours + " : " + GameData.remainingTimer.Minutes
                    + " : " + GameData.remainingTimer.Seconds;
        }

        if (isPlaying)
        {
            clock_Label.RemoveFromClassList("RemainingTimer");
            clock_Label.AddToClassList("ExtraTimer");
        }

        while (isPlaying)
        {
            await Awaitable.WaitForSecondsAsync(1f);
            GameData.extraTimer.IncreaseOneSecond();
            clock_Label.text = GameData.extraTimer.Hours + " : " + GameData.extraTimer.Minutes
                    + " : " + GameData.extraTimer.Seconds;
        }
    }

}

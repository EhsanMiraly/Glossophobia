using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    public void OnEnable()
    {
        EventsManager.OnSimulationStarted_Event += OnSimulationStarted;
        EventsManager.OnSimulationPaused_Event += OnSimulationPaused;
        EventsManager.OnSimulationResumed_Event += OnSimulationResumed;
        EventsManager.OnSimulationEnded_Event += OnSimulationEnded;
    }

    private void OnDisable()
    {
        EventsManager.OnSimulationStarted_Event -= OnSimulationStarted;
        EventsManager.OnSimulationPaused_Event -= OnSimulationPaused;
        EventsManager.OnSimulationResumed_Event -= OnSimulationResumed;
        EventsManager.OnSimulationEnded_Event -= OnSimulationEnded;
    }

    private async void OnSimulationStarted()
    {
        Time.timeScale = 1;

        await SceneManager.LoadSceneAsync("SimulationScene", LoadSceneMode.Additive);
    }

    public void OnSimulationPaused()
    {
        Time.timeScale = 0.001f;
    }

    public void OnSimulationResumed()
    {
        Time.timeScale = 1;
    }

    private async void OnSimulationEnded()
    {
        if (SceneManager.GetSceneByName("SimulationScene").isLoaded)
        {
            await SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("SimulationScene"));
        }
    }

}

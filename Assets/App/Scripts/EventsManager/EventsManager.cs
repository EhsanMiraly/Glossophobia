using UnityEngine;

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

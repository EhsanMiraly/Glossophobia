using UnityEngine;

public delegate void OnNotify();

public class EventsManager
{
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


}

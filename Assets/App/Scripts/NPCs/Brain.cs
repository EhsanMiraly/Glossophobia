using System.Threading.Tasks;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public bool timeToAddNPC = false;
    public bool timeToRemoveNPC = false;


    private async void OnEnable()
    {
        //loop every 5 second to change env
        ChairsUtilities.UpdateChairPossibility();

        while (true)
        {
            await Awaitable.WaitForSecondsAsync(1f);

            if (timeToAddNPC)
            {
                EventsManager.InvokeOnAddNPC();
                timeToAddNPC = false;
            }

            if (timeToRemoveNPC)
            {
                EventsManager.InvokeOnRemoveNPC();
                timeToRemoveNPC = false;
            }

        }
    }

}

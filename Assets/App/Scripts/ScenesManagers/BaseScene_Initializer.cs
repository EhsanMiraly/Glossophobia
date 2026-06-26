using System.Threading.Tasks;
using UnityEngine;

public class BaseScene_Initializer : MonoBehaviour
{
    private async void Awake()
    {
        using (LoadingWindow loadingWindow = new LoadingWindow(new GameObject()))
        {
            await Awaitable.WaitForSecondsAsync(1f);
            loadingWindow.SetProgress(10);
            await Awaitable.WaitForSecondsAsync(1f);
            loadingWindow.SetProgress(20);
            await Awaitable.WaitForSecondsAsync(1f);
            loadingWindow.SetProgress(30);
            await Awaitable.WaitForSecondsAsync(1f);
            loadingWindow.SetProgress(40);



            await Awaitable.WaitForSecondsAsync(1f);
            loadingWindow.SetProgress(100);
        }
    }


}

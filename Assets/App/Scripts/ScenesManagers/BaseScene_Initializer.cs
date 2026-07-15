using System.Threading.Tasks;
using UnityEngine;

public class BaseScene_Initializer : MonoBehaviour
{
    [SerializeField] GameObject ui_GameObject;

    private async void Awake()
    {
        Settings_SaveSystem.Load_Settings();

        using (LoadingWindow_PopUp loadingWindow_PopUp = new LoadingWindow_PopUp(new GameObject()))
        {
            loadingWindow_PopUp.SetProgress(10);
            await Awaitable.WaitForSecondsAsync(1f);//Delete

            ui_GameObject = Instantiate(ui_GameObject);
            await Awaitable.WaitForSecondsAsync(1f);//Delete

            loadingWindow_PopUp.SetProgress(100);
            await Awaitable.WaitForSecondsAsync(1f);//Delete
        }
    }


}

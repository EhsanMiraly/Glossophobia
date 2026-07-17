using System.Threading.Tasks;
using UnityEngine;

public class BaseScene_Initializer : MonoBehaviour
{
    [SerializeField] GameObject mainCamera_GameObject;
    [SerializeField] GameObject ui_GameObject;


    private async void Awake()
    {
        Settings_SaveSystem.Load_Settings();

        using (LoadingWindow_PopUp loadingWindow_PopUp = new LoadingWindow_PopUp(new GameObject()))
        {
            loadingWindow_PopUp.SetProgress(10);

            mainCamera_GameObject = Instantiate(mainCamera_GameObject);

            loadingWindow_PopUp.SetProgress(20);

            ui_GameObject = Instantiate(ui_GameObject);

            loadingWindow_PopUp.SetProgress(100);
        }
    }


}

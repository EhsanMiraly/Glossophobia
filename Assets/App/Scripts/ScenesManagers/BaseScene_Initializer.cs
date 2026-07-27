using System.Threading.Tasks;
using UnityEngine;

public class BaseScene_Initializer : MonoBehaviour
{
    [SerializeField] GameObject mainCamera_GameObject;
    [SerializeField] GameObject fontLoader_GameObject;
    [SerializeField] GameObject screenUI_GameObject;
    [SerializeField] GameObject scenesManager_GameObject;



    private void Awake()
    {
        Settings_SaveSystem.Load_Settings();

        using (LoadingWindow_PopUp loadingWindow_PopUp = new LoadingWindow_PopUp(new GameObject()))
        {
            loadingWindow_PopUp.SetProgress(10);

            mainCamera_GameObject = Instantiate(mainCamera_GameObject);

            loadingWindow_PopUp.SetProgress(20);

            fontLoader_GameObject = Instantiate(fontLoader_GameObject);

            loadingWindow_PopUp.SetProgress(30);

            screenUI_GameObject = Instantiate(screenUI_GameObject);

            loadingWindow_PopUp.SetProgress(40);

            scenesManager_GameObject = Instantiate(scenesManager_GameObject);

            loadingWindow_PopUp.SetProgress(100);
        }
    }


}

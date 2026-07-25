using UnityEngine;
using UnityEngine.UIElements;
using Firebase.Auth;


[RequireComponent(typeof(LogInPage), typeof(SignUpPage), typeof(LogOutPage))]
public class AccountPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    VisualElement accountPage_VisualElement;


    #region Pages
    [System.NonSerialized] public VisualElement logInPage_VisualElement;
    [System.NonSerialized] public VisualElement signUpPage_VisualElement;
    [System.NonSerialized] public VisualElement logOutPage_VisualElement;
    #endregion

    private bool isUIReady = false;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        ConnectEvents();
    }


    private void OnDisable()
    {
        DisconnectEvents();

        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        accountPage_VisualElement = root.Q<VisualElement>("AccountPage_VisualElement");

        logInPage_VisualElement = accountPage_VisualElement.Q<VisualElement>("LogInPage_VisualElement");
        signUpPage_VisualElement = accountPage_VisualElement.Q<VisualElement>("SignUpPage_VisualElement");
        logOutPage_VisualElement = accountPage_VisualElement.Q<VisualElement>("LogOutPage_VisualElement");

        isUIReady = true;
    }


    #region Events Manager

    private void ConnectEvents()
    {
        FirebaseAuth.DefaultInstance.StateChanged += OnAuthStateChanged;
    }

    private void DisconnectEvents()
    {
        FirebaseAuth.DefaultInstance.StateChanged -= OnAuthStateChanged;
    }


    private async void OnAuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        while (!isUIReady)
        {
            await Awaitable.EndOfFrameAsync();
        }

        if (FirebaseAuth.DefaultInstance.CurrentUser != null && FirebaseAuth.DefaultInstance.CurrentUser.IsValid())
        {
            SetPageActive(logOutPage_VisualElement);
            EventsManager.InvokeOnLoggedIn();
        }
        else
        {
            SetPageActive(signUpPage_VisualElement);
        }
    }

    #endregion


    public void SetPageActive(VisualElement page)
    {
        logInPage_VisualElement.style.display = DisplayStyle.None;
        signUpPage_VisualElement.style.display = DisplayStyle.None;
        logOutPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }
}

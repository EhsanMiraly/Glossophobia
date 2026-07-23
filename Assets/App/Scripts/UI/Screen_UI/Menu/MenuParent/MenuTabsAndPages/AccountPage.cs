using UnityEngine;
using UnityEngine.UIElements;
using Firebase.Auth;


[RequireComponent(typeof(LogInPage), typeof(SignUpPage), typeof(LogOutPage))]
public class AccountPage : MonoBehaviour
{
    private FirebaseAuth firebaseAuthenticator;

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
        firebaseAuthenticator = FirebaseAuth.DefaultInstance;
        firebaseAuthenticator.StateChanged += AuthStateChanged;

        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);
    }


    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);

        firebaseAuthenticator.StateChanged -= AuthStateChanged;
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        accountPage_VisualElement = root.Q<VisualElement>("AccountPage_VisualElement");

        logInPage_VisualElement = accountPage_VisualElement.Q<VisualElement>("LogInPage_VisualElement");
        signUpPage_VisualElement = accountPage_VisualElement.Q<VisualElement>("SignUpPage_VisualElement");
        logOutPage_VisualElement = accountPage_VisualElement.Q<VisualElement>("LogOutPage_VisualElement");

        isUIReady = true;
    }


    private async void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        while (!isUIReady)
        {
            await Awaitable.WaitForSecondsAsync(0.1f);
        }

        if (firebaseAuthenticator.CurrentUser != null && firebaseAuthenticator.CurrentUser.IsValid())
        {
            SetPageActive(logOutPage_VisualElement);
            EventsManager.InvokeOnLoggedIn();
        }
        else
        {
            SetPageActive(logInPage_VisualElement);
        }
    }


    public void SetPageActive(VisualElement page)
    {
        logInPage_VisualElement.style.display = DisplayStyle.None;
        signUpPage_VisualElement.style.display = DisplayStyle.None;
        logOutPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }
}

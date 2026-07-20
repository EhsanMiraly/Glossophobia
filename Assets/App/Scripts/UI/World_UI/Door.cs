using System;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class Door : MonoBehaviour
{
    Animator animator;
    int openDoor_Hash;
    int closeDoor_Hash;
    bool isDoorClosed = true;

    PanelRenderer[] panelRenderer;

    VisualElement background_VisualElement0;
    Label door_Label0;

    VisualElement background_VisualElement1;
    Label door_Label1;


    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        openDoor_Hash = Animator.StringToHash("OpenDoor");
        closeDoor_Hash = Animator.StringToHash("CloseDoor");

        panelRenderer = GetComponentsInChildren<PanelRenderer>();
        panelRenderer[0].RegisterUIReloadCallback(OnUIReload0CallBack);
        panelRenderer[1].RegisterUIReloadCallback(OnUIReload1CallBack);

        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;
    }

    private void OnDisable()
    {
        panelRenderer[0].UnregisterUIReloadCallback(OnUIReload0CallBack);
        panelRenderer[1].UnregisterUIReloadCallback(OnUIReload1CallBack);

        background_VisualElement0.UnregisterCallback<ClickEvent>(OnVisualElementSelected);
        background_VisualElement1.UnregisterCallback<ClickEvent>(OnVisualElementSelected);

        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }


    private async void OnUIReload0CallBack(PanelRenderer panelRenderer, VisualElement root)
    {
        background_VisualElement0 = root.Q<VisualElement>("Background_VisualElement");
        door_Label0 = background_VisualElement0.Q<Label>("Door_Label");
        door_Label0.style.unityTextAlign = TextAnchor.MiddleRight;

        background_VisualElement0.RegisterCallback<ClickEvent>(OnVisualElementSelected);

        await Awaitable.WaitForSecondsAsync(1f);
        OnLanguageChanged();
        OnFontSizeChanged();
    }

    private async void OnUIReload1CallBack(PanelRenderer panelRenderer, VisualElement root)
    {
        background_VisualElement1 = root.Q<VisualElement>("Background_VisualElement");
        door_Label1 = background_VisualElement1.Q<Label>("Door_Label");
        door_Label1.style.unityTextAlign = TextAnchor.MiddleLeft;

        background_VisualElement1.RegisterCallback<ClickEvent>(OnVisualElementSelected);

        await Awaitable.WaitForSecondsAsync(1f);
        OnLanguageChanged();
        OnFontSizeChanged();
    }

    #region Events Manager

    private void OnLanguageChanged()
    {
        #region door_Label0
        if (isDoorClosed)
        {
            door_Label0.text = LanguageTextsData.open[SettingsData.currentLanguageIndex];
        }
        else
        {
            door_Label0.text = LanguageTextsData.close[SettingsData.currentLanguageIndex];
        }
        door_Label0.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        door_Label0.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region door_Label1
        if (isDoorClosed)
        {
            door_Label1.text = LanguageTextsData.open[SettingsData.currentLanguageIndex];
        }
        else
        {
            door_Label1.text = LanguageTextsData.close[SettingsData.currentLanguageIndex];
        }
        door_Label1.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        door_Label1.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region door_Label0
        door_Label0.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[1];
        #endregion

        #region door_Label1
        door_Label1.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[1];
        #endregion
    }

    #endregion


    private void OnVisualElementSelected(ClickEvent clickEvent)
    {
        if (isDoorClosed)
        {
            ActivateAnimation(openDoor_Hash);
            isDoorClosed = false;
            OnLanguageChanged();
        }
        else
        {
            ActivateAnimation(closeDoor_Hash);
            isDoorClosed = true;
            OnLanguageChanged();
        }
    }

    private void ActivateAnimation(int animation_Hash)
    {
        animator.SetBool(openDoor_Hash, false);
        animator.SetBool(closeDoor_Hash, false);

        animator.SetBool(animation_Hash, true);
    }
}

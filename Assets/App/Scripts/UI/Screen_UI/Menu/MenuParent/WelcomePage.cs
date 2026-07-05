using UnityEngine;
using UnityEngine.UIElements;


public class WelcomePage : MonoBehaviour
{
    PanelRenderer panelRenderer;
    MenuParent menuParent;

    VisualElement welcomePage_VisualElement;
    Label welcome_Label;
    VisualElement englishButton_TemplateContainer;
    Label english_Label;
    VisualElement farsiButton_TemplateContainer;
    Label farsi_Label;
    VisualElement startButton_TemplateContainer;
    Label start_Label;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        menuParent = GetComponent<MenuParent>();

        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;
    }

    private void OnDisable()
    {
        RemoveFunctionality();
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);

        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }

    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        welcomePage_VisualElement = root.Q<VisualElement>("WelcomePage_VisualElement");

        welcome_Label = welcomePage_VisualElement.Q<Label>("Welcome_Label");
        englishButton_TemplateContainer =
            welcomePage_VisualElement.Q<VisualElement>("EnglishButton_TemplateContainer");
        english_Label = englishButton_TemplateContainer.Q<Label>("Text_Label");
        farsiButton_TemplateContainer =
            welcomePage_VisualElement.Q<VisualElement>("FarsiButton_TemplateContainer");
        farsi_Label = farsiButton_TemplateContainer.Q<Label>("Text_Label");
        startButton_TemplateContainer =
            welcomePage_VisualElement.Q<VisualElement>("StartButton_TemplateContainer");
        start_Label = startButton_TemplateContainer.Q<Label>("Text_Label");


        InitializeUI();
    }

    private void InitializeUI()
    {
        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();

        if (SettingsData.currentLanguageIndex == 0)
        {
            ChangeSelectedLanguageTo(englishButton_TemplateContainer);
        }
        else if (SettingsData.currentLanguageIndex == 1)
        {
            ChangeSelectedLanguageTo(farsiButton_TemplateContainer);
        }
    }

    public void ChangeSelectedLanguageTo(VisualElement languageButton_TemplateContainer)
    {
        VisualElement englishButton_Background_VisualElement =
            englishButton_TemplateContainer.Q<VisualElement>("Background_VisualElement");

        VisualElement farsiButton_Background_VisualElement =
            farsiButton_TemplateContainer.Q<VisualElement>("Background_VisualElement");

        englishButton_Background_VisualElement.RemoveFromClassList("ButtonSelected");
        farsiButton_Background_VisualElement.RemoveFromClassList("ButtonSelected");

        languageButton_TemplateContainer.Q<VisualElement>("Background_VisualElement").
            AddToClassList("ButtonSelected");
    }


    #region Functionality

    private void AddFunctionality()
    {
        englishButton_TemplateContainer.RegisterCallback<ClickEvent>(OnEnglishButtenSelected);
        farsiButton_TemplateContainer.RegisterCallback<ClickEvent>(OnFarsiButtenSelected);
        startButton_TemplateContainer.RegisterCallback<ClickEvent>(OnStartButtenSelected);
    }

    private void RemoveFunctionality()
    {
        englishButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnEnglishButtenSelected);
        farsiButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnFarsiButtenSelected);
        startButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnStartButtenSelected);
    }

    private void OnEnglishButtenSelected(ClickEvent clickEvent)
    {
        ChangeSelectedLanguageTo(englishButton_TemplateContainer);

        SettingsData.currentLanguageIndex = 0;
        Settings_SaveSystem.Save_Settings();
        EventsManager.InvokeOnLanguageChanged();
    }

    private void OnFarsiButtenSelected(ClickEvent clickEvent)
    {
        ChangeSelectedLanguageTo(farsiButton_TemplateContainer);

        SettingsData.currentLanguageIndex = 1;
        Settings_SaveSystem.Save_Settings();
        EventsManager.InvokeOnLanguageChanged();
    }

    private void OnStartButtenSelected(ClickEvent clickEvent)
    {
        menuParent.SetPageActive(menuParent.menuTabsAndPages_VisualElement);
        SettingsData.currentSawWelcome = true;
        Settings_SaveSystem.Save_Settings();
    }

    #endregion


    #region Events Manager

    private void OnLanguageChanged()
    {
        #region Welcome_Label
        welcome_Label.text = LanguageTextsData.welcome[SettingsData.currentLanguageIndex];
        welcome_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        welcome_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region English_Label
        english_Label.text =
            LanguageTextsData.languages[0].language;
        english_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        english_Label.style.unityFont =
            LanguageTextsData.languages[0].font;
        #endregion

        #region Farsi_Label
        farsi_Label.text =
            LanguageTextsData.languages[1].language;
        farsi_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        farsi_Label.style.unityFont =
            LanguageTextsData.languages[1].font;
        #endregion

        #region Start_Label
        start_Label.text =
            LanguageTextsData.start[SettingsData.currentLanguageIndex];
        start_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        start_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region Welcome_Label
        welcome_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region English_Label
        english_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Farsi_Label
        farsi_Label.style.fontSize =
                LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Start_Label
        start_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion
}

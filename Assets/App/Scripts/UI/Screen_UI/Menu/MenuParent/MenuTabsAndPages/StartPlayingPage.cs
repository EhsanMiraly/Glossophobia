using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

public class StartPlayingPage : MonoBehaviour
{
    PanelRenderer panelRenderer;

    MenuParent menuParent;

    VisualElement startPlayingPage_VisualElement;


    #region PDF
    VisualElement pdf_VisualElement;

    ScrollView pdfsHolder_ScrollView;

    VisualElement deletePDFButton_TemplateContainer;
    Label deletePDFButton_Label;
    VisualElement addPDFButton_TemplateContainer;
    Label addPDFButton_Label;

    VisualTreeAsset pdfLabel_Template;
    List<VisualElement> pdfLabels;

    Label currentPDF_Label = null;
    #endregion



    #region Playing
    VisualElement playing_VisualElement;

    VisualElement timer_TemplateContainer;
    Label timer_Label;
    VisualElement hour_MinusButton_TemplateContainer;
    Label hour_Label;
    VisualElement hour_PlusButton_TemplateContainer;
    VisualElement minute_MinusButton_TemplateContainer;
    Label minute_Label;
    VisualElement minute_PlusButton_TemplateContainer;


    VisualElement startPlayingButton_TemplateContainer;
    Label startPlayingButton_Label;
    #endregion



    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        menuParent = GetComponent<MenuParent>();

        pdfLabel_Template = Resources.Load<VisualTreeAsset>("UI/BasicElements/PDFLabel_Template");
        pdfLabels = new List<VisualElement>();

        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;
    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
        RemoveFunctionality();

        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }

    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        startPlayingPage_VisualElement = root.Q<VisualElement>("StartPlayingPage_VisualElement");

        #region PDF
        pdf_VisualElement = startPlayingPage_VisualElement.Q<VisualElement>("PDF_VisualElement");

        pdfsHolder_ScrollView = pdf_VisualElement.Q<ScrollView>("PDFsHolder_ScrollView");
        UI_Utilities.Initialize_ScrollView(pdfsHolder_ScrollView);

        deletePDFButton_TemplateContainer = pdf_VisualElement.Q<VisualElement>("DeletePDFButton_TemplateContainer");
        deletePDFButton_Label = deletePDFButton_TemplateContainer.Q<Label>();
        addPDFButton_TemplateContainer = pdf_VisualElement.Q<VisualElement>("AddPDFButton_TemplateContainer");
        addPDFButton_Label = addPDFButton_TemplateContainer.Q<Label>();
        #endregion

        #region Playing
        playing_VisualElement = startPlayingPage_VisualElement.Q<VisualElement>("Playing_VisualElement");

        timer_TemplateContainer = playing_VisualElement.Q<VisualElement>("Timer_TemplateContainer");
        timer_Label = timer_TemplateContainer.Q<Label>("Timer_Label");
        hour_MinusButton_TemplateContainer =
            timer_TemplateContainer.Q<VisualElement>("Hour_MinusButton_TemplateContainer");
        hour_Label = timer_TemplateContainer.Q<Label>("Hour_Label");
        hour_PlusButton_TemplateContainer =
            timer_TemplateContainer.Q<VisualElement>("Hour_PlusButton_TemplateContainer");
        minute_MinusButton_TemplateContainer =
            timer_TemplateContainer.Q<VisualElement>("Minute_MinusButton_TemplateContainer");
        minute_Label = timer_TemplateContainer.Q<Label>("Minute_Label");
        minute_PlusButton_TemplateContainer =
            timer_TemplateContainer.Q<VisualElement>("Minute_PlusButton_TemplateContainer");

        startPlayingButton_TemplateContainer =
            playing_VisualElement.Q<VisualElement>("StartPlayingButton_TemplateContainer");
        startPlayingButton_Label = startPlayingButton_TemplateContainer.Q<Label>();

        #endregion

        InitializeUI();
    }

    private void InitializeUI()
    {
        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();

        UI_Utilities.MouseVisible(true);

        if (!Directory.Exists(GameData.pdfFilesFolderPath))
        {
            Directory.CreateDirectory(GameData.pdfFilesFolderPath);
        }
        ResetPDFsInScrollView();

    }


    #region UI Utilities

    private void ResetPDFsInScrollView()
    {
        pdfsHolder_ScrollView.Clear();

        List<string> PDFNames = PDF_Utilities.GetPDFsFileNames();
        pdfLabels = new List<VisualElement>();

        for (int i = 0; i < PDFNames.Count; i++)
        {
            VisualElement visualElement = pdfLabel_Template.Instantiate();
            visualElement.style.width = Length.Percent(100);
            visualElement.style.height = Screen.width / 20;
            visualElement.style.marginBottom = Screen.width / 100;

            visualElement.RegisterCallback<ClickEvent>(OnPDFSelected);

            pdfLabels.Add(visualElement);

            Label label = visualElement.Q<Label>();
            label.text = PDFNames[i];

            FixPDFLabel_Template(label);

            pdfsHolder_ScrollView.Add(visualElement);
        }

    }


    private void FixPDFLabel_Template(Label label)
    {
        label.languageDirection = LanguageTextsData.languages[1].languageDirection;
        label.style.unityFont = LanguageTextsData.languages[1].font;
        label.style.fontSize = LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
    }

    #endregion


    #region Functionality

    private void AddFunctionality()
    {
        deletePDFButton_TemplateContainer.RegisterCallback<ClickEvent>(OnDeletePDFButtonSelected);
        addPDFButton_TemplateContainer.RegisterCallback<ClickEvent>(OnAddPDFButtonSelected);

        hour_MinusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnHour_MinusButtonSelected);
        hour_PlusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnHour_PlusButtonSelected);

        minute_MinusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnMinute_MinusButtonSelected);
        minute_PlusButton_TemplateContainer.RegisterCallback<ClickEvent>(OnMinute_PlusButtonSelected);

        startPlayingButton_TemplateContainer.RegisterCallback<ClickEvent>(OnStartPlayingButtonSelected);
    }

    private void RemoveFunctionality()
    {
        deletePDFButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnDeletePDFButtonSelected);
        addPDFButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnAddPDFButtonSelected);

        hour_MinusButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnHour_MinusButtonSelected);
        hour_PlusButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnHour_PlusButtonSelected);

        minute_MinusButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnMinute_MinusButtonSelected);
        minute_PlusButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnMinute_PlusButtonSelected);

        startPlayingButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnStartPlayingButtonSelected);
    }


    private void OnDeletePDFButtonSelected(ClickEvent clickEvent)
    {
        if (currentPDF_Label != null)
        {
            PDF_Utilities.DeletePDF(currentPDF_Label.text);
            currentPDF_Label = null;
            ResetPDFsInScrollView();
        }
    }

    private async void OnAddPDFButtonSelected(ClickEvent clickEvent)
    {
        await PDF_FileDialogManager.OpenFileDialog();
        ResetPDFsInScrollView();
    }

    private void OnHour_MinusButtonSelected(ClickEvent clickEvent)
    {
        GameData.initialTimer.Hours--;
        if (GameData.initialTimer.Hours < 0)
        {
            GameData.initialTimer.Hours = 0;
        }
        hour_Label.text =
            LanguageTextsData.hour[SettingsData.currentLanguageIndex] + GameData.initialTimer.Hours;
    }

    private void OnHour_PlusButtonSelected(ClickEvent clickEvent)
    {
        GameData.initialTimer.Hours++;
        if (GameData.initialTimer.Hours > 10)
        {
            GameData.initialTimer.Hours = 10;
        }
        hour_Label.text =
            LanguageTextsData.hour[SettingsData.currentLanguageIndex] + GameData.initialTimer.Hours;
    }

    private void OnMinute_MinusButtonSelected(ClickEvent clickEvent)
    {
        GameData.initialTimer.Minutes--;
        if (GameData.initialTimer.Minutes < 0)
        {
            GameData.initialTimer.Minutes = 0;
        }
        minute_Label.text =
            LanguageTextsData.minute[SettingsData.currentLanguageIndex] + GameData.initialTimer.Minutes;
    }

    private void OnMinute_PlusButtonSelected(ClickEvent clickEvent)
    {
        GameData.initialTimer.Minutes++;
        if (GameData.initialTimer.Minutes > 59)
        {
            GameData.initialTimer.Minutes = 59;
        }
        minute_Label.text =
            LanguageTextsData.minute[SettingsData.currentLanguageIndex] + GameData.initialTimer.Minutes;
    }


    private async void OnStartPlayingButtonSelected(ClickEvent clickEvent)
    {
        if (currentPDF_Label != null) // And Scene not Loaded And Not Playing
        {
            using (LoadingWindow_PopUp loadingWindow_PopUp = new LoadingWindow_PopUp(new GameObject()))
            {
                loadingWindow_PopUp.SetProgress(10);

                EventsManager.InvokeOnSimulationStarted();
                loadingWindow_PopUp.SetProgress(20);

                UI_Utilities.MouseVisible(false);
                loadingWindow_PopUp.SetProgress(30);

                GameData.remainingTimer = new Timer(GameData.initialTimer.Hours,
                    GameData.initialTimer.Minutes, GameData.initialTimer.Seconds);
                loadingWindow_PopUp.SetProgress(40);

                await PdfToImage.StartConversion();
                loadingWindow_PopUp.SetProgress(50);

                menuParent.SetPageActive(menuParent.nothingPage_VisualElement);
                loadingWindow_PopUp.SetProgress(60);

                await Awaitable.WaitForSecondsAsync(2f);//Remove Later after Fixing Door Animation
                loadingWindow_PopUp.SetProgress(100);//Remove Later after Fixing Door Animation
            }
        }
    }


    private void OnPDFSelected(ClickEvent clickEvent)
    {
        if (currentPDF_Label != null)
        {
            currentPDF_Label.RemoveFromClassList("PDFSelected");
            currentPDF_Label.AddToClassList("PDFNotSelected");
        }

        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        Label label = visualElement.Q<Label>();

        currentPDF_Label = label;
        label.RemoveFromClassList("PDFNotSelected");
        label.AddToClassList("PDFSelected");

        GameData.selectedPdfFileFullPath = Path.Combine(GameData.pdfFilesFolderPath, label.text);
    }

    #endregion



    #region Events Manager

    private void OnLanguageChanged()
    {
        #region deletePDFButton_Label
        deletePDFButton_Label.text = LanguageTextsData.deletePDF[SettingsData.currentLanguageIndex];
        deletePDFButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        deletePDFButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region addPDFButton_Label
        addPDFButton_Label.text = LanguageTextsData.addPDF[SettingsData.currentLanguageIndex];
        addPDFButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        addPDFButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region startPlayingButton_Label
        startPlayingButton_Label.text = LanguageTextsData.startPlaying[SettingsData.currentLanguageIndex];
        startPlayingButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        startPlayingButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        for (int i = 0; i < pdfLabels.Count; i++)
        {
            Label label = pdfLabels[i].Q<Label>();
            FixPDFLabel_Template(label);
        }

        #region Timer
        timer_Label.text = LanguageTextsData.timer[SettingsData.currentLanguageIndex];
        timer_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        timer_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        hour_Label.text =
            LanguageTextsData.hour[SettingsData.currentLanguageIndex] + GameData.initialTimer.Hours;
        hour_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        hour_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

        minute_Label.text =
            LanguageTextsData.minute[SettingsData.currentLanguageIndex] + GameData.initialTimer.Minutes;
        minute_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        minute_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region deletePDFButton_Label
        deletePDFButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region addPDFButton_Label
        addPDFButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region startPlayingButton_Label
        startPlayingButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        for (int i = 0; i < pdfLabels.Count; i++)
        {
            Label label = pdfLabels[i].Q<Label>();
            FixPDFLabel_Template(label);
        }

        #region Timer
        timer_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        hour_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];

        minute_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion

}

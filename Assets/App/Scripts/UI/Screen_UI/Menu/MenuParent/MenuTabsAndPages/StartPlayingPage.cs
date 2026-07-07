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


    #region Playing
    VisualElement playing_VisualElement;
    #endregion


    #region PDF
    VisualElement pdf_VisualElement;

    ScrollView pdfsHolder_ScrollView;

    VisualElement deletePDFButton_TemplateContainer;
    Label deletePDFButton_Label;
    VisualElement addPDFButton_TemplateContainer;
    Label addPDFButton_Label;
    VisualElement goForwardButton_TemplateContainer;
    Label goForwardButton_Label;
    #endregion


    VisualTreeAsset pdfLabel_Template;
    List<VisualElement> pdfLabels;

    Label currentPDF_Label = null;



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

        #region Playing
        playing_VisualElement = startPlayingPage_VisualElement.Q<VisualElement>("Playing_VisualElement");
        #endregion

        #region PDF
        pdf_VisualElement = startPlayingPage_VisualElement.Q<VisualElement>("PDF_VisualElement");

        pdfsHolder_ScrollView = pdf_VisualElement.Q<ScrollView>("PDFsHolder_ScrollView");
        UI_Utilities.Initialize_ScrollView(pdfsHolder_ScrollView);

        deletePDFButton_TemplateContainer = pdf_VisualElement.Q<VisualElement>("DeletePDFButton_TemplateContainer");
        deletePDFButton_Label = deletePDFButton_TemplateContainer.Q<Label>();
        addPDFButton_TemplateContainer = pdf_VisualElement.Q<VisualElement>("AddPDFButton_TemplateContainer");
        addPDFButton_Label = addPDFButton_TemplateContainer.Q<Label>();
        goForwardButton_TemplateContainer = pdf_VisualElement.Q<VisualElement>("GoForwardButton_TemplateContainer");
        goForwardButton_Label = goForwardButton_TemplateContainer.Q<Label>();
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
        label.style.fontSize = LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
    }

    #endregion


    #region Functionality

    private void AddFunctionality()
    {
        deletePDFButton_TemplateContainer.RegisterCallback<ClickEvent>(OnDeletePDFButtonSelected);
        addPDFButton_TemplateContainer.RegisterCallback<ClickEvent>(OnAddPDFButtonSelected);
        goForwardButton_TemplateContainer.RegisterCallback<ClickEvent>(OnGoForwardButtonSelected);
    }

    private void RemoveFunctionality()
    {
        deletePDFButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnDeletePDFButtonSelected);
        addPDFButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnAddPDFButtonSelected);
        goForwardButton_TemplateContainer.UnregisterCallback<ClickEvent>(OnGoForwardButtonSelected);
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

    private async void OnGoForwardButtonSelected(ClickEvent clickEvent)
    {
        if (currentPDF_Label != null)
        {
            await PdfToImage.StartConversion();///////////////////////////////////////////////
            Debug.Log(GameData.pageTextures.Count);/////////////OK -Delete

            UI_Utilities.MouseVisible(false);

            menuParent.SetPageActive(menuParent.nothingPage_VisualElement);
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

        #region goForwardButton_Label
        goForwardButton_Label.text = LanguageTextsData.goForward[SettingsData.currentLanguageIndex];
        goForwardButton_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        goForwardButton_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        for (int i = 0; i < pdfLabels.Count; i++)
        {
            Label label = pdfLabels[i].Q<Label>();
            FixPDFLabel_Template(label);
        }
    }

    private void OnFontSizeChanged()
    {
        #region deletePDFButton_Label
        deletePDFButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region addPDFButton_Label
        addPDFButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region goForwardButton_Label
        goForwardButton_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        for (int i = 0; i < pdfLabels.Count; i++)
        {
            Label label = pdfLabels[i].Q<Label>();
            FixPDFLabel_Template(label);
        }
    }

    #endregion

}

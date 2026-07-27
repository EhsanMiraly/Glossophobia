using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Laptop : MonoBehaviour
{
    [SerializeField] GameObject laptop_Screen;
    private Material laptopScreenMaterial;
    [SerializeField] GameObject projector_Screen;
    private Material projectorScreenMaterial;
    private float emissionIntensity = 1.1f;


    private int slideIndex = 0;
    private bool isShowingSlides = false;


    PanelRenderer panelRenderer;

    VisualElement startPage_VisualElement;
    Label start_Label;

    VisualElement controllerPage_VisualElement;
    VisualElement chevronLeft_TemplateContainer;
    VisualElement chevronRight_TemplateContainer;

    VisualElement endPage_VisualElement;
    Label end_Label;


    private PlayerInput playerInput;
    private PlayerInput.UIActions uI;



    private void OnEnable()
    {
        laptopScreenMaterial = laptop_Screen.GetComponent<Renderer>().material;
        projectorScreenMaterial = projector_Screen.GetComponent<Renderer>().material;

        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;

        playerInput = new PlayerInput();
        uI = playerInput.UI;
        uI.Enable();
        uI.LastSlide.performed += context => { OnChevronLeftSelected(new ClickEvent()); };
        uI.NextSlide.performed += context => { OnChevronRightSelected(new ClickEvent()); };
    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
        RemoveFunctionality();

        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;

        uI.LastSlide.performed -= context => { OnChevronLeftSelected(new ClickEvent()); };
        uI.NextSlide.performed -= context => { OnChevronRightSelected(new ClickEvent()); };
        uI.Disable();
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        startPage_VisualElement = root.Q<VisualElement>("StartPage_VisualElement");
        start_Label = startPage_VisualElement.Q<Label>("Start_Label");

        controllerPage_VisualElement = root.Q<VisualElement>("ControllerPage_VisualElement");
        chevronLeft_TemplateContainer =
            controllerPage_VisualElement.Q<VisualElement>("ChevronLeft_TemplateContainer");
        chevronRight_TemplateContainer =
            controllerPage_VisualElement.Q<VisualElement>("ChevronRight_TemplateContainer");

        endPage_VisualElement = root.Q<VisualElement>("EndPage_VisualElement");
        end_Label = endPage_VisualElement.Q<Label>("End_Label");


        InitializeUI();
    }

    private void InitializeUI()
    {
        //Move else where
        //if (SettingsData.currentSawLaptopHint)
        //SetPageActive(menuTabsAndPages_VisualElement);

        AddFunctionality();

        OnLanguageChanged();
        OnFontSizeChanged();

        SetPageActive(startPage_VisualElement);
    }


    #region Functionality

    private void AddFunctionality()
    {
        start_Label.RegisterCallback<ClickEvent>(OnStartLabelSelected);
        chevronLeft_TemplateContainer.RegisterCallback<ClickEvent>(OnChevronLeftSelected);
        chevronRight_TemplateContainer.RegisterCallback<ClickEvent>(OnChevronRightSelected);
        end_Label.RegisterCallback<ClickEvent>(OnEndLabelSelected);
    }

    private void RemoveFunctionality()
    {
        start_Label.UnregisterCallback<ClickEvent>(OnStartLabelSelected);
        chevronLeft_TemplateContainer.UnregisterCallback<ClickEvent>(OnChevronLeftSelected);
        chevronRight_TemplateContainer.UnregisterCallback<ClickEvent>(OnChevronRightSelected);
        end_Label.UnregisterCallback<ClickEvent>(OnEndLabelSelected);
    }


    private void OnStartLabelSelected(ClickEvent clickEvent)
    {
        OnEnableEmission();
        SetScreensEmissionColorToWhite();
        slideIndex = 0;
        isShowingSlides = true;
        SetSlideInScreens(slideIndex);

        SetPageActive(controllerPage_VisualElement);
        EventsManager.InvokeOnClockStarted();
    }

    private void OnChevronLeftSelected(ClickEvent clickEvent)
    {
        if (!isShowingSlides)
        {
            return;
        }

        slideIndex--;
        if (slideIndex < 0)
        {
            slideIndex = 0;
        }

        SetSlideInScreens(slideIndex);
    }

    private void OnChevronRightSelected(ClickEvent clickEvent)
    {
        if (!isShowingSlides)
        {
            return;
        }

        slideIndex++;
        if (slideIndex > GameData.pageTextures.Count - 1)
        {
            isShowingSlides = false;
            SetPageActive(endPage_VisualElement);
            ResetSlideInScreens();
            OnDisableEmission();
        }
        else
        {
            SetSlideInScreens(slideIndex);
        }
    }

    private void OnEndLabelSelected(ClickEvent clickEvent)
    {
        endPage_VisualElement.style.display = DisplayStyle.None;
        EventsManager.InvokeOnClockEnded();
    }

    #endregion


    #region Events Manager

    private void OnLanguageChanged()
    {
        #region start_Label
        start_Label.text = LanguageTextsData.start[SettingsData.currentLanguageIndex];
        start_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        start_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region end_Label
        end_Label.text = LanguageTextsData.end[SettingsData.currentLanguageIndex];
        end_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        end_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region start_Label
        start_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySuperSmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region end_Label
        end_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySuperSmall[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion


    public void SetPageActive(VisualElement page)
    {
        startPage_VisualElement.style.display = DisplayStyle.None;
        controllerPage_VisualElement.style.display = DisplayStyle.None;
        endPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }

    public void SetSlideInScreens(int slideIndex)
    {
        laptopScreenMaterial.SetTexture("_EmissionMap", GameData.pageTextures[slideIndex]);
        projectorScreenMaterial.SetTexture("_EmissionMap", GameData.pageTextures[slideIndex]);
    }

    public void SetScreensEmissionColorToBlack()
    {
        laptopScreenMaterial.SetColor("_EmissionColor", Color.black * emissionIntensity);
        projectorScreenMaterial.SetColor("_EmissionColor", Color.black * emissionIntensity);
    }

    public void ResetSlideInScreens()
    {
        laptopScreenMaterial.SetTexture("_EmissionMap", null);
        projectorScreenMaterial.SetTexture("_EmissionMap", null);
        SetScreensEmissionColorToBlack();
    }


    public void SetScreensEmissionColorToWhite()
    {
        laptopScreenMaterial.SetColor("_EmissionColor", Color.white * emissionIntensity);
        projectorScreenMaterial.SetColor("_EmissionColor", Color.white * emissionIntensity);
    }



    public void OnEnableEmission()
    {
        laptopScreenMaterial.EnableKeyword("_EMISSION");
        projectorScreenMaterial.EnableKeyword("_EMISSION");
    }
    public void OnDisableEmission()
    {
        laptopScreenMaterial.DisableKeyword("_EMISSION");
        projectorScreenMaterial.DisableKeyword("_EMISSION");
    }


}

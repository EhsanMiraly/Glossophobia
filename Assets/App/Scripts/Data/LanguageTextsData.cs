using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LanguageTextsData
{
    #region Fonts

    public static List<Font> fonts = new List<Font>()
    {
        Resources.Load<Font>("Fonts/English/Roboto-Medium"),
        Resources.Load<Font>("Fonts/Farsi/Parastoo-Bold")
    };

    #endregion


    #region LoadingPage
    public static List<string> loading = new List<string> { "Loading...", "در حال لود شدن..." };
    #endregion


    #region ManuParent


    #region WelcomePage
    public static List<string> welcome = new List<string> { "Welcome", "خوش آمدید" };
    public static List<string> start = new List<string> { "Start", "شروع" };
    #endregion


    #region MenuTabsAndPages


    #region Tabs
    public static List<string> account = new List<string> { "Account", "اکانت" };
    //3
    public static List<string> settings = new List<string> { "Settings", "تنظیمات" };
    #endregion


    #region Pages


    #region AccountPage
    public static List<string> logIn = new List<string> { "Log in", "ورود به حساب کاربری" };
    #endregion


    #region SettingsPage

    public static List<Language> languages = new List<Language>
    {
        new Language("English", LanguageDirection.LTR, fonts[0]),
        new Language("فارسی", LanguageDirection.RTL,fonts[1])
    };

    public static List<FontSize> fontSize_Text = new List<FontSize>
    {
        new FontSize("Font size: Small","اندازه فونت: کوچک"),
        new FontSize("Font size: Average","اندازه فونت: متوسط"),
        new FontSize("Font size: Big","اندازه فونت: بزرگ")
    };


    public static List<int> fontSize_CategorySmall = new List<int> { 10, 20, 30 };
    public static List<int> fontSize_CategoryAverage = new List<int> { 20, 40, 60 };
    public static List<int> fontSize_CategoryBig = new List<int> { 40, 70, 100 };

    public static List<string> soundVolume = new List<string> { "Sound volume: ", "بلندی صدا: " };

    public static List<string> targetFrameRate = new List<string> { "Target frame rate: ", "نرخ فریم هدف: " };
    public static List<int> targetFrameRates = new List<int> { 60, 90, 120, 144, 165, 240, 300 };

    public static List<string> fieldOfView = new List<string> { "Field of view: ", "زاویه دید: " };
    public static List<int> fieldOfViews = new List<int> { 60, 65, 70, 75, 80, 85, 90 };

    public static List<string> moveSpeed = new List<string> { "Move speed: ", "سرعت حرکت: " };
    public static List<string> horizontalSensitivity =
        new List<string> { "Horizontal sensitivity: ", "حساسیت افقی: " };
    public static List<string> verticalSensitivity =
        new List<string> { "Vertical sensitivity: ", "حساسیت عمودی: " };

    #endregion


    #endregion

    #endregion


    #endregion

}

public class Language
{
    public string language { get; }
    public LanguageDirection languageDirection { get; }
    public Font font { get; }

    public Language(string language, LanguageDirection languageDirection, Font font)
    {
        this.language = language;
        this.languageDirection = languageDirection;
        this.font = font;
    }
}

public class FontSize
{
    public List<string> FontSizeLanguage { get; }

    public FontSize(string fontSizeEnglish, string fontSizeFarsi)
    {
        FontSizeLanguage = new List<string> { fontSizeEnglish, fontSizeFarsi };
    }
}

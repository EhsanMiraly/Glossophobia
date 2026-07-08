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
    public static List<string> startPlaying = new List<string> { "Start playing", "شروع بازی" };
    //3
    public static List<string> settings = new List<string> { "Settings", "تنظیمات" };
    #endregion


    #region Pages


    #region AccountPage
    public static List<string> logIn = new List<string> { "Log in", "ورود به حساب کاربری" };
    public static List<string> username = new List<string> { "Username", "نام کاربری" };
    public static List<string> enterUsername = new List<string> { "Enter username???", "نام کاربری را وارد کنید" };
    public static List<string> password = new List<string> { "Password", "رمز عبور" };
    public static List<string> enterPassword = new List<string> { "Enter password??", "رمز عبور را وارد کنید" };
    public static List<string> repeatPassword = new List<string> { "Repeat password", "تکرار رمز عبور" };
    public static List<string> enterRepeatPassword = new List<string> {
        "Enter repeat password???", "تکرار رمز عبور را وارد کنید" };

    public static List<string> wrongUsernameOrPassword = new List<string> {
        "Wrong username or password", "نام کاربری یا رمز عبور اشتباه است" };

    public static List<string> signUpPage = new List<string> { "Sign up page", "صفحه ایجاد حساب کاربری" };
    public static List<string> logInPage = new List<string> { "Log in page", "صفحه ورود به حساب کاربری" };

    public static List<string> signUp = new List<string> { "Sign up", "ایجاد حساب کاربری" };
    public static List<string> wrongCharacter = new List<string> { "Wrong character", "کاراکتر اشتباه" };
    public static List<string> usernameLength = new List<string> {
        "Username length must be 8 characters or more", "نام کاربری باید 8 کاراکتر یا بیشتر باشد" };
    public static List<string> passwordLength = new List<string> {
        "Password length must be 8 characters or more", "رمز عبور باید 8 کاراکتر یا بیشتر باشد" };
    public static List<string> passwordConfirmationPassword = new List<string> {
        "The password does not match the confirmation password", "رمز عبور با تکرار رمز عبور همخوانی ندارد" };
    public static List<string> howTo = new List<string> { "Usable characters: ", "کاراکترهای قابل استفاده: " };

    public static List<string> youAreSignedInAs = new List<string> {
        "You are signed in as", "شما با این عنوان وارد شده‌اید" };

    public static List<string> logOut = new List<string> {
        "Log out", "خروج از حساب کاربری" };

    #endregion

    #region StartPlayingPage

    public static List<string> deletePDF = new List<string> { "Delete pdf", "حذف فایل" };
    public static List<string> addPDF = new List<string> { "Add pdf", "اضافه کردن فایل" };
    public static List<string> goForward = new List<string> { "Go Forward", "برو جلو" };

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




    #region Laptop
    public static List<string> end = new List<string> { "End", "پایان" };
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

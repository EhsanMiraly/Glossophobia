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
        //,Resources.Load<Font>("Fonts/Clock/Clock_Font")
    };

    #endregion


    #region LoadingPage
    public static List<string> loading = new List<string> { "Loading...", "در حال لود شدن..." };
    #endregion


    #region MenuParent


    #region WelcomePage
    public static List<string> welcome = new List<string> { "Welcome", "خوش آمدید" };
    public static List<string> start = new List<string> { "Start", "شروع" };
    #endregion


    #region MenuTabsAndPages


    #region Tabs
    public static List<string> account = new List<string> { "Account", "حساب کاربری" };
    public static List<string> demographics = new List<string> { "Demographics", "اطلاعات جمعیت‌شناختی" };
    public static List<string> PRPSA = new List<string> { "PRPSA", "پرسشنامه خودارزیابی" };
    public static List<string> personalReportOfPublicSpeakingAnxiety =
        new List<string> { "Personal report of public speaking anxiety", "گزارش شخصی اضطراب سخنرانی در جمع" };
    public static List<string> startPlaying = new List<string> { "Start playing", "شروع بازی" };
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


    #region Demographics
    public static List<string> gender = new List<string> { "Gender", "جنسیت" };
    public static List<TwoStrings> genderList = new List<TwoStrings>
    {
        new TwoStrings("Gender: Male","جنسیت: مرد"),
        new TwoStrings("Gender: Female","جنسیت: زن"),
        new TwoStrings("Gender: Other","جنسیت: سایر")
    };

    public static List<string> age = new List<string> { "Age: ", "سن: " };

    public static List<string> educationLevel = new List<string> { "Education level", "سطح تحصیلات" };

    public static List<TwoStrings> educationLevelList = new List<TwoStrings>()
    {
        new TwoStrings("High School Diploma or Below","؟؟؟"),
        new TwoStrings("Bachelor’s Degree","؟؟؟"),
        new TwoStrings("Master’s Degree","؟؟؟"),
        new TwoStrings("Doctorate (PhD)","؟؟؟"),
        new TwoStrings("Other","؟؟؟")
    };

    public static string fieldOfStudy = "Field of study";
    public static List<string> fieldOfStudys_List_DropdownField =
    new List<string>() { "Engineering & Technology","Natural Sciences","Humanities & Social Sciences",
    "Medical & Health Sciences","Arts & Architecture","Business & Management",
    "Information Technology & Computer Science","Other"};

    public static string job = "Job";
    public static List<string> jobs_List_DropdownField =
    new List<string>() { "Full-time Employed","Part-time Employed","Student","Homemaker",
    "Unemployed / Looking for Work","Retired","Other"};

    public static string LevelOfExperience = "Level of public speaking experience";
    public static string LevelOfNeed = "Level of need for public speaking";
    public static string TypicalLevelOfAnxiety = "Typical level of anxiety when speaking in public";
    public static string HistoryOfFormalTraining = "History of formal training in public speaking";
    public static string HistoryOfTakingSedativeMedication = "History of taking sedative medication to reduce stress and anxiety during public speaking";

    public static List<string> VeryLowToVeryHigh = new List<string>() { "Very low", "Low", "Medium", "High", "Very high" };
    public static List<string> YesNo = new List<string>() { "Yes", "No" };
    public static string similarExperience = "Similar experience info";
    public static string experienceWith3DGames = "Level of prior experience with 3D games";
    public static string experienceWithPublicSpeakingSimulationGames = "Level of prior experience with public speaking simulation games";


    public static List<string> save = new List<string> { "Save", "ذخیره" };
    #endregion

    #region PRPSA

    ///////////////////////////////////////////////////////////
    #endregion


    #region StartPlayingPage

    public static List<string> deletePDF = new List<string> { "Delete pdf", "حذف فایل" };
    public static List<string> addPDF = new List<string> { "Add pdf", "اضافه کردن فایل" };
    public static List<string> goForward = new List<string> { "Go Forward", "برو جلو" };

    #endregion


    #region SettingsPage

    public static List<Language> languages = new List<Language>
    {
        new Language("Language: English", LanguageDirection.LTR, fonts[0]),
        new Language("زبان: فارسی", LanguageDirection.RTL,fonts[1])
    };

    public static List<TwoStrings> fontSize_Text = new List<TwoStrings>
    {
        new TwoStrings("Font size: Small","اندازه فونت: کوچک"),
        new TwoStrings("Font size: Average","اندازه فونت: متوسط"),
        new TwoStrings("Font size: Big","اندازه فونت: بزرگ")
    };


    public static List<int> fontSize_CategorySuperSmall = new List<int> { 2, 4, 6 };
    public static List<int> fontSize_CategorySmall = new List<int> { 10, 15, 20 };
    public static List<int> fontSize_CategoryAverage = new List<int> { 20, 25, 30 };
    public static List<int> fontSize_CategoryBig = new List<int> { 40, 60, 80 };

    public static List<string> soundVolume = new List<string> { "Sound volume: ", "بلندی صدا: " };

    public static List<string> frameRate = new List<string> { "Frame rate: ", "نرخ فریم: " };
    public static List<int> frameRates = new List<int> { 60, 90, 120, 144, 165, 240, 300 };

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


public class TwoStrings
{
    public List<string> ListString { get; }

    public TwoStrings(string english, string farsi)
    {
        ListString = new List<string> { english, farsi };
    }
}

using System.Collections.Generic;
using UnityEngine;

public class PersianTexts
{
    public static string persianLanguage = "Persian";

    //General
    public static string back = "بازگشت";

    #region Account

    //Login_Page And SignUp_Page
    public static string email = "ایمیل";
    public static string enterYourEmail = "ایمیل خود را وارد کنید";
    public static string password = "رمز عبور";
    public static string enterYourPassword = "رمز عبور خود را وارد کنید (6 کاراکتر یا بیشتر)";

    //Login_Page
    public static string LogInToYourAccount = "وارد حساب کاربری خود شوید";
    public static string logIn = "ورود";
    public static string forgotPassword = "فراموشی رمز عبور";
    public static string dontHaveAnAccount = "حساب کاربری ندارید؟";
    public static string signUp = "ثبت نام";
    public static string wrongEmailOrPassword = "ایمیل یا رمز عبور اشتباه است !";

    //ResetPassword Page
    public static string resetPasswordviaLink = "بازنشانی رمزعبور از طریق ارسال لینک بازنشانی به ایمیل";
    public static string sendResetLink = "ارسال لینک بازنشانی";
    public static string emailNotFound = "ایمیل پیدا نشد.\n";
    public static string passwordResetEmailHasBeenSent = "ایمیل بازنشانی رمز عبور ارسال شد.\n";
    //Reset Resul Errors

    //SignUp_Page
    public static string makeNewAccount = "ساخت حساب جدید";
    public static string repeatPassword = "تکرار رمز عبور";
    public static string enterYourPasswordAgain = "رمز عبور خود را دوباره وارد کنید ...";
    public static string makeAccount = "ایجاد حساب کاربری";

    public static string possibleProblems = "مشکلات احتمالی: ";
    public static string passwordAndRepeatPasswordDontMatch = "رمز عبور و تکرار آن مطابقت ندارند.\n";
    public static string invalidEmail = "ایمیل نامعتبر.\n";
    public static string passwordMustBe6LettersOrMore = "رمزعبور باید 6 کاراکتر و یا بیشتر باید.\n";
    public static string emailIsAlreadyInUse = "ایمیل از قبل وجود دارد.\n";


    #region ChangeAccount_Page
    public static string youAreLoggedInAs = "شما با این نام کاربری وارد شده اید:";
    public static string signOut = "خروج از حساب کاربری";
    public static string areYouSure = "اطمینان دارید؟";
    public static string yes = "بله";
    public static string no = "خیر";
    public static string deleteAccount = "حذف حساب کاربری";
    #endregion

    #endregion

    #region Demographics

    #region BasicInfo01_Page
    public static string enterYourDemographics = "این فرم را پر کنید";
    public static string gender = "جنسیت";

    public static List<string> genders_List_RadioButtonGroup =
    new List<string>() { "مرد", "زن", "سایر" };

    public static string age = "سن";

    public static string educationLevel = "سطح تحصیلات";
    public static List<string> educationLevels_List_DropdownField =
    new List<string>() { "دیپلم و کمتر", "کارشناسی", "کارشناسی ارشد", "دکتری", "سایر" };

    public static string fieldOfStudy = "زمینه تحصیلات";
    public static List<string> fieldOfStudys_List_DropdownField =
    new List<string>() { "فنی و مهندسی","علوم پایه","علوم انسانی و اجتماعی","پزشکی و سلامت",
    "هنر و معماری","مدیریت و کسب‌وکار","فناوری اطلاعات و کامپیوتر","سایر"};

    public static string job = "شغل";
    public static List<string> jobs_List_DropdownField =
    new List<string>() { "شاغل تمام‌وقت", "شاغل پاره‌وقت", "دانشجو", "خانه‌دار", "بیکار و در جستجوی کار",
     "بازنشسته", "سایر" };

    public static string next = "بعدی";
    #endregion

    #region BasicInfo02_Page
    public static string LevelOfExperience = "سطح تجربه سخنرانی در جمع";
    public static string LevelOfNeed = "سطح نیاز به سخنرانی در جمع";
    public static string TypicalLevelOfAnxiety = "سطح اضطراب معمول هنگام سخنرانی در جمع";
    public static string HistoryOfFormalTraining = "سابقه آموزش رسمی برای سخنرانی در جمع";
    public static string HistoryOfTakingSedativeMedication = "سابقه مصرف داروی آرام بخش برای کاهش استرس و اضطراب هنگام سخنرانی در جمع";

    public static List<string> VeryLowToVeryHigh = new List<string>() { "خیلی کم", "کم", "متوسط", "زیاد", "خیلی زیاد" };
    public static List<string> YesNo = new List<string>() { "بله", "خیر" };
    #endregion

    #region BasicInfo03_Page
    public static string similarExperience = "اطلاعات تجربه مشابه";
    public static string experienceWith3DGames = "سطح تجربه قبلی با بازی‌های 3 بعدی";
    public static string experienceWithPublicSpeakingSimulationGames = "سطح تجربه قبلی با بازی‌های شبیه‌ساز سخنرانی در جمع";

    #endregion

    #region ChangeBasicInfo_Page
    public static string doYouWantToChangeTheFormInformation = "آیا می‌خواهید اطلاعات فرم را تغییر دهید؟";
    public static string changeInformation = "تغییر اطلاعات";
    #endregion

    #endregion

    #region PRPSA

    #region Explain_Page
    public static string explain = "لطفاً هر جمله‌ای را که پس از زدن دکمه «شروع» می‌بینید با دقت بخوانید و مشخص کنید تا چه حد با آن موافق یا مخالف هستید.\nهیچ پاسخ درست یا غلطی وجود ندارد؛ فقط نظر و احساس واقعی خودتان مهم است.\nلطفاً به همهٔ سؤالات پاسخ دهید و همان گزینه‌ای را انتخاب کنید که ابتدا به ذهنتان می‌رسد. نیازی به فکر کردن زیاد نیست.";
    public static string start = "شروع";
    #endregion

    #region Questions_Page
    public static List<string> questions = new List<string>()
    {
        "وقتی قرار است در جمع صحبت کنم، ضربان قلبم بالا می‌رود.",
        "قبل از سخنرانی، احساس دل‌درد یا دل‌شوره می‌کنم.",
        "هنگام ارائه، احساس می‌کنم دست‌ها یا صدایم می‌لرزد.",
        "نزدیک شروع ارائه، احساس تنگی نفس یا خشکی دهان دارم.",
        "هنگام سخنرانی، نمی‌توانم به‌طور کامل روی تنفسم کنترل داشته باشم.",
        "نگرانم که مخاطبان فکر کنند من اطلاعات کافی ندارم.",
        "می‌ترسم هنگام ارائه، اشتباه کنم و دیگران متوجه شوند.",
        "هنگام صحبت کردن، توجه زیادی به واکنش چهره‌ی مخاطبان دارم.",
        "هنگام ارائه، مدام نگرانم که موضوع را خوب انتقال ندهم.",
        "اگر یک مکث یا وقفه پیش بیاید، فوراً تصور می‌کنم ارائه‌ام خراب شده است.",
        "احساس می‌کنم توانایی ارائه‌ی یک سخنرانی خوب را دارم.",
        "وقتی شروع به صحبت می‌کنم، رفته‌رفته احساس بهتری پیدا می‌کنم.",
        "هنگام ارائه، می‌توانم نسبتاً آرام بمانم.",
        "معمولاً از اینکه در جمع دیده شوم حس بدی پیدا نمی‌کنم.",
        "اگر از قبل آماده باشم، اعتماد دارم ارائه‌ام موفق خواهد بود.",
        "اگر امکان داشته باشد، سعی می‌کنم از سخنرانی در جمع دوری کنم.",
        "در حین ارائه، گاهی ذهنم قفل می‌کند.",
        "قبل از سخنرانی، مدام به احتمال اشتباه فکر می‌کنم.",
        "بعد از ارائه، زیاد به اینکه «کجا بد بودم» فکر می‌کنم.",
        "وقتی قرار است صحبت کنم، فکرهای منفی مزاحم تمرکزم می‌شوند."
    };
    public static string yourChoice = "انتخاب شما";
    public static List<string> stronglyDisagreeToStronglyAgree = new List<string>()
    {
        "کاملاً مخالفم",
        "مخالفم",
        "نه موافقم، نه مخالف",
        "موافقم",
        "کاملاً موافقم"
    };
    #endregion

    #region ThankYou_Page
    public static string thankYou = "ممنون";
    #endregion

    #endregion


    #region PDFFileUploader
    public static string uploadNewPDF = "آپلود PDF جدید";
    public static string selectExistingPDF = "انتخاب PDF موجود جهت ارائه یا حذف";
    public static string finalizeSelection = "نهایی‌سازی انتخاب";
    public static string deleteSelected = "حذف PDF انتخاب شده";
    #endregion

    #region ChangePDF
    public static string setPresentationTime = "تنظیم محدودیت زمانی ارائه";
    public static string minus = "کم کردن";
    public static string hours = "ساعت";
    public static string minutes = "دقیقه";
    public static string plus = "اضافه کردن";
    public static string selectedPDFIs = "PDF انتخاب شده";
    public static string changePDF = "تغییر PDF";
    #endregion

    #region Slides_UI
    public static string startPresentation = "شروع ارائه";
    public static string last = "قبلی";
    public static string end = "پایان";
    public static string endPresentation = "پایان ارائه";
    #endregion

    #region Settings
    public static string gameSettings = "تنظیمات بازی";
    public static string targetFrameRate = "نرخ فریم هدف: ";
    public static string fieldOfView = "زاویه دید: ";
    public static string moveSpeed = "سرعت حرکت: ";
    public static string mouseSensitivity = "حساسیت موس";
    public static string horizontal = "افقی: ";
    public static string vertical = "عمودی: ";


    #endregion


}

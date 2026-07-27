using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LanguageTextsData
{
    public static List<Font> fonts;

    public static List<Language> languages;


    #region LoadingPage_PopUp
    public static List<string> loading = new List<string> { "Loading...", "در حال لود شدن..." };
    #endregion

    #region MessageWindow_PopUp
    public static List<string> answerEveryThing =
        new List<string> { "Answer every thing", "به همه چیز جواب بدید" };
    public static List<string> thereIsSomethingWrong =
    new List<string> { "There is something wrong.", "مشکلی پیش آمده است." };
    public static List<string> thereIsSomethingWrongWithYourAccount = new List<string>
        { "There is something wrong with your account.", "مشکلی برای حساب شما پیش آمده است." };

    public static List<string> unavailable = new List<string>
        { "Unable to connect to the server. Please check your internet connection and try again.",
        "امکان اتصال به سرور وجود ندارد. لطفاً اتصال اینترنت خود را بررسی کنید و دوباره تلاش کنید." };
    public static List<string> deadlineExceeded = new List<string>
        { "The request took too long. Please check your internet connection and try again.",
        "درخواست بیش از حد طول کشید. لطفاً اتصال اینترنت خود را بررسی کنید و دوباره تلاش کنید." };
    public static List<string> unauthenticated = new List<string>
        { "Your session has expired. Please log in again.",
        "نشست شما منقضی شده است. لطفاً دوباره وارد حساب خود شوید." };

    public static List<string> ok =
        new List<string> { "Ok", "باشه" };
    #endregion


    #region Door_UI
    public static List<string> open = new List<string> { "Open", "باز کن" };
    public static List<string> close = new List<string> { "Close", "ببند" };
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
    public static List<string> baselinePRPSA = new List<string> { "Baseline PRPSA", "پرسشنامه خودارزیابی پایه" };
    public static List<string> personalReportOfPublicSpeakingAnxiety =
        new List<string> { "Personal report of public speaking anxiety", "گزارش شخصی اضطراب سخنرانی در جمع" };
    public static List<string> startPlaying = new List<string> { "Start playing", "شروع بازی" };
    public static List<string> settings = new List<string> { "Settings", "تنظیمات" };
    #endregion


    #region Pages


    #region AccountPage
    public static List<string> logIn = new List<string> { "Log in", "ورود به حساب کاربری" };
    public static List<string> enterEmail = new List<string> { "Enter email", "ایمیل را وارد کنید" };
    public static List<string> enterPassword = new List<string> { "Enter password", "رمز عبور را وارد کنید" };
    public static List<string> enterRepeatPassword = new List<string> {
        "Enter repeat password???", "تکرار رمز عبور را وارد کنید" };

    public static List<string> wrongEmailOrPassword = new List<string> {
        "Wrong email or password.", "ایمیل یا رمز عبور اشتباه است." };

    public static List<string> signUpPage = new List<string> { "Sign up page", "صفحه ایجاد حساب کاربری" };
    public static List<string> logInPage = new List<string> { "Log in page", "صفحه ورود به حساب کاربری" };

    public static List<string> signUp = new List<string> { "Sign up", "ایجاد حساب کاربری" };
    public static List<string> passwordConfirmationPassword = new List<string> {
        "The password does not match the confirmation password", "رمز عبور با تکرار رمز عبور همخوانی ندارد" };

    public static List<string> youAreSignedInAs = new List<string> {
        "You are signed in as", "شما با این عنوان وارد شده‌اید" };

    public static List<string> logOut = new List<string> {
        "Log out", "خروج از حساب کاربری" };

    public static List<string> invalidEmail = new List<string> {
        "Please enter a valid email address.", "لطفاً یک آدرس ایمیل معتبر وارد کنید." };
    public static List<string> weakPassword = new List<string> {
        "Your password is too weak. Please use a stronger password.",
        "رمز عبور شما ضعیف است. لطفاً از رمز عبور قوی‌تری استفاده کنید." };
    public static List<string> emailAlreadyInUse = new List<string> {
        "This email address is already registered. Please use another email or log in.",
        "این ایمیل قبلاً ثبت شده است. لطفاً از ایمیل دیگری استفاده کنید یا وارد شوید." };
    public static List<string> networkRequestFailed = new List<string> {
        "Unable to connect to the server. Please check your internet connection.",
        "اتصال به سرور برقرار نشد. لطفاً اتصال اینترنت خود را بررسی کنید." };
    public static List<string> tooManyRequests = new List<string> {
        "Too many attempts. Please try again later.",
        "تعداد درخواست‌ها بیش از حد مجاز است. لطفاً بعداً دوباره تلاش کنید." };
    public static List<string> operationNotAllowed = new List<string> {
        "This sign-in method is currently unavailable.",
        "این روش ثبت‌نام در حال حاضر در دسترس نیست." };
    public static List<string> unknownError = new List<string> {
        "An unknown error occurred. Please try again.",
        "یک خطای ناشناخته رخ داد. لطفاً دوباره تلاش کنید." };

    #endregion


    #region Demographics

    public static List<string> changeDemographics =
        new List<string> { "Change demographics", "تغییر اطلاعات جمعیت‌شناختی" };

    public static List<string> gender = new List<string> { "Gender", "جنسیت" };
    public static List<TwoStrings> genderList = new List<TwoStrings>
    {
        new TwoStrings("Male","مرد"),
        new TwoStrings("Female","زن"),
        new TwoStrings("Other","سایر")
    };

    public static List<string> ageGroup = new List<string> { "Age group", "گروه سنی" };
    public static List<TwoStrings> ageGroupList = new List<TwoStrings>
    {
        new TwoStrings("Child _ Under 12 years","کودک _ کمتر از ۱۲ سال"),
        new TwoStrings("Adolescent _ 12–17 years","نوجوان _ ۱۲ تا ۱۷ سال"),
        new TwoStrings("Young Adult _ 18–25 years","جوان _ ۱۸ تا ۲۵ سال"),
        new TwoStrings("Early Adulthood _ 26–35 years","بزرگسالی اولیه _ ۲۶ تا ۳۵ سال"),
        new TwoStrings("Middle Adulthood _ 36–55 years","بزرگسالی میانی _ ۳۶ تا ۵۵ سال"),
        new TwoStrings("Older Adult _ 56–65 years","بزرگسال مسن _ ۵۶ تا ۶۵ سال"),
        new TwoStrings("Elderly / Older Adult _ Above 65 years","سالمند _ بالاتر از ۶۵ سال")
    };

    public static List<string> educationLevel = new List<string> { "Education level", "سطح تحصیلات" };
    public static List<TwoStrings> educationLevelList = new List<TwoStrings>()
    {
        new TwoStrings("High School Diploma or Below","دیپلم و کمتر"),
        new TwoStrings("Bachelor’s Degree","کارشناسی"),
        new TwoStrings("Master’s Degree","کارشناسی ارشد"),
        new TwoStrings("Doctorate (PhD)","دکتری"),
        new TwoStrings("Other","سایر")
    };

    public static List<string> fieldOfStudy = new List<string> { "Field of study", "زمینه تحصیلات" };
    public static List<TwoStrings> fieldOfStudyList = new List<TwoStrings>()
    {
        new TwoStrings("Engineering & Technology","فنی و مهندسی"),
        new TwoStrings("Natural Sciences","علوم پایه"),
        new TwoStrings("Humanities & Social Sciences","علوم انسانی و اجتماعی"),
        new TwoStrings("Medical & Health Sciences","پزشکی و سلامت"),
        new TwoStrings("Arts & Architecture","هنر و معماری"),
        new TwoStrings("Business & Management","مدیریت و کسب‌وکار"),
        new TwoStrings("Information Technology & Computer Science","فناوری اطلاعات و کامپیوتر"),
        new TwoStrings("Other","سایر")
    };

    public static List<string> job = new List<string> { "Job", "شغل" };
    public static List<TwoStrings> jobList = new List<TwoStrings>()
    {
        new TwoStrings("Full-time Employed","شاغل تمام‌وقت"),
        new TwoStrings("Part-time Employed","شاغل پاره‌وقت"),
        new TwoStrings("Student","دانشجو"),
        new TwoStrings("Homemaker","خانه‌دار"),
        new TwoStrings("Unemployed / Looking for Work","بیکار و در جستجوی کار"),
        new TwoStrings("Retired","بازنشسته"),
        new TwoStrings("Other","سایر")
    };

    public static List<TwoStrings> veryLowToVeryHigh = new List<TwoStrings>()
    {
        new TwoStrings("Very low","خیلی کم"),
        new TwoStrings("Low","کم"),
        new TwoStrings("Medium","متوسط"),
        new TwoStrings("High","زیاد"),
        new TwoStrings("Very high","خیلی زیاد")
    };

    public static List<TwoStrings> yesNo = new List<TwoStrings>()
    {
        new TwoStrings("Yes","بله"),
        new TwoStrings("No","خیر")
    };

    public static List<string> levelOfExperience = new List<string>
        { "Level of public speaking experience", "سطح تجربه سخنرانی در جمع" };
    public static List<string> levelOfNeed = new List<string>
        { "Level of need for public speaking", "سطح نیاز به سخنرانی در جمع" };
    public static List<string> levelOfAnxiety = new List<string>
        { "Typical level of anxiety when speaking in public", "سطح اضطراب معمول هنگام سخنرانی در جمع" };
    public static List<string> formalTraining = new List<string>
        { "History of formal training in public speaking", "سابقه آموزش رسمی برای سخنرانی در جمع" };
    public static List<string> takingMedication = new List<string>
        { "History of taking sedative medication to reduce stress and anxiety during public speaking",
          "سابقه مصرف داروی آرام بخش برای کاهش استرس و اضطراب هنگام سخنرانی در جمع" };
    public static List<string> games3D = new List<string>
        { "Level of prior experience with 3D games", "سطح تجربه قبلی با بازی‌های 3 بعدی" };
    public static List<string> simulationGames = new List<string>
        { "Level of prior experience with public speaking simulation games",
          "سطح تجربه قبلی با بازی‌های شبیه‌ساز سخنرانی در جمع" };

    public static List<string> save = new List<string> { "Save", "ذخیره" };
    #endregion

    #region PRPSA
    public static List<string> explainPRPSA = new List<string>
    {
        "Please read each statement that appears after you press “Start” and indicate how much you agree or disagree with it.\nThere are no right or wrong answers; only your true feelings and opinions matter.\nMake sure to answer all the questions, and choose the option that first comes to mind without overthinking.",
        "لطفاً هر جمله‌ای را که پس از زدن دکمه «شروع» می‌بینید با دقت بخوانید و مشخص کنید تا چه حد با آن موافق یا مخالف هستید.\nهیچ پاسخ درست یا غلطی وجود ندارد؛ فقط نظر و احساس واقعی خودتان مهم است.\nلطفاً به همهٔ سؤالات پاسخ دهید و همان گزینه‌ای را انتخاب کنید که ابتدا به ذهنتان می‌رسد. نیازی به فکر کردن زیاد نیست."
    };

    public static List<TwoStrings> baselinePRPSAQuestions = new List<TwoStrings>()
    {
        new TwoStrings("My heart rate increases when I am about to speak in front of an audience.",
            "وقتی قرار است در جمع صحبت کنم، ضربان قلبم بالا می‌رود."),
        new TwoStrings("Before a presentation, I feel stomach discomfort or nervous butterflies.",
            "قبل از سخنرانی، احساس دل‌درد یا دل‌شوره می‌کنم."),
        new TwoStrings("During a presentation, I feel that my hands or voice are shaking.",
            "هنگام ارائه، احساس می‌کنم دست‌ها یا صدایم می‌لرزد."),
        new TwoStrings("Right before starting my speech, I experience shortness of breath or a dry mouth.",
            "نزدیک شروع ارائه، احساس تنگی نفس یا خشکی دهان دارم."),
        new TwoStrings("While speaking, I feel I cannot fully control my breathing.",
            "هنگام سخنرانی، نمی‌توانم به‌طور کامل روی تنفسم کنترل داشته باشم."),
        new TwoStrings("I worry that the audience thinks I don’t have enough knowledge.",
            "نگرانم که مخاطبان فکر کنند من اطلاعات کافی ندارم."),
        new TwoStrings("I am afraid I will make a mistake during the presentation and others will notice it.",
            "می‌ترسم هنگام ارائه، اشتباه کنم و دیگران متوجه شوند."),
        new TwoStrings("While speaking, I pay a lot of attention to the audience’s facial reactions.",
            "هنگام صحبت کردن، توجه زیادی به واکنش چهره‌ی مخاطبان دارم."),
        new TwoStrings("During my presentation, I constantly worry that I am not conveying the topic well.",
            "هنگام ارائه، مدام نگرانم که موضوع را خوب انتقال ندهم."),
        new TwoStrings("If I pause or hesitate even briefly, I immediately assume my presentation is going badly.",
            "اگر یک مکث یا وقفه پیش بیاید، فوراً تصور می‌کنم ارائه‌ام خراب شده است."),
        new TwoStrings("I feel I am capable of giving a good presentation.",
            "احساس می‌کنم توانایی ارائه‌ی یک سخنرانی خوب را دارم."),
        new TwoStrings("Once I start speaking, I gradually feel more comfortable.",
            "وقتی شروع به صحبت می‌کنم، رفته‌رفته احساس بهتری پیدا می‌کنم."),
        new TwoStrings("I am able to stay relatively calm during a presentation.",
            "هنگام ارائه، می‌توانم نسبتاً آرام بمانم."),
        new TwoStrings("I usually don’t feel bad about being observed by others.",
            "معمولاً از اینکه در جمع دیده شوم حس بدی پیدا نمی‌کنم."),
        new TwoStrings("If I am well-prepared, I am confident that my presentation will be successful.",
            "اگر از قبل آماده باشم، اعتماد دارم ارائه‌ام موفق خواهد بود."),
        new TwoStrings("If possible, I try to avoid speaking in front of an audience.",
            "اگر امکان داشته باشد، سعی می‌کنم از سخنرانی در جمع دوری کنم."),
        new TwoStrings("During presentations, my mind sometimes freezes or goes blank.",
            "در حین ارائه، گاهی ذهنم قفل می‌کند."),
        new TwoStrings("Before speaking, I constantly think about the possibility of making mistakes.",
            "قبل از سخنرانی، مدام به احتمال اشتباه فکر می‌کنم."),
        new TwoStrings("After a presentation, I worry a lot about what I did wrong.",
            "بعد از ارائه، زیاد به اینکه «کجا بد بودم» فکر می‌کنم."),
        new TwoStrings("When I am about to speak, negative thoughts interfere with my focus.",
            "وقتی قرار است صحبت کنم، فکرهای منفی مزاحم تمرکزم می‌شوند.")
    };

    public static List<string> yourChoice = new List<string> { "Your choice", "انتخاب شما" };

    public static List<TwoStrings> stronglyDisagreeToStronglyAgree = new List<TwoStrings>()
    {
        new TwoStrings("Strongly Disagree","کاملاً مخالفم"),
        new TwoStrings("Disagree","مخالفم"),
        new TwoStrings("Neither Agree nor Disagree","نه موافقم، نه مخالف"),
        new TwoStrings("Agree","موافقم"),
        new TwoStrings("Strongly Agree","کاملاً موافقم")
    };

    public static List<string> finish = new List<string> { "Finish", "پایان" };

    public static List<string> last = new List<string> { "Last", "قبلی" };
    public static List<string> next = new List<string> { "Next", "بعدی" };

    public static List<string> changePRPSA =
        new List<string> { "Change PRPSA", "تغییر پرسشنامه خودارزیابی" };

    #endregion


    #region StartPlayingPage

    public static List<string> deletePDF = new List<string> { "Delete pdf", "حذف فایل" };
    public static List<string> addPDF = new List<string> { "Add pdf", "اضافه کردن فایل" };
    //public static List<string> startPlaying = new List<string> { "Go Forward", "برو جلو" };

    public static List<string> timer = new List<string> { "Timer", "زمان سنج" };
    public static List<string> hour = new List<string> { "Hour: ", "ساعت: " };
    public static List<string> minute = new List<string> { "Minute: ", "دقیقه: " };
    #endregion


    #region SettingsPage



    public static List<TwoStrings> fontSize_Text = new List<TwoStrings>
    {
        new TwoStrings("Font size: Small","اندازه فونت: کوچک"),
        new TwoStrings("Font size: Average","اندازه فونت: متوسط"),
        new TwoStrings("Font size: Big","اندازه فونت: بزرگ")
    };


    public static List<int> fontSize_CategorySuperSmall = new List<int> { 2, 4, 6 };
    public static List<int> fontSize_CategorySmall = new List<int> { 12, 16, 20 };
    public static List<int> fontSize_CategoryAverage = new List<int> { 20, 24, 28 };
    public static List<int> fontSize_CategoryBig = new List<int> { 35, 40, 45 };

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
    public static List<string> laptopHint = new List<string>
    {
        "You can also navigate through the slides using the left and right arrow keys on your keyboard.",
        "همچنین می‌توانید با استفاده از کلیدهای چپ و راست صفحه‌کلید، بین اسلایدها جابه‌جا شوید."
    };

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

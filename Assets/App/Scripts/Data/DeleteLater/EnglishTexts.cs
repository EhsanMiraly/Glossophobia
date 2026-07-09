using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using Unity.VisualScripting;
using UnityEngine;

public class EnglishTexts
{
    public static string englishLanguage = "English";

    //General
    public static string back = "Back";

    #region Account

    #region Login_Page And SignUp_Page
    public static string email = "Email";
    public static string enterYourEmail = "Enter your email (3 Characters or more)";
    public static string password = "Password";
    public static string enterYourPassword = "Enter your password (6 Characters or more)";
    #endregion

    //Login_Page
    public static string LogInToYourAccount = "Log in to your account";
    public static string logIn = "Log in";
    public static string forgotPassword = "Forgot password";
    public static string dontHaveAnAccount = "Don't have an account?";
    public static string signUp = "Sign up";
    public static string wrongEmailOrPassword = "Wrong email or password !";

    //ResetPassword Page
    public static string resetPasswordviaLink = "Reset password by sending a reset link to the email";
    public static string sendResetLink = "Send reset link";
    public static string emailNotFound = "Email not found.\n";
    public static string passwordResetEmailHasBeenSent = "Password reset email has been sent.\n";
    //Reset Resul Errors

    #region SignUp_Page
    public static string makeNewAccount = "Create a new account";
    public static string repeatPassword = "Repeat password";
    public static string enterYourPasswordAgain = "Enter your password again ...";
    public static string makeAccount = "Create an account";

    public static string possibleProblems = "Possible problems: ";
    public static string passwordAndRepeatPasswordDontMatch = "Password and repeat password dont match.\n";
    public static string invalidEmail = "Invalid email.\n";
    public static string passwordMustBe6LettersOrMore = "Password must be 6 letters or more.\n";
    public static string emailIsAlreadyInUse = "Email is already in use.\n";

    #endregion

    #region ChangeAccount_Page
    public static string youAreLoggedInAs = "You are logged in as:";
    public static string signOut = "Sign out";
    public static string areYouSure = "Are you sure?";
    public static string yes = "Yes";
    public static string no = "No";
    public static string deleteAccount = "Delete Account";
    #endregion


    #endregion

    #region Demographics

    #region BasicInfo01_Page
    public static string FillOutThisForm = "Fill out this form";
    public static string gender = "Gender";

    public static List<string> genders_List_RadioButtonGroup =
    new List<string>() { "Male", "Female", "Other" };

    public static string age = "Age";

    public static string educationLevel = "Education level";
    public static List<string> educationLevels_List_DropdownField =
    new List<string>() { "High School Diploma or Below", "Bachelor’s Degree", "Master’s Degree", "Doctorate (PhD)", "Other" };

    public static string fieldOfStudy = "Field of study";
    public static List<string> fieldOfStudys_List_DropdownField =
    new List<string>() { "Engineering & Technology","Natural Sciences","Humanities & Social Sciences",
    "Medical & Health Sciences","Arts & Architecture","Business & Management",
    "Information Technology & Computer Science","Other"};

    public static string job = "Job";
    public static List<string> jobs_List_DropdownField =
    new List<string>() { "Full-time Employed","Part-time Employed","Student","Homemaker",
    "Unemployed / Looking for Work","Retired","Other"};

    public static string next = "Next";
    #endregion

    #region BasicInfo02_Page
    public static string LevelOfExperience = "Level of public speaking experience";
    public static string LevelOfNeed = "Level of need for public speaking";
    public static string TypicalLevelOfAnxiety = "Typical level of anxiety when speaking in public";
    public static string HistoryOfFormalTraining = "History of formal training in public speaking";
    public static string HistoryOfTakingSedativeMedication = "History of taking sedative medication to reduce stress and anxiety during public speaking";

    public static List<string> VeryLowToVeryHigh = new List<string>() { "Very low", "Low", "Medium", "High", "Very high" };
    public static List<string> YesNo = new List<string>() { "Yes", "No" };
    #endregion

    #region BasicInfo03_Page
    public static string similarExperience = "Similar experience info";
    public static string experienceWith3DGames = "Level of prior experience with 3D games";
    public static string experienceWithPublicSpeakingSimulationGames = "Level of prior experience with public speaking simulation games";

    #endregion

    #region ChangeBasicInfo_Page
    public static string doYouWantToChangeTheFormInformation = "Do you want to change the form information?";
    public static string changeInformation = "Change information";
    #endregion

    #endregion

    #region PRPSA

    #region Explain_Page
    public static string explain = "Please read each statement that appears after you press “Start” and indicate how much you agree or disagree with it.\nThere are no right or wrong answers; only your true feelings and opinions matter.\nMake sure to answer all the questions, and choose the option that first comes to mind without overthinking.";
    public static string start = "Start";
    #endregion

    #region Questions_Page
    public static List<string> questions = new List<string>()
    {
        "My heart rate increases when I am about to speak in front of an audience.",
        "Before a presentation, I feel stomach discomfort or nervous butterflies.",
        "During a presentation, I feel that my hands or voice are shaking.",
        "Right before starting my speech, I experience shortness of breath or a dry mouth.",
        "While speaking, I feel I cannot fully control my breathing.",
        "I worry that the audience thinks I don’t have enough knowledge.",
        "I am afraid I will make a mistake during the presentation and others will notice it.",
        "While speaking, I pay a lot of attention to the audience’s facial reactions.",
        "During my presentation, I constantly worry that I am not conveying the topic well.",
        "If I pause or hesitate even briefly, I immediately assume my presentation is going badly.",
        "I feel I am capable of giving a good presentation.",
        "Once I start speaking, I gradually feel more comfortable.",
        "I am able to stay relatively calm during a presentation.",
        "I usually don’t feel bad about being observed by others.",
        "If I am well-prepared, I am confident that my presentation will be successful.",
        "If possible, I try to avoid speaking in front of an audience.",
        "During presentations, my mind sometimes freezes or goes blank.",
        "Before speaking, I constantly think about the possibility of making mistakes.",
        "After a presentation, I worry a lot about what I did wrong.",
        "When I am about to speak, negative thoughts interfere with my focus."
    };
    public static string yourChoice = "Your choice";
    public static List<string> stronglyDisagreeToStronglyAgree = new List<string>()
    {
        "Strongly Disagree",
        "Disagree",
        "Neither Agree nor Disagree",
        "Agree",
        "Strongly Agree"
    };
    #endregion

    #region ThankYou_Page
    public static string thankYou = "Thank you";
    #endregion

    #endregion

    #region PDF

    #region PDFFileUploader
    public static string uploadNewPDF = "Upload new PDF";
    public static string selectExistingPDF = "Select existing PDF for presentation or deletion";
    public static string finalizeSelection = "Finalize selection";
    public static string deleteSelected = "Delete selected PDF";

    #endregion

    #region ChangePDF
    public static string setPresentationTime = "Set presentation time limit";
    public static string minus = "Minus";
    public static string hours = "Hours";
    public static string minutes = "Minutes";
    public static string plus = "Plus";
    public static string selectedPDFIs = "Selected PDF is";
    public static string changePDF = "Change PDF";
    #endregion

    #endregion

    #region Slides_UI
    public static string startPresentation = "Start presentation";
    public static string last = "Last";
    public static string end = "End";

    public static string endPresentation = "End presentation";
    #endregion

    #region Settings
    public static string gameSettings = "Game settings";
    public static string targetFrameRate = "Target frame rate: ";
    public static string fieldOfView = "Field of view: ";
    public static string moveSpeed = "Move speed: ";
    public static string mouseSensitivity = "Mouse sensitivity";
    public static string horizontal = "Horizontal: ";
    public static string vertical = "Vertical: ";


    #endregion


}

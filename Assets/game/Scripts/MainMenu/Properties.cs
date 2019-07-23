using UnityEngine;
using System;
/*
    Afrikaans = 0,
    Arabic = 1,
    Basque = 2,
    Belarusian = 3,
    Bulgarian = 4,
    Catalan = 5,
    Chinese = 6,
    Czech = 7,
    Danish = 8,
    Dutch = 9,
    English = 10,
    Estonian = 11,
    Faroese = 12,
    Finnish = 13,
    French = 14,
    German = 0xF,
    Greek = 0x10,
    Hebrew = 17,
    [Obsolete ("Use SystemLanguage.Hungarian instead (UnityUpgradable) -> Hungarian", true)]
    Hugarian = 18,
    Icelandic = 19,
    Indonesian = 20,
    Italian = 21,
    Japanese = 22,
    Korean = 23,
    Latvian = 24,
    Lithuanian = 25,
    Norwegian = 26,
    Polish = 27,
    Portuguese = 28,
    Romanian = 29,
    Russian = 30,
    SerboCroatian = 0x1F,
    Slovak = 0x20,
    Slovenian = 33,
    Spanish = 34,
    Swedish = 35,
    Thai = 36,
    Turkish = 37,
    Ukrainian = 38,
    Vietnamese = 39,
    ChineseSimplified = 40,
    ChineseTraditional = 41,
    Unknown = 42,
    Hungarian = 18
*/

/*
 * 국가 코드 
 * https://eminwon.qia.go.kr/common/CountrySP.jsp
 */
/* 
{
   { SystemLanguage.Afrikaans, "ZA" },
   { SystemLanguage.Arabic    , "SA" },
   { SystemLanguage.Basque    , "US" },
   { SystemLanguage.Belarusian    , "BY" },
   { SystemLanguage.Bulgarian    , "BJ" },
   { SystemLanguage.Catalan    , "ES" },
   { SystemLanguage.Chinese    , "CN" },
   { SystemLanguage.Czech    , "HK" },
   { SystemLanguage.Danish    , "DK" },
   { SystemLanguage.Dutch    , "BE" },
   { SystemLanguage.English    , "US" },
   { SystemLanguage.Estonian    , "EE" },
   { SystemLanguage.Faroese    , "FU" },
   { SystemLanguage.Finnish    , "FI" },
   { SystemLanguage.French    , "FR" },
   { SystemLanguage.German    , "DE" },
   { SystemLanguage.Greek    , "JR" },
   { SystemLanguage.Hebrew    , "IL" },
   { SystemLanguage.Icelandic    , "IS" },
   { SystemLanguage.Indonesian    , "ID" },
   { SystemLanguage.Italian    , "IT" },
   { SystemLanguage.Japanese    , "JP" },
   { SystemLanguage.Korean    , "KR" },
   { SystemLanguage.Latvian    , "LV" },
   { SystemLanguage.Lithuanian    , "LT" },
   { SystemLanguage.Norwegian    , "NO" },
   { SystemLanguage.Polish    , "PL" },
   { SystemLanguage.Portuguese    , "PT" },
   { SystemLanguage.Romanian    , "RO" },
   { SystemLanguage.Russian    , "RU" },
   { SystemLanguage.SerboCroatian    , "SP" },
   { SystemLanguage.Slovak    , "SK" },
   { SystemLanguage.Slovenian    , "SI" },
   { SystemLanguage.Spanish    , "ES" },
   { SystemLanguage.Swedish    , "SE" },
   { SystemLanguage.Thai    , "TH" },
   { SystemLanguage.Turkish    , "TR" },
   { SystemLanguage.Ukrainian    , "UA" },
   { SystemLanguage.Vietnamese    , "VN" },
   { SystemLanguage.ChineseSimplified    , "CN" },
   { SystemLanguage.ChineseTraditional    , "CN" },
   { SystemLanguage.Unknown    , "US" },
   { SystemLanguage.Hungarian    , "HU" },
   };
*/

public class TextFontInfo
{
    public string Text { get; }
    public Font Font { get; }
    public int FontSize { get; }

    private TextFontInfo()
    { }

    public TextFontInfo(string text, string fontName, int fontSize)
    {
        Text = text;
        Font = (Font)Resources.Load(fontName);
        FontSize = fontSize;
    }
};

/*
 * 현재 지원 언어 
 * 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)     
 */
public static class Properties
{
    private static string[] COUNTRY_NAME =
    {
        /*00*/"Afrikaans"    , "Arabic"           , "Basque"            , "Belarusian", "Bulgarian",
        /*05*/"Catalan"      , "Chinese"          , "Czech"             , "Danish"    , "Dutch"    ,             
        /*10*/"English"      , "Estonian"         , "Faroese"           , "Finnish"   , "French"   , 
        /*15*/"German"       , "Greek"            , "Hebrew"            , "Hugarian"  ,
        /*19*/"Icelandic"    , "Indonesian"       , "Italian"           , "Japanese"  , 
        /*23*/"Korean"       , "Latvian"          , "Lithuanian"        , "Norwegian" , 
        /*27*/"Polish"       , "Portuguese"       , "Romanian"          , "Russian"   , 
        /*31*/"SerboCroatian", "Slovak"           , "Slovenian"         , "Spanish"   , 
        /*35*/"Swedish"      , "Thai"             , "Turkish"           , "Ukrainian" , 
        /*39*/"Vietnamese"   , "ChineseSimplified", "ChineseTraditional", "Unknown"
    };

    private static string[] SUPPORT_LANGUAGE =
    {
        // 한국어, 영어, 러시아어, 베트남어, 태국어, 스페인어
        "Korean",
        "English",
        "Russian",
        "Vietnamese",
        "Thai",
        "Spanish",
    };

    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] MAIN_MENU_TITLE =
    {
        // 한국어, 영어, 러시아어, 베트남어, 태국어, 스페인어
        "운석을 피해라!",
        "Avoid meteorites!",
        "Избегайте метеоритов!",
        "Tránh thiên thạch!",
        "หลีกเลี่ยงอุกกาบาต!",
        "¡Evita los meteoritos!",
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] MAIN_MENU_TITLE_FONT =
    {
        /*00*/"fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF",
        /*05*/"fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF",
        /*10*/"fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF",
        /*15*/"fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF",
        /*19*/"fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF",
        /*23*/"fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF",
        /*27*/"fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF",
        /*31*/"fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF",
        /*35*/"fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF",
        /*39*/"fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF","fonts/JalnanOTF"
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static int[] MAIN_MENU_TITLE_FONTSIZE =
    {
        /*00*/80,80,80,80,80,
        /*05*/80,80,80,80,80,
        /*10*/80,80,80,80,80,
        /*15*/80,80,80,80,
        /*19*/80,80,80,80,
        /*23*/80,80,80,80,
        /*27*/80,80,80,80,
        /*31*/80,80,80,80,
        /*35*/80,80,80,80,
        /*39*/80,80,80,80
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] INDICATE_OFFLINE_MODE_MESSAGE =
    {
        // 한국어, 영어, 러시아어, 베트남어, 태국어, 스페인어
        "인터넷에 연결되어 있지 않습니다",
        "Disconnected the internet",
        "Вы не подключены к Интернету",
        "Bạn không được kết nối với Internet",
        "คุณไม่ได้เชื่อมต่อกับอินเทอร์เน็ต",
        "Usted no está conectado a Internet",
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] DATABASE_CONNECTION_ERROR_MESSAGE =
    {
        // 한국어, 영어, 러시아어, 베트남어, 태국어, 스페인어
        "서버 문제로 로그인을 할 수 없습니다",
        "Checking Server",
        "Проверка сервера",
        "Kiểm tra máy chủ",
        "กำลังตรวจสอบเซิร์ฟเวอร์",
        "Comprobar servidor",
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] LOGIN_SUCCESS_MESSAGE =
    {
        // 한국어, 영어, 러시아어, 베트남어, 태국어, 스페인어
        "로그인 성공",
        "Login Succeed",
        "Успешный вход в систему",
        "Đăng nhập thành công",
        "เข้าสู่ระบบสำเร็จ",
        "Inicia sesión con éxito",
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] LOGIN_FAIL_MESSAGE =
    {
        // 한국어, 영어, 러시아어, 베트남어, 태국어, 스페인어
        "로그인 실패",
        "Login Failed",
        "Ошибка входа",
        "Đăng nhập thất bại",
        "การเข้าสู่ระบบล้มเหลว",
        "error de inicio de sesion",
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] PROCEED_TO_THE_PROLOGUE_MESSAGE =
    {
        // 한국어, 영어, 러시아어, 베트남어, 태국어, 스페인어
        "프롤로그로 진행 합니다",
        "Proceed to the prologue",
        "Ошибка входа",
        "Đăng nhập thất bại",
        "การเข้าสู่ระบบล้มเหลว",
        "error de inicio de sesion",
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] EXIT_POPUP_MESSAGE =
    {
        // 한국어, 영어, 러시아어, 베트남어, 태국어, 스페인어
        "다시 돌아올거라 믿습니다!",
        "I believe that you will be back soon",
        "Ошибка входа",
        "Đăng nhập thất bại",
        "การเข้าสู่ระบบล้มเหลว",
        "error de inicio de sesion",
    };

    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] UPDATE_POPUP_MESSAGE =
    {
        // 한국어, 영어, 러시아어, 베트남어, 태국어, 스페인어
        "최신 버전이 존재합니다.\n업데이트 하시겠습니까?",
        "The latest version exists.\nDo you want to update?",
        "TODO",
        "TODO",
        "TODO",
        "TODO",
    };

    public static TextFontInfo GetMainMenuTitleText()
    {
        int idx = GetCurrentLanguageIndex();
        TextFontInfo tfi = new TextFontInfo(MAIN_MENU_TITLE[idx], MAIN_MENU_TITLE_FONT[idx], MAIN_MENU_TITLE_FONTSIZE[idx]);
        return tfi;
    }

    public static TextFontInfo GetIndicateOfflineModeMessageText()
    {
        int idx = GetCurrentLanguageIndex();
        TextFontInfo tfi = new TextFontInfo(INDICATE_OFFLINE_MODE_MESSAGE[idx], MAIN_MENU_TITLE_FONT[idx], MAIN_MENU_TITLE_FONTSIZE[idx]);
        return tfi;
    }

    public static string GetDatabaseConnectionErrorMessage()
    {
        return DATABASE_CONNECTION_ERROR_MESSAGE[GetCurrentLanguageIndex()];
    }

    public static string GetLoginSucceedMessage()
    {
        return LOGIN_SUCCESS_MESSAGE[GetCurrentLanguageIndex()];
    }

    public static string GetLoginFailedMessage()
    {
        return LOGIN_FAIL_MESSAGE[GetCurrentLanguageIndex()];
    }

    public static string GetProceedToPrologueMessage()
    {
        return PROCEED_TO_THE_PROLOGUE_MESSAGE[GetCurrentLanguageIndex()];
    }

    public static string GetExitPopupMessage()
    {
        return EXIT_POPUP_MESSAGE[GetCurrentLanguageIndex()];
    }

    public static string GetUpdatePopupMessage()
    {
        return UPDATE_POPUP_MESSAGE[GetCurrentLanguageIndex()];
    }

    public static int GetCurrentLanguageIndex()
    {
        int retVal = 1; // default
        SystemLanguage sysLang = Application.systemLanguage;
        string curLang = sysLang.ToString();
        int i;
        for (i = 0; i < SUPPORT_LANGUAGE.Length; i++)
        {
            if (curLang.Equals(SUPPORT_LANGUAGE[i]))
            {
                retVal = i;
                break;
            }
        }
        Debug.Log("GetCurrentLanguageIndex (curLang : " + curLang + ", index : " + i + ")");
        return i;
    }
}


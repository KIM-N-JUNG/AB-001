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

    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] MAIN_MENU_TITLE =
    {
        /*00*/"Avoid meteorites!","Tránh thiên thạch!","Avoid meteorites!","Avoid meteorites!","Avoid meteorites!",
        /*05*/"Avoid meteorites!","Avoid meteorites!","Avoid meteorites!","Avoid meteorites!","Avoid meteorites!",
        /*10*/"Avoid meteorites!","Avoid meteorites!","Avoid meteorites!","Avoid meteorites!","Avoid meteorites!",
        /*15*/"Avoid meteorites!","Avoid meteorites!","Avoid meteorites!","Avoid meteorites!",
        /*19*/"Avoid meteorites!","Avoid meteorites!","Avoid meteorites!","Avoid meteorites!",
        /*23*/"운석을 피해라!","Avoid meteorites!","Avoid meteorites!","Avoid meteorites!",
        /*27*/"Avoid meteorites!","Avoid meteorites!","Avoid meteorites!","Избегайте метеоритов!",
        /*31*/"Avoid meteorites!","Avoid meteorites!","Avoid meteorites!","¡Evita los meteoritos!",
        /*35*/"Avoid meteorites!","หลีกเลี่ยงอุกกาบาต!","Avoid meteorites!","Avoid meteorites!",
        /*39*/"Tránh các thiên thạch!","Avoid meteorites!","Avoid meteorites!","Avoid meteorites!"
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] MAIN_MENU_TITLE_FONT =
    {
        /*00*/"fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush",
        /*05*/"fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush",
        /*10*/"fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush",
        /*15*/"fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush",
        /*19*/"fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush",
        /*23*/"fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush",
        /*27*/"fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush",
        /*31*/"fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush",
        /*35*/"fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush",
        /*39*/"fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush","fonts/NanumBrush"
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static int[] MAIN_MENU_TITLE_FONTSIZE =
    {
        /*00*/100,100,100,100,100,
        /*05*/100,100,100,100,100,
        /*10*/100,100,100,100,100,
        /*15*/100,100,100,100,
        /*19*/100,100,100,100,
        /*23*/130,100,100,100,
        /*27*/100,100,100,090,
        /*31*/100,100,100,100,
        /*35*/100,076,100,100,
        /*39*/120,100,100,100
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] INDICATE_OFFLINE_MODE_MESSAGE =
    {
        /*00*/"Disconnected the internet","Disconnected the internet","Disconnected the internet","Disconnected the internet","Disconnected the internet",
        /*05*/"Disconnected the internet","Disconnected the internet","Disconnected the internet","Disconnected the internet","Disconnected the internet",
        /*10*/"Disconnected the internet","Disconnected the internet","Disconnected the internet","Disconnected the internet","Disconnected the internet",
        /*15*/"Disconnected the internet","Disconnected the internet","Disconnected the internet","Disconnected the internet",
        /*19*/"Disconnected the internet","Disconnected the internet","Disconnected the internet","Disconnected the internet",
        /*23*/"인터넷에 연결되어 있지 않습니다","Disconnected the internet","Disconnected the internet","Disconnected the internet",
        /*27*/"Disconnected the internet","Disconnected the internet","Disconnected the internet","Вы не подключены к Интернету",
        /*31*/"Disconnected the internet","Disconnected the internet","Disconnected the internet","Usted no está conectado a Internet",
        /*35*/"Disconnected the internet","คุณไม่ได้เชื่อมต่อกับอินเทอร์เน็ต","Disconnected the internet","Disconnected the internet",
        /*39*/"Bạn không được kết nối với Internet","Disconnected the internet","Disconnected the internet","Disconnected the internet"
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] DATABASE_CONNECTION_ERROR_MESSAGE =
    {
        /*00*/"Checking Server","Checking Server","Checking Server","Checking Server","Checking Server",
        /*05*/"Checking Server","Checking Server","Checking Server","Checking Server","Checking Server",
        /*10*/"Checking Server","Checking Server","Checking Server","Checking Server","Checking Server",
        /*15*/"Checking Server","Checking Server","Checking Server","Checking Server",
        /*19*/"Checking Server","Checking Server","Checking Server","Checking Server",
        /*23*/"서버 문제로 로그인을 할 수 없습니다","Checking Server","Checking Server","Checking Server",
        /*27*/"Checking Server","Checking Server","Checking Server","Проверка сервера",
        /*31*/"Checking Server","Checking Server","Checking Server","Comprobar servidor",
        /*35*/"Checking Server","กำลังตรวจสอบเซิร์ฟเวอร์","Checking Server","Checking Server",
        /*39*/"Kiểm tra máy chủ","Checking Server","Checking Server","Checking Server"
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] LOGIN_SUCCESS_MESSAGE =
    {
        /*00*/"Login Succeed","Login Succeed","Login Succeed","Login Succeed","Login Succeed",
        /*05*/"Login Succeed","Login Succeed","Login Succeed","Login Succeed","Login Succeed",
        /*10*/"Login Succeed","Login Succeed","Login Succeed","Login Succeed","Login Succeed",
        /*15*/"Login Succeed","Login Succeed","Login Succeed","Login Succeed",
        /*19*/"Login Succeed","Login Succeed","Login Succeed","Login Succeed",
        /*23*/"로그인 성공","Login Succeed","Login Succeed","Login Succeed",
        /*27*/"Login Succeed","Login Succeed","Login Succeed","Успешный вход в систему",
        /*31*/"Login Succeed","Login Succeed","Login Succeed","Inicia sesión con éxito",
        /*35*/"Login Succeed","เข้าสู่ระบบสำเร็จ","Login Succeed","Login Succeed",
        /*39*/"Đăng nhập thành công","Login Succeed","Login Succeed","Login Succeed"
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] LOGIN_FAIL_MESSAGE =
    {
        /*00*/"Login Failed","Login Failed","Login Failed","Login Failed","Login Failed",
        /*05*/"Login Failed","Login Failed","Login Failed","Login Failed","Login Failed",
        /*10*/"Login Failed","Login Failed","Login Failed","Login Failed","Login Failed",
        /*15*/"Login Failed","Login Failed","Login Failed","Login Failed",
        /*19*/"Login Failed","Login Failed","Login Failed","Login Failed",
        /*23*/"로그인 실패","Login Failed","Login Failed","Login Failed",
        /*27*/"Login Failed","Login Failed","Login Failed","Ошибка входа",
        /*31*/"Login Failed","Login Failed","Login Failed","error de inicio de sesion",
        /*35*/"Login Failed","การเข้าสู่ระบบล้มเหลว","Login Failed","Login Failed",
        /*39*/"Đăng nhập thất bại","Login Failed","Login Failed","Login Failed"
    };
    // 한국어(23), 영어(10), 러시아어(30), 베트남어(39), 태국어(36), 스페인어(34)
    private static string[] PROCEED_TO_THE_PROLOGUE_MESSAGE =
    {
        /*00*/"Proceed to the prologue","Proceed to the prologue","Proceed to the prologue","Proceed to the prologue","Proceed to the prologue",
        /*05*/"Proceed to the prologue","Proceed to the prologue","Proceed to the prologue","Proceed to the prologue","Proceed to the prologue",
        /*10*/"Proceed to the prologue","Proceed to the prologue","Proceed to the prologue","Proceed to the prologue","Proceed to the prologue",
        /*15*/"Proceed to the prologue","Proceed to the prologue","Proceed to the prologue","Proceed to the prologue",
        /*19*/"Proceed to the prologue","Proceed to the prologue","Proceed to the prologue","Proceed to the prologue",
        /*23*/"프롤로그로 진행 합니다","Proceed to the prologue","Proceed to the prologue","Proceed to the prologue",
        /*27*/"Proceed to the prologue","Proceed to the prologue","Proceed to the prologue","Ошибка входа",
        /*31*/"Proceed to the prologue","Proceed to the prologue","Proceed to the prologue","error de inicio de sesion",
        /*35*/"Proceed to the prologue","การเข้าสู่ระบบล้มเหลว","Proceed to the prologue","Proceed to the prologue",
        /*39*/"Đăng nhập thất bại","Proceed to the prologue","Proceed to the prologue","Proceed to the prologue"
    };

    public static string GetMainMenuTitle()
    {
        return MAIN_MENU_TITLE[(int)Application.systemLanguage];
    }
    public static string GetMainMenuTitleFont()
    {
        return MAIN_MENU_TITLE_FONT[(int)Application.systemLanguage];
    }
    public static int GetMainMenuTitleFontSize()
    {
        return MAIN_MENU_TITLE_FONTSIZE[(int)Application.systemLanguage];
    }
    public static string GetIndicateOfflineModeMessage()
    {
        return INDICATE_OFFLINE_MODE_MESSAGE[(int)Application.systemLanguage];
    }

    public static string GetDatabaseConnectionErrorMessage()
    {
        return DATABASE_CONNECTION_ERROR_MESSAGE[(int)Application.systemLanguage];
    }

    public static string GetLoginSucceedMessage()
    {
        return LOGIN_SUCCESS_MESSAGE[(int)Application.systemLanguage];
    }

    public static string GetLoginFailedMessage()
    {
        return LOGIN_FAIL_MESSAGE[(int)Application.systemLanguage];
    }

    public static string GetProceedToPrologueMessage()
    {
        return PROCEED_TO_THE_PROLOGUE_MESSAGE[(int)Application.systemLanguage];
    }
}


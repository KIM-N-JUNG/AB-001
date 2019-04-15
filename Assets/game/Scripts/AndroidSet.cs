using System;
using UnityEngine;

public class AndroidSet : MonoBehaviour
{
    private AndroidJavaObject UnityActivity;
    private AndroidJavaObject UnityInstance;

    private void Start()
    {
        try
        {
            AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            UnityActivity = ajc.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaClass ajc2 = new AndroidJavaClass("toastplugin.kimnjung.com.unitytoast.AndroidToast");
            UnityInstance = ajc2.CallStatic<AndroidJavaObject>("instance");
            UnityInstance.Call("setContext", UnityActivity);
        }
        catch (Exception e)
        {
            Debug.Log("Error!");
            Debug.Log(e);
        }
    }

    public void ToastButton()
    {
        ShowToast("유니티에서도 호출됨", false);
    }
    public void ShowToast(string msg, bool isLong)
    {
        if (UnityActivity == null)
        {
            Debug.Log("UnityActivity is null!");
            return;
        }
        UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => 
        {
            UnityInstance.Call("ShowToast", msg, isLong ? 1 : 0);
        }));
    }
}

//public class AndroidSet : MonoBehaviour
//{

//#if UNITY_ANDROID

//    static public AndroidToast instance;

//    AndroidJavaObject currentActivity;
//    AndroidJavaClass UnityPlayer;
//    AndroidJavaObject context;
//    AndroidJavaObject toast;


//    void Awake()
//    {
//        if (instance == null) instance = this;
//        else Destroy(gameObject);

//        UnityPlayer =
//            new AndroidJavaClass("com.unity3d.player.UnityPlayer");

//        currentActivity = UnityPlayer
//            .GetStatic<AndroidJavaObject>("currentActivity");


//        context = currentActivity
//            .Call<AndroidJavaObject>("getApplicationContext");

//        DontDestroyOnLoad(this.gameObject);
//    }

//    void ShowToast(string message)
//    {
//        currentActivity.Call
//        (
//            "runOnUiThread",
//            new AndroidJavaRunnable(() =>
//            {
//                AndroidJavaClass Toast
//                = new AndroidJavaClass("android.widget.Toast");

//                AndroidJavaObject javaString
//                = new AndroidJavaObject("java.lang.String", message);

//                toast = Toast.CallStatic<AndroidJavaObject>
//                (
//                    "makeText",
//                    context,
//                    javaString,
//                    Toast.GetStatic<int>("LENGTH_SHORT")
//                );

//                toast.Call("show");
//            })
//         );
//    }

//    public void CancelToast()
//    {
//        currentActivity.Call("runOnUiThread",
//            new AndroidJavaRunnable(() =>
//            {
//                if (toast != null) toast.Call("cancel");
//            }));
//    }


//#else
//    void Awake()
//    {
//        Destroy(gameObject);
//    }
//#endif

//}
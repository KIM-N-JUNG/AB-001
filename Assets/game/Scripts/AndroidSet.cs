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
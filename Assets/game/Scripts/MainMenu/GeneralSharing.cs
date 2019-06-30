using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSharing : MonoBehaviour
{
    private const string subject = "[게임 공유] 운석을 피해라";
    private const string title = "컨트롤에 자신 있는 최고의 조종사를 찾습니다.";
    private const string body = "https://play.google.com/store/apps/details?id=com.kimnjung.avoidmeteor&showAllReviews=true";

    public void ShareGame()
    {
        Debug.Log("ShareGame");
        // StartCoroutine(ShareAndroid());
        ShareAndroid();
    }

    public void ShareAndroid()
    {
        Debug.Log("ShareAndroid");
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent")) 
        using (AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent")) {
        Debug.Log("ShareAndroid 1");
            intentObject.Call<AndroidJavaObject>("setAction", intentObject.GetStatic<string>("ACTION_SEND"));
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_SUBJECT"), subject);
    		intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_TITLE"), title);
            intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_TEXT"), body);
            Debug.Log("ShareAndroid 2");
            using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity")) {
                    using (AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via")) {
                        Debug.Log("ShareAndroid 3");
                        currentActivity.Call("startActivity", jChooser);
                    }
                }
            }
        }
#endif
        Debug.Log("ShareAndroid 4");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VersionCheckManager : MonoBehaviour
{
    public AndroidSet androidSet;
    
    private static VersionCheckManager instance;
    public static VersionCheckManager GetInstance()
    {
        if (!instance)
        {
            instance = GameObject.FindObjectOfType<VersionCheckManager>();
            if (!instance)
                Debug.Log("There needs to be one active VersionCheckManager script on a VersionCheckManager in your scene.");
        }
        return instance;
    }


    public Transform _transformPopup;
    public GameObject _popupVersionCheckerPrefab;        //팝업 : 버전체크 업데이트 프리팹
    public string url;              //데이터를 가져올 URL -> https://play.google.com/store/apps/details?id=com.kimnjung.avoidmeteor
    public Text _textVersion;        //버전을 표시할 텍스트

    //유니티 자체에서 bundleIdentifier를 읽을수도 있지만, 이렇게 읽을 수 도 있다.
    // public string _bundleIdentifier { get { return url.Substring(url.IndexOf("details"), url.LastIndexOf("details") + 1); } }


    [HideInInspector]
    public bool isSamePlayStoreVersion = false;

    public bool isTestMode = false;        //테스트 모드 여부


    private void Start()
    {
        if (isTestMode == false)
            StartCoroutine(PlayStoreVersionCheck());
        else
            isSamePlayStoreVersion = true;

        Debug.Log("Application Version : " + Application.version);
        _textVersion.text = "Version : " + Application.version;
    }

    /// <summary>
    /// 버전체크를 하여, 강제업데이트를 체크한다.
    /// </summary>
    /// <returns></returns>
      private IEnumerator PlayStoreVersionCheck()
    {
        Debug.Log("PlayStoreVersionCheck");

        WWW www = new WWW(url);
        yield return www;

        //인터넷 연결 에러가 없다면, 
        if (www.error == null)
        {
            Debug.Log("www.text.IndexOf('softwareVersion') : " + www.text.IndexOf("softwareVersion"));
            Debug.Log("www.text.IndexOf('Current Version') : " + www.text.IndexOf("Current Version"));
            int index = www.text.IndexOf("Current Version");

            Debug.Log("www.text.Substring(index, 100) : " + www.text.Substring(index, 100));
            string versionText = www.text.Substring(index, 100);

            //플레이스토어에 올라간 APK의 버전을 가져온다.
            int endSpanPos = versionText.IndexOf("</span>");
            string playStoreVersion = versionText.Substring(endSpanPos - 5, 5);
            Debug.Log("Application.version : " + Application.version);
            Debug.Log("playStoreVersion : " + playStoreVersion);

            //버전이 같다면,
            if (playStoreVersion.Equals(Application.version))
            {
                Debug.Log("true : " + playStoreVersion + " : " + Application.version);
                //androidSet.ShowToast("최신 버전을 사용중입니다.", false);

                //버전이 같다면, 앱을 넘어가도록 한다.
                isSamePlayStoreVersion = true;
            } else {
                //버전이 다르므로, 마켓으로 보낸다.
                Debug.Log("false : " + playStoreVersion + " : " + Application.version);
                //androidSet.ShowToast("현재 최신 버전은 " + playStoreVersion + "입니다.", false);

                //업데이트 팝업을 연결한다.
                if (false) {
                    Instantiate(_popupVersionCheckerPrefab, _transformPopup, false);
                    Debug.Log("Instantiate 호출 됨");
                } else {
                    _popupVersionCheckerPrefab.SetActive(true);
                }
            }
        } else {
            //인터넷 연결 에러시
            Debug.Log(www.error);
            //PopupCreateMgr.GetInstance().Create_ConfirmPopup("인터넷 연결 안내", "인터넷 연결이 되지 않았습니요.\n인터넷 연결을 확인하니요.",
                                                             //"다시 연결하기", _transformPopup, "OnClick_ReConnectionVersionChecker");
        }
    }

    /// <summary>
    /// 업데이트 팝업에서 업데이트 여부를 체크한다.
    /// </summary>
    public void Call_PlayStoreVersionCheck()
    {
        StartCoroutine(PlayStoreVersionCheck());
    }

    public void UpdateGameVersion() {
        Debug.Log("UpdateGameVersion");

        Application.OpenURL("market://details?id=com.kimnjung.avoidmeteor"); 
    }
}
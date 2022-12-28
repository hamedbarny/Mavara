using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Text;

public class VerifyManager : MonoBehaviour
{
    [SerializeField]
    GameObject verify, signUp, signIn;

    [SerializeField]
    TMP_InputField usernameText, codeText;

    [SerializeField]
    TMP_Text logText;

    #region Verify Request

    public void UploadVerifyData()
    {
        VerifyJsonToSend VerifyjsonCreat = new VerifyJsonToSend();
        string username = usernameText.text, code = codeText.text;
        VerifyjsonCreat.email = username;
        VerifyjsonCreat.code = code;
        string json = JsonUtility.ToJson(VerifyjsonCreat);
        StartCoroutine(PostJSON("http://94.101.184.60:1001/api/User/verify", json));
    }
    IEnumerator PostJSON(string url, string bodyJsonString)
    {
        Debug.LogError(usernameText.text);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        var recievedMsg = JsonUtility.FromJson<VerifyJsonToRecieve>(request.downloadHandler.text);
        logText.SetText(recievedMsg.result);
        if (recievedMsg.success)
        {
            verify.SetActive(false);
            signIn.SetActive(true);
        }

    }

    public class VerifyJsonToSend
    {
        public string email;
        public string code;
    }
    public class VerifyJsonToRecieve
    {
        public bool success = false;
        public string result;
        public int state;
    }

    #endregion

    public void ButtonCancel()
    {
        verify.SetActive(false);
        signUp.SetActive(true);
    }
}

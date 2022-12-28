using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Text;

public class SignUpManager : MonoBehaviour
{
    [SerializeField]
    GameObject verify, signUp;

    [SerializeField]
    TMP_InputField nicknameText, usernameText, pwText;

    [SerializeField]
    TMP_Text logText;

    #region signUp Request
    public void UploadSignUpData()
    {
        JsonToSend jsonCreat = new JsonToSend();
        string nickname = nicknameText.text, username = usernameText.text, pw = pwText.text;
        jsonCreat.nickname = nickname;
        jsonCreat.email = username;
        jsonCreat.password = pw;
        string json = JsonUtility.ToJson(jsonCreat);
        StartCoroutine(PostJSON("http://94.101.184.60:1001/api/User/signUp", json));
    }
    IEnumerator PostJSON(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        var recievedMsg = JsonUtility.FromJson<JsonToRecieve>(request.downloadHandler.text);
        logText.SetText(recievedMsg.result);
        Debug.LogWarning(recievedMsg.result);
        if (recievedMsg.success)
        {
            verify.SetActive(true);
            signUp.SetActive(false);
        }

    }
    public class JsonToSend
    {
        public string nickname;
        public string email;
        public string password;
    }
    public class JsonToRecieve
    {
        public bool success = false;
        public string result;
        public string _id;
    }

    #endregion
}

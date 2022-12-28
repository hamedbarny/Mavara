using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class SignInManager : MonoBehaviour
{
    [SerializeField]
    GameObject signIn, signUp;

    [SerializeField]
    TMP_InputField usernameText, pwText;

    [SerializeField]
    TMP_Text logText;

    #region Login Request
    public void UploadLoginData()
    {
        JsonToSend jsonCreat = new JsonToSend();
        string username = usernameText.text, pw = pwText.text;
        jsonCreat.email = username;
        jsonCreat.password = pw;
        string json = JsonUtility.ToJson(jsonCreat);
        StartCoroutine(PostJSON("http://94.101.184.60:1001/api/User/signIn", json));
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
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }
    public class JsonToSend
    {
        public string email;
        public string password;
    }
    public class JsonToRecieve
    {
        public bool success = false;
        public string result;
        public string token;
        public string _id;
    }

    #endregion

    #region Sign in&up Tabs
    /// <summary>
    /// switch between login tab and sign up tab
    /// </summary>
    public void SignInPage()
    {
        signIn.SetActive(true);
        signUp.SetActive(false);
    }
    public void SignUpPage()
    {
        signIn.SetActive(false);
        signUp.SetActive(true);
    }
    #endregion
}

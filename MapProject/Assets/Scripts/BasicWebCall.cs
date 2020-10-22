using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BasicWebCall : MonoBehaviour {

    public void Start() {
        //Debug.Log("test1");
        StartCoroutine(Login());
    }

    private static IEnumerator Login() {
        Debug.Log("test2");
        string weburl = "https://www.google.com/webhp?hl=zh-TW&sa=X&ved=0ahUKEwjM1qr32sbsAhUL7J4KHfGpA3kQPAgI";
        UnityWebRequest www = UnityWebRequest.Post(
            $"{weburl}", 
            new List<IMultipartFormSection> {
                new MultipartFormDataSection("username", "chsu65"),
                new MultipartFormDataSection("password", "pass"),
            }
        );

        yield return www.SendWebRequest();
        
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
            Debug.Log("test3");
        }
        else {
            foreach (var s in www.GetResponseHeaders()) {
                Debug.Log("s=" + s);
                Debug.Log("test4");
            }
        }
    }
/*    public Text messageText;
    public InputField scoreToSend;

    readonly string getURL = "http://homecookedgames.com/tutorialScrips/UWR_Tut_Get.php";
    //readonly string postURL = "http://homecookedgames.com/tutorialScrips/UWR_Tut_Post.php";
    readonly string postURL = "";

    private void Start()
    {
        messageText.text = "Press buttons to interact with web server";
    }

    public void OnButtonGetScore()
    {
        messageText.text = "Downloading data...";
        StartCoroutine(SimpleGetRequest());
    }

    IEnumerator SimpleGetRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(getURL);

        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }

        else
        {
            messageText.text = www.downloadHandler.text;
        }
    }

    public void OnButtonSendScore()
    {
        if (scoreToSend.text == string.Empty)
        {
            messageText.text = "Error: No high score to send.\nEnter a value in the input field.";
        }
        else
        {
            messageText.text = "Sending data...";
            StartCoroutine(SimplePostRequest(scoreToSend.text));
        }
    }

    IEnumerator SimplePostRequest(string curScore)
    {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("curScoreKey", curScore));

        UnityWebRequest www = UnityWebRequest.Post(postURL, wwwForm);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }

        else
        {
            messageText.text = www.downloadHandler.text;
        }
    }
    */
}
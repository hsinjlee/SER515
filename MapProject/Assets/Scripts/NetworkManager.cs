using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.Networking;

 
public class NetworkManager: MonoBehaviour {
    void Start() {
        StartCoroutine(GetText());
    }

    [Obsolete]
    public IEnumerator GetText() {
        UnityWebRequest www = UnityWebRequest.Get("http://www.my-server.com");
        yield return www.Send();
 
        if(www.isError) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
 
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}

/*
public class NetworkManager : MonoBehaviour
{
    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest("https://www.example.com"));

        // A non-existing page.
        StartCoroutine(GetRequest("https://error.html"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}
*/


/*
public class NetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    //    StartCoroutine(GetText());
    }


    IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();
            callback(request);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPosts() {
        StartCoroutine(GetRequest("https://jsonplaceholder.typicode.com/posts", (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            } else
            {
                Debug.Log(req.downloadHandler.text);
            }
        }));
    }
}

*/

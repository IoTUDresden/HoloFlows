using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class POSTRequest : RESTRequest
{
    private string requestURL;
    private string requestBody;

    public POSTRequest(string requestURL, string requestBody)
    {
        this.requestURL = requestURL;
        this.requestBody = requestBody;
    }

    public string getStatus()
    {
        return "OK";
    }

    public IEnumerator performAction()
    {
        UnityWebRequest www;

        //Dirty hack because of Unity sucking hard (automatically does URL encoding of characters)
        if (requestURL.Contains("bulb210_color"))
        {
            www = UnityWebRequest.Put(requestURL, requestBody);
            www.SetRequestHeader("Content-Type", "text/plain");
            www.method = "POST";
        } else
        {
        www = UnityWebRequest.Post(requestURL, requestBody);
        www.SetRequestHeader("Content-Type", "text/plain");
        }

        yield return www.Send();

        while (!www.isDone)
        {
            Debug.LogError(".");
            yield return null;
        }

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("request sent: "+requestURL +" "+ requestBody);
        }
    }
}


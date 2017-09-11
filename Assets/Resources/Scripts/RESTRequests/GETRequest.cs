using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
#if WINDOWS_UWP
using System.Runtime.Serialization.Json;
#endif

public class GETRequest : RESTRequest
{
    private string requestURL;
    private string currentStatus;

    public GETRequest(string requestURL)
    {
        this.requestURL = requestURL;
        this.currentStatus = "establishing connection...";
    }

    public string getStatus()
    {
        return currentStatus;
    }

    public IEnumerator performAction()
    {
        UnityWebRequest www;

        www = UnityWebRequest.Get(requestURL);

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
            // Show results as text
            //Debug.Log(www.downloadHandler.text);
            string temp = www.downloadHandler.text;
#if WINDOWS_UWP
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GETResponse));

            MemoryStream ms = new MemoryStream();
            StreamWriter writer = new StreamWriter(ms);
            writer.Write(temp);
            writer.Flush();
            ms.Position = 0;
                //get status from json answer
            GETResponse r = (GETResponse)ser.ReadObject(ms);
            currentStatus = r.state;
#endif
        }
    }
}

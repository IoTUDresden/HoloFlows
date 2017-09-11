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

public class MyoGetStatusRequest
{
    private string requestURL;
    private MyoResponseFormat getResponse;

    public MyoGetStatusRequest(string requestURL)
    {
        this.requestURL = requestURL;
        this.getResponse = new MyoResponseFormat();
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
            string temp = www.downloadHandler.text;
#if WINDOWS_UWP
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MyoResponseFormat));
                
                MemoryStream ms = new MemoryStream();
                StreamWriter writer = new StreamWriter(ms);
                writer.Write(temp);
                writer.Flush();
                ms.Position = 0;
                //get status from json answer
                MyoResponseFormat r = (MyoResponseFormat)ser.ReadObject(ms);
                getResponse = r;
#endif

            ItemManager.getInstance().updateItem(Settings.myoArmband, Settings.myoArmband, getResponse.pose, Settings.myoArmband);
        }
    }
}

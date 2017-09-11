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

public class CoffeemachineGETStatusRequest
{
    private string requestURL;
    private CoffeeMachineResponseFormat getResponse;

    public CoffeemachineGETStatusRequest(string requestURL)
    {
        this.requestURL = requestURL;
        this.getResponse = new CoffeeMachineResponseFormat();
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
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(CoffeeMachineResponseFormat));
                
                MemoryStream ms = new MemoryStream();
                StreamWriter writer = new StreamWriter(ms);
                writer.Write(temp);
                writer.Flush();
                ms.Position = 0;
                //get status from json answer
                CoffeeMachineResponseFormat r = (CoffeeMachineResponseFormat)ser.ReadObject(ms);
                getResponse = r;
#endif

            String status="";

            if (getResponse.status.ready)
            {
                status = "Waiting";
            } else
            {
                status = "Brewing";
            }

            string waterLevel = "";

            switch (getResponse.sensors.waterlevel)
            {
                case 0 :
                    waterLevel = "empty";
                    break;
                case 1:
                    waterLevel = "low";
                    break;
                case 2:
                    waterLevel = "half-full";
                    break;
                case 3:
                    waterLevel = "full";
                    break;
                default: waterLevel = getResponse.sensors.waterlevel.ToString();
                    break;

            }

            ItemManager.getInstance().updateItem(Settings.CoffeeMachine, Settings.CoffeeMachine, status, getResponse.appliance.model);
            CoffeeMachineActuator cma = (CoffeeMachineActuator)ItemManager.getInstance().getItem(Settings.CoffeeMachine);
            cma.setWaterLevel(waterLevel);
        }
    }
}

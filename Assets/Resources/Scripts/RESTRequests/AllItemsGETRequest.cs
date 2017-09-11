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

public class AllItemsGETRequest
{
    private string requestURL;
    private List<GETResponse> getResponses;

    public AllItemsGETRequest(string requestURL)
    {
        this.requestURL = requestURL;
        this.getResponses = new List<GETResponse>();
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
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<GETResponse>));

            MemoryStream ms = new MemoryStream();
            StreamWriter writer = new StreamWriter(ms);
            writer.Write(temp);
            writer.Flush();
            ms.Position = 0;
                //get status from json answer
            getResponses = (List<GETResponse>)ser.ReadObject(ms);
#endif
            foreach(string fullItemName in Settings.Items)
            {
                GETResponse singleItemGetResponse = getResponses.Find(x => x.name.Equals(fullItemName));
                if(singleItemGetResponse != null)
                {

                    //gather all info needed to update or create the item in the ItemManager
                    //we need key, name, state, shortName, unit (optionally). We already have key, name, state.
                    //first read in the unit, if existent
                    string unit = "";

                    if (singleItemGetResponse.stateDescription != null && singleItemGetResponse.stateDescription.pattern != null)
                    {
                        string[] stateDesc = singleItemGetResponse.stateDescription.pattern.Split(' ');
                        if (stateDesc.Length > 1)
                        {
                            unit = stateDesc[1];
                            if (unit.EndsWith("%"))
                                unit = unit.Remove(unit.Length - 1);
                        }
                    }

                    //next, the shortName
                    string shortName;
                    switch(fullItemName)
                    {
                        case Settings.LinearPoti:
                            shortName = Settings.LinearPoti;
                            break;
                        case Settings.NFCReader:
                            shortName = Settings.NFCReader;
                            break;
                        case Settings.CoffeeMachine:
                            shortName = Settings.CoffeeMachine;
                            break;
                        case Settings.myoArmband:
                            shortName = Settings.myoArmband;
                            break;
                        case Settings.hueLamp1dimmer:
                            shortName = "huedimmer_1";
                            break;
                        case Settings.hueLamp2dimmer:
                            shortName = "huedimmer_2";
                            break;
                        default:
                            string[] tempName = fullItemName.Split('_');
                            shortName = tempName[tempName.Length - 2] + "" + tempName[tempName.Length - 1];
                            break;
                    }

                    //now we have all we need. Update the list!

                    //if the current item is the dimmer, update both sensor and actuator reference
                    if (fullItemName.Contains("homematic_dimmer"))
                    {
                        ItemManager.getInstance().updateItem(fullItemName + "_Sensor", fullItemName, singleItemGetResponse.state, shortName, unit);
                        ItemManager.getInstance().updateItem(fullItemName + "_Actuator", fullItemName, singleItemGetResponse.state, shortName, unit);
                    } else
                        if (fullItemName.Contains("bulb210_color"))
                        {
                            ItemManager.getInstance().updateItem(fullItemName, fullItemName, singleItemGetResponse.state, shortName, unit);
                            HueColor i = (HueColor)ItemManager.getInstance().getItem(fullItemName);
                            string[] colors = singleItemGetResponse.state.Split(',');
                            int.TryParse(colors[0], out i.h);
                            int.TryParse(colors[1], out i.s);
                            int.TryParse(colors[2], out i.v);
                        } else

                            if (fullItemName.Equals(Settings.colorSensor))
                            {
                                ItemManager.getInstance().updateItem(fullItemName, fullItemName, singleItemGetResponse.state, shortName, unit);
                                ColorSensor c = (ColorSensor)ItemManager.getInstance().getItem(fullItemName);
                                string[] colors = singleItemGetResponse.state.Split(',');
                                int.TryParse(colors[0], out c.r);
                                int.TryParse(colors[1], out c.g);
                                int.TryParse(colors[2], out c.b);
                            }
                    else
                        ItemManager.getInstance().updateItem(fullItemName, fullItemName, singleItemGetResponse.state, shortName, unit);
                }
                       
                    }
                    //default case
                    
                
            
        }
    }
}

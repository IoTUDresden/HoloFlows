using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemInfoUpdater: MonoBehaviour
{

    private int updateCounter;

    // Use this for initialization
    void Start()
    {
        updateCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateCounter++;
        if (updateCounter >= Settings.statusUpdateNumber)
        {
            string status = "invalid";

            string objectName;
            string[] nameSubstrings;

            objectName = gameObject.transform.parent.name;

            nameSubstrings = objectName.Split('_');
            
            int pos = objectName.IndexOf('_');
            string cleanedName = objectName.Substring(pos + 1);

            Item i = ItemManager.getInstance().getItem(cleanedName);
            if (i != null)
            {
                status = i.state;
                if (i.type.Equals("Sensor"))
                {
                    status += " " + ((Sensor)i).unit;

                    if (cleanedName.Contains("idscan"))
                        status = ((NfcReader)i).getName();
                }
                updateCounter = 0;
                this.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = i.shortName;
                //if(objectName.Contains("dimmer"))
                //    Debug.Log("shortname of " + objectName + " is " + i.shortName + " with status " + status);
            }
            if (i is ColorSensor || i is HueDimmer || i is CoffeeMachineActuator)
            {
                if (i is ColorSensor || i is HueDimmer)
                {
                    byte r, g, b;

                    if (i is ColorSensor)
                    {
                        ColorSensor c = (ColorSensor)i;
                        r = Convert.ToByte(c.r);
                        g = Convert.ToByte(c.g);
                        b = Convert.ToByte(c.b);
                        this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(r, g, b, 255);
                    }
                    else
                    {
                        HueDimmer hd = (HueDimmer)i;
                        HueColor c = hd.hueColor;
                        
                        float hf = (float)c.h / 360;
                        float sf = (float)c.s / 100;
                        float vf = (float)c.v / 100;
                        
                        Color rgbColor = Color.HSVToRGB(hf, sf, vf, false);
                        this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = rgbColor;

                        string RGBstring = rgbColor.ToString();
                       
                        int Ri = (int)(rgbColor.r * 255);
                        int Bi = (int)(rgbColor.b * 255);
                        int Gi = (int)(rgbColor.g * 255);
                        
                        string RGBstr = Ri.ToString()+ "," + Gi.ToString() + "," + Bi.ToString();
                        Debug.Log(RGBstr);
                        this.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = RGBstr;
                    }
                                        
                    this.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = status;
                }
                else
                {
                    CoffeeMachineActuator c = (CoffeeMachineActuator)i;
                    this.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = c.getWaterLevel();
                    this.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = status;
                }                
            }
            else
            this.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = status;
        }
    }
}

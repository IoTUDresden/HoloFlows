// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// mit kreativen Erweiterungen von MGohlke :D
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HoloToolkit.Unity.InputModule.Tests
{
    /// <summary>
    /// Test behaviour that simply prints out a message very time a supported event is received from the input module.
    /// This is used to make sure that the input module routes events appropriately to game objects.
    /// </summary>
    public class ToggleSenActInDecrease : MonoBehaviour, IInputClickHandler, IFocusable

    {
        private bool isFocused;
        private string ButtonName;

        private string SenActFensterName;
        private GameObject SenActFenster;

        [Tooltip("Set to true if gestures update (ManipulationUpdated, NavigationUpdated) should be logged. Note that this can impact performance.")]
        [HideInInspector]
        public bool LogGesturesUpdateEvents = false;

        private string StartWertText;
        private float StartWertFloat;
        private float Summand;
        [HideInInspector]
        public string SourceName;
        private float AktuellerWert;

        private int DictIndexmax;
        private int StartIndex;

        public string UbergabeString;

        private SpriteRenderer BtnSpriteRenderer;
        private Style style;

        public void OnInputUp(InputEventData eventData)
        {
            Debug.LogFormat("OnInputUp\r\nSource: {0}  SourceId: {1}", eventData.InputSource, eventData.SourceId);
        }

        public void OnInputDown(InputEventData eventData)
        {
            Debug.LogFormat("OnInputDown\r\nSource: {0}  SourceId: {1}", eventData.InputSource, eventData.SourceId);
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            SenActFensterName = this.gameObject.transform.parent.parent.parent.name;
            SenActFenster = this.gameObject.transform.parent.parent.parent.gameObject;
            ButtonName = this.name;

            StartWertFloat = SenActFenster.transform.GetComponent<LogicSenActInput>().GetSenValue();
            SourceName = SenActFenster.transform.GetComponent<LogicSenActInput>().SourceName;

            Summand = ((Sensor)ItemManager.getInstance().getItem(SourceName)).interval;

            Debug.Log("Mein Sourcename ist " + SourceName);
                       
            if (ButtonName == "Button_Down")
            {
                if (!SourceName.Equals(Settings.NFCReader)&&!SourceName.Equals(Settings.myoArmband))
                {
                    AktuellerWert = StartWertFloat - Summand;
                    SenActFenster.transform.GetComponent<LogicSenActInput>().SetSenValue(AktuellerWert);
                }
                if (SourceName.Equals(Settings.NFCReader))
                {
                    //SenActFenster.transform.GetComponent<LogicSenActInput>().SetSenValue(AktuellerWert);
                    NfcReader nfc = (NfcReader)ItemManager.getInstance().getItem(SourceName);
                    DictIndexmax = nfc.getDict().Count;
                    Debug.Log("DictIndexmax= " + DictIndexmax + " - Das Dictionary enthält: "+ DictIndexmax + " Objekte.");
                    StartIndex = SenActFenster.transform.GetComponent<LogicSenActInput>().GetDictIndex();
                    Debug.Log("StartIndex= " + StartIndex);
                    if (StartIndex == 0)
                    {
                        StartIndex = DictIndexmax-1;
                        Debug.Log("neuerIndex ist: " + StartIndex);
                    }   
                    else
                    {
                        StartIndex = StartIndex - 1;
                        Debug.Log("neuerIndex ist: " + StartIndex);
                    }
                    Debug.Log("Index ist immernoch: " + StartIndex);
                    SenActFenster.transform.GetComponent<LogicSenActInput>().SetDictIndex(StartIndex);
                    Debug.Log("jetzt wurde der index mit Set übergeben");
                } 
                else
                {
                    if (SourceName.Equals(Settings.myoArmband))
                    {
                        MyoArmband myo = (MyoArmband)ItemManager.getInstance().getItem(SourceName);
                        DictIndexmax = myo.getDict().Count;
                        Debug.Log("DictIndexmax= " + DictIndexmax + " - Das Dictionary enthält: " + DictIndexmax + " Objekte.");
                        StartIndex = SenActFenster.transform.GetComponent<LogicSenActInput>().GetDictIndex();
                        Debug.Log("StartIndex= " + StartIndex);
                        if (StartIndex == 0)
                        {
                            StartIndex = DictIndexmax - 1;
                            Debug.Log("neuerIndex ist: " + StartIndex);
                        }
                        else
                        {
                            StartIndex = StartIndex - 1;
                            Debug.Log("neuerIndex ist: " + StartIndex);
                        }
                        Debug.Log("Index ist immernoch: " + StartIndex);
                        SenActFenster.transform.GetComponent<LogicSenActInput>().SetDictIndex(StartIndex);
                        Debug.Log("jetzt wurde der index mit Set übergeben");
                    }
                    Debug.Log("ButtonDown gedrückt aber was soll getan werden?");
                }
            }

            if (ButtonName == "Button_Up")
            {      
                if (!SourceName.Equals(Settings.NFCReader)&&!SourceName.Equals(Settings.myoArmband))
                {
                    AktuellerWert = StartWertFloat + Summand;
                    SenActFenster.transform.GetComponent<LogicSenActInput>().SetSenValue(AktuellerWert);
                }
                if (SourceName.Equals(Settings.NFCReader))
                {
                    //SenActFenster.transform.GetComponent<LogicSenActInput>().SetSenValue(AktuellerWert);
                    NfcReader nfc = (NfcReader)ItemManager.getInstance().getItem(SourceName);
                    DictIndexmax = nfc.getDict().Count;
                    Debug.Log("DictIndexmax= " + DictIndexmax + " - Das Dictionary enthält: " + DictIndexmax + " Objekte.");
                    StartIndex = SenActFenster.transform.GetComponent<LogicSenActInput>().GetDictIndex();
                    Debug.Log("StartIndex= " + StartIndex);

                    if (StartIndex == DictIndexmax -1)
                    {
                        StartIndex = 0;
                        Debug.Log("neuerIndex ist: " + StartIndex);
                    }
                    else
                    {
                        StartIndex = StartIndex +1 ;
                        Debug.Log("neuerIndex ist: " + StartIndex);
                    }
                    Debug.Log("Index ist immernoch: " + StartIndex);
                    SenActFenster.transform.GetComponent<LogicSenActInput>().SetDictIndex(StartIndex);
                    Debug.Log("jetzt wurde der index mit Set übergeben");
                }
                 else
                {
                    if(SourceName.Equals(Settings.myoArmband)) {
                        MyoArmband myo = (MyoArmband)ItemManager.getInstance().getItem(SourceName);
                        DictIndexmax = myo.getDict().Count;
                        Debug.Log("DictIndexmax= " + DictIndexmax + " - Das Dictionary enthält: " + DictIndexmax + " Objekte.");
                        StartIndex = SenActFenster.transform.GetComponent<LogicSenActInput>().GetDictIndex();
                        Debug.Log("StartIndex= " + StartIndex);

                        if (StartIndex == DictIndexmax - 1)
                        {
                            StartIndex = 0;
                            Debug.Log("neuerIndex ist: " + StartIndex);
                        }
                        else
                        {
                            StartIndex = StartIndex + 1;
                            Debug.Log("neuerIndex ist: " + StartIndex);
                        }
                        Debug.Log("Index ist immernoch: " + StartIndex);
                        SenActFenster.transform.GetComponent<LogicSenActInput>().SetDictIndex(StartIndex);
                        Debug.Log("jetzt wurde der index mit Set übergeben");
                    }
                    Debug.Log("ButtonDown gedrückt aber was soll getan werden?");
                }

            }
            Debug.Log("Mein SourceName ist: " + SourceName);
            if (SourceName.Equals(Settings.NFCReader))
            {
                Debug.Log("Mein SourceName ist: " + SourceName + " - und dies ist die NFC-Ausgabe");
                NfcReader nfc = (NfcReader)ItemManager.getInstance().getItem(SourceName);                
                //UbergabeString = (string)(nfc.getDict()[StartIndex]); // würde auch gehen
                UbergabeString = nfc.getNamebyIndex(StartIndex);
                Debug.Log("(fall NFCreader)mein ÜbergabeString ist: " + UbergabeString);
            }
            else
            {
                if (SourceName.Equals(Settings.myoArmband))
                {
                    Debug.Log("Mein SourceName ist: " + SourceName + " - und dies ist die Myo-Ausgabe");
                    MyoArmband myo = (MyoArmband)ItemManager.getInstance().getItem(SourceName);
                    //UbergabeString = (string)(nfc.getDict()[StartIndex]); // würde auch gehen
                    UbergabeString = myo.getNamebyIndex(StartIndex);
                    Debug.Log("(fall Myo)mein ÜbergabeString ist: " + UbergabeString);
                } else
                {

                Debug.Log("Mein SourceName ist: " + SourceName + "und ich bin kein NFC-Leser");
                UbergabeString = AktuellerWert.ToString();
                }

            }                
            SenActFenster.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Text>().text = UbergabeString;
            SenActFenster.transform.GetComponent<LogicSenActInput>().SetSenValueString(UbergabeString);

        }


        public void OnFocusEnter()
        {
            //Debug.Log("OnFocusEnter");
            isFocused = true;
        }

        public void OnFocusExit()
        {
            //Debug.Log("OnFocusExit");
            isFocused = false;

        }


    }
}
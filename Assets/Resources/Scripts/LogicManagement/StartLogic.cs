// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// mit kreativen Erweiterungen von MGohlke :D
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace HoloToolkit.Unity.InputModule.Tests
{
    /// <summary>
    /// Test behaviour that simply prints out a message very time a supported event is received from the input module.
    /// This is used to make sure that the input module routes events appropriately to game objects.
    /// </summary>
    public class StartLogic : MonoBehaviour, IInputClickHandler, IFocusable
        
    {
        [HideInInspector]
        public string SourceName;
        [HideInInspector]
        public string SourceType;
        [HideInInspector]
        public string SourceNameShort;

        [HideInInspector]
        public string TargetName;
        [HideInInspector]
        public string TargetType;
        [HideInInspector]
        public string TargetNameShort;

        [HideInInspector]
        public string LineID;
        private GameObject Line;

        [HideInInspector]
        public Transform LogicSenActPrefabb;
        private Transform LogicSenActInitializer;

        [HideInInspector]
        public Transform LogicActActPrefabb;
        private Transform LogicActActInitializer;

        private Transform AktuellesFenster;
        private GameObject AktuellesFensterGO;
        [HideInInspector]
        public Vector3 FensterPoss;

        public Connection c;

        private int updateCounter;

        private int colorUpdateCounter;

        [HideInInspector]
        public string EingabeFensterName;

        //// properties for highlighting behaviour
        //private Style style;
        //private MeshRenderer cubeMeshRenderer;
        //private Material cubeDefaultMat;
        private bool isFocused;

        private bool isSelected; // used while creating new connections

        [Tooltip("Set to true if gestures update (ManipulationUpdated, NavigationUpdated) should be logged. Note that this can impact performance.")]
        [HideInInspector]
        public bool LogGesturesUpdateEvents = false;

        void Start()
        {
            updateCounter = 0;
            colorUpdateCounter = 0;
            isSelected = false;
        }

        void Update()
        {
            //if (isSelected || isFocused)
            //{
            //    cubeMeshRenderer.material = style.highlightMat;
            //}
            //else
            //{
            //    cubeMeshRenderer.material = cubeDefaultMat;
            //}

            if (this.c!=null)
            {

                updateCounter++;
                colorUpdateCounter++;
                if (updateCounter >= Settings.requestUpdateNumber)
                {

                    if (c.connState.Equals(Connection.States.Active))
                    {
                        Item source = ItemManager.getInstance().getItem(SourceName);
                        Item target = ItemManager.getInstance().getItem(TargetName);
                        if (source is Poti && target is DimmerActuator)
                        {
                            Poti sPoti = (Poti)source;
                            DimmerActuator tDimmer = (DimmerActuator)target;

                            int stateDim = int.Parse(tDimmer.state);
                            int statePoti = int.Parse(sPoti.state);
                            if (statePoti == 0)
                            {
                                StartCoroutine(tDimmer.sendOFF());
                            }
                            else if (statePoti == 100)
                            {
                                StartCoroutine(tDimmer.sendON());
                            }
                            else if (stateDim <= (statePoti - 10))
                            {
                                StartCoroutine(tDimmer.sendINCREASE());
                            }
                            else if (stateDim >= (statePoti + 10))
                            {
                                StartCoroutine(tDimmer.sendDECREASE());
                            }

                        } else
                        {
                            if (source is Poti && target is HueDimmer)
                            {
                                Poti sPoti = (Poti)source;
                                HueDimmer hDimmer = (HueDimmer)target;
                                StartCoroutine(hDimmer.sendState(sPoti.state));
                            } else
                            {
                                if (source is ColorSensor && target is HueDimmer)
                                {
                                    if (colorUpdateCounter>=100)
                                    {
                                        byte r, g, b;
                                        ColorSensor cs = (ColorSensor)source;
                                        r = Convert.ToByte(cs.r);
                                        g = Convert.ToByte(cs.g);
                                        b = Convert.ToByte(cs.b);

                                        float h, s, v;

                                        Color32 colRgb = new Color32(r, g, b, 255);
                                        Color.RGBToHSV(colRgb, out h, out s, out v);
                                        HueDimmer hDimmer = (HueDimmer)target;

                                        int hr = (int)(h * 360);
                                        int sr = (int)(s * 100);
                                        int vr = (int)(v * 100);

                                        StartCoroutine(hDimmer.sendColor(hr,sr,vr));
                                        colorUpdateCounter = 0;
                                    }
                                }
                            }

                        }

                    }
                updateCounter = 0;

                }

            }

        }

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
            if (SourceName.Contains("dimmer")){
                if (SourceType.Equals("Sensor"))
                    SourceName = SourceName + "_Sensor";
                else
                    SourceName = SourceName + "_Actuator";
            }

            Item source = ItemManager.getInstance().getItem(SourceName);
            Item target = ItemManager.getInstance().getItem(TargetName);

            AktuellesFenster = this.gameObject.transform.parent;
            AktuellesFensterGO = this.gameObject.transform.parent.gameObject;
            Line = GameObject.Find(LineID);

            if ((source is Poti && target is DimmerActuator)||(source is Poti && target is HueDimmer)||(source is ColorSensor && target is HueDimmer))
            {
                this.transform.localScale = new Vector3(0, 0, 0);
                Debug.Log("ich versuche auf (0 0 0) zu skalieren: " +this.transform.name);
                AktuellesFensterGO.transform.GetChild(2).transform.localScale = new Vector3(0, 0, 0);
                Debug.Log("ich versuche auf (0 0 0) zu skalieren: " + AktuellesFensterGO.transform.GetChild(2).name);
                AktuellesFensterGO.transform.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                Debug.Log("ich versuche auf (0,1 0,1 0,1) zu skalieren: " + AktuellesFensterGO.transform.GetChild(3).name);
                AktuellesFensterGO.transform.GetChild(4).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                Debug.Log("ich versuche auf (0,1 0,1 0,1) zu skalieren: " + AktuellesFensterGO.transform.GetChild(4).name);

                if (!ConnectionManager.getInstance().getConnections().ContainsKey(LineID))
                {
                    if (source is Poti)
                    {
                        if (target is DimmerActuator)
                        {
                            this.c = ConnectionCreator.createPotiDimmerConnection(LineID, SourceName, TargetName);
                        } else
                        {
                            this.c = ConnectionCreator.createPotiHueConnection(LineID, SourceName, TargetName);
                        }
                    } else
                    {
                        this.c = ConnectionCreator.createColorHueConnection(LineID, SourceName, TargetName);
                    }
                    ConnectionManager.getInstance().startConnection(LineID);
                } else
                {
                    ConnectionManager.getInstance().startConnection(LineID);
                }

            }
            
            else
            {
                if (source is Sensor)
                {
                    Destroy(AktuellesFensterGO);
                    LogicSenActInitializer = Instantiate(LogicSenActPrefabb) as Transform;
                    LogicSenActInitializer.transform.position = FensterPoss;
                    LogicSenActInitializer.name = "Window_" + SourceName + TargetName;
                    EingabeFensterName = LogicSenActInitializer.name; // weiß nicht ob es sinnvoll ist das nochmal irgendwohin zu übergeben
                    LogicSenActInitializer.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = SourceNameShort;
                    LogicSenActInitializer.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = TargetNameShort;
                    LogicSenActInitializer.GetChild(0).GetChild(2).GetChild(2).GetComponent<Text>().text = source.state;
                    Debug.Log("source " + SourceName + " is of type " + SourceType);
                    Sensor Sens = (Sensor)source;
                    LogicSenActInitializer.GetChild(0).GetChild(2).GetChild(4).GetComponent<Text>().text = Sens.unit;

                    LogicSenActInitializer.GetComponent<LogicSenActInput>().SourceName = SourceName;
                    LogicSenActInitializer.GetComponent<LogicSenActInput>().TargetName= TargetName;
                    LogicSenActInitializer.GetComponent<LogicSenActInput>().LineID = LineID;

                    LogicSenActInitializer.GetChild(2).GetComponent<DeleteConnection>().LineID = LineID;

                    LogicSenActInitializer.GetChild(1).GetComponent<StopConnection>().LineID = LineID;
                    LogicSenActInitializer.GetChild(1).transform.localScale = new Vector3(0, 0, 0);

                }

                else
                {
                    Destroy(AktuellesFensterGO);
                    LogicActActInitializer = Instantiate(LogicActActPrefabb) as Transform;
                    LogicActActInitializer.transform.position = FensterPoss;
                    LogicActActInitializer.name = "Window_" + SourceName + TargetName;
                    EingabeFensterName = LogicActActInitializer.name; // weiß nicht ob es sinnvoll ist das nochmal irgendwohin zu übergeben
                    
                    LogicActActInitializer.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = SourceNameShort;
                    LogicActActInitializer.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text = TargetNameShort;

                    LogicActActInitializer.GetChild(3).GetComponent<StartActActConnection>().SourceNamelong = SourceName;
                    LogicActActInitializer.GetChild(3).GetComponent<StartActActConnection>().TargetNamelong = TargetName;
                    LogicActActInitializer.GetChild(3).GetComponent<StartActActConnection>().LineID = LineID;

                    LogicActActInitializer.GetChild(2).GetComponent<DeleteConnection>().LineID = LineID;

                    LogicActActInitializer.GetChild(1).GetComponent<StopConnection>().LineID = LineID;
                    LogicActActInitializer.GetChild(1).transform.localScale = new Vector3(0, 0, 0);


                }
            }

            isSelected = !isSelected;
        }


        public void OnFocusEnter()
        {
            //Debug.Log("OnFocusEnter");
            isFocused = true;

            //cubeMeshRenderer.material = style.highlightMat;
        }

        public void OnFocusExit()
        {
            //Debug.Log("OnFocusExit");
            isFocused = false;

            //if (!isSelected)
              //  cubeMeshRenderer.material = cubeDefaultMat;
        }


    }
}
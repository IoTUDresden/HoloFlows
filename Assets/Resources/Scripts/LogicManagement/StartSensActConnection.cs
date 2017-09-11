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
    public class StartSensActConnection : MonoBehaviour, IInputClickHandler, IFocusable

    {
        private bool isFocused;
        private string ButtonName;

        private string SenActFensterName;
        private GameObject SenActFenster;

        public string SourceNamelong; // ohne "Actuator_"  vorne
        [HideInInspector]
        public string TargetNamelong; // ohne "Actuator_"  vorne
        [HideInInspector]
        public string LineID;
        [HideInInspector]
        public float rightSide;
        [HideInInspector]
        public string rightSideString;
        private SensActConnection c;
        private NfcActConnection nc;
        private MyoActConnection mc;

        private int updateCounter;
        private LogicSenActInput.Operatoren condOperator;

        private SensActConnection.commands targetCommand;

        [Tooltip("Set to true if gestures update (ManipulationUpdated, NavigationUpdated) should be logged. Note that this can impact performance.")]
        [HideInInspector]
        public bool LogGesturesUpdateEvents = false;

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
            //Needs: SourceName, TargetName, ActuatorOperation, ConditionOperator, rightSide of Condition as String

            //SensActConnection.commands targetCommand;

            SenActFensterName = this.gameObject.transform.parent.name;
            SenActFenster = this.gameObject.transform.parent.gameObject;

            this.LineID = SenActFenster.GetComponent<LogicSenActInput>().LineID;
            this.SourceNamelong = SenActFenster.GetComponent<LogicSenActInput>().SourceName;
            this.TargetNamelong = SenActFenster.GetComponent<LogicSenActInput>().TargetName;
            this.condOperator = SenActFenster.GetComponent<LogicSenActInput>().GetOperator();

            bool targetOp = SenActFenster.GetComponent<LogicSenActInput>().GetTargetStatus();

            if (!targetOp)
                targetCommand = SensActConnection.commands.OFF;
            else
                targetCommand = SensActConnection.commands.ON;

            Item source = ItemManager.getInstance().getItem(SourceNamelong);
            Item target = ItemManager.getInstance().getItem(TargetNamelong);

            if (source is NfcReader || source is MyoArmband)
            {
                this.rightSideString = SenActFenster.GetComponent<LogicSenActInput>().GetSenValueString();
            } else

                this.rightSide = SenActFenster.GetComponent<LogicSenActInput>().GetSenValue();

            if (source is Sensor && target is Actuator)
            {

                if (!ConnectionManager.getInstance().getConnections().ContainsKey(LineID))
                {
                    Sensor sSens = (Sensor)source;
                    Actuator tDimmer = (Actuator)target;
                    if (source is NfcReader || source is MyoArmband)
                    {
                        if (source is NfcReader)
                        {
                            NfcReader nfcr = (NfcReader)source;
                            nc = ConnectionCreator.createNfcActConnection(LineID, SourceNamelong, TargetNamelong, rightSideString, condOperator, targetCommand);
                        } else
                        {
                            MyoArmband myoa = (MyoArmband)source;
                            mc = ConnectionCreator.createMyoActConnection(LineID, SourceNamelong, TargetNamelong, rightSideString, condOperator, targetCommand);
                        }
                    } else 
                        c = ConnectionCreator.createSensActConnection(LineID, SourceNamelong, TargetNamelong, rightSide, condOperator,targetCommand );
                    ConnectionManager.getInstance().startConnection(LineID);
                }
                else
                {
                    if (source is NfcReader || source is MyoArmband)
                    {
                        if (source is NfcReader)
                        {
                            nc = (NfcActConnection)ConnectionManager.getInstance().getConnection(LineID);
                        } else
                        {
                            mc = (MyoActConnection)ConnectionManager.getInstance().getConnection(LineID);
                        }
                    } else
                        c = (SensActConnection)ConnectionManager.getInstance().getConnection(LineID);
                    ConnectionManager.getInstance().startConnection(LineID);
                }
            }

            SenActFenster.transform.GetChild(1).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            this.transform.localScale = new Vector3(0, 0, 0);

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

        void Update()
        {
            if (c != null || nc != null || mc != null)
            {
                updateCounter++;

                if (updateCounter >= Settings.requestUpdateNumber)
                {
                    if (c != null)
                    {
                        if (c.connState.Equals(Connection.States.Active))
                        {

                            Item source = ItemManager.getInstance().getItem(SourceNamelong);
                            Actuator target = (Actuator)ItemManager.getInstance().getItem(TargetNamelong);

                            string leftSide = source.state;

                            float leftValue = float.Parse(leftSide);
                            float rightValue = rightSide;

                            switch (c.condOperator)
                            {
                                case LogicSenActInput.Operatoren.kleiner:
                                    {
                                        if (leftValue < rightValue)
                                        {
                                            if (c.targetCommand.Equals(SensActConnection.commands.ON))
                                            {
                                                StartCoroutine(target.sendON());
                                                ConnectionManager.getInstance().stopConnection(c.id);
                                                SenActFenster.transform.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                                SenActFenster.transform.GetChild(1).transform.localScale = new Vector3(0, 0, 0);

                                            }
                                            else
                                            {
                                                StartCoroutine(target.sendOFF());
                                                ConnectionManager.getInstance().stopConnection(c.id);
                                                SenActFenster.transform.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                                SenActFenster.transform.GetChild(1).transform.localScale = new Vector3(0, 0, 0);

                                            }
                                        }
                                        break;
                                    }

                                case LogicSenActInput.Operatoren.gleich:
                                    {
                                        if (leftValue == rightValue)
                                        {
                                            if (c.targetCommand.Equals(SensActConnection.commands.ON))
                                            {
                                                StartCoroutine(target.sendON());
                                                ConnectionManager.getInstance().stopConnection(c.id);
                                                SenActFenster.transform.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                                SenActFenster.transform.GetChild(1).transform.localScale = new Vector3(0, 0, 0);

                                            }
                                            else
                                            {
                                                StartCoroutine(target.sendOFF());
                                                ConnectionManager.getInstance().stopConnection(c.id);
                                                SenActFenster.transform.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                                SenActFenster.transform.GetChild(1).transform.localScale = new Vector3(0, 0, 0);

                                            }
                                        }
                                        break;
                                    }
                                case LogicSenActInput.Operatoren.groesser:
                                    {
                                        if (leftValue > rightValue)
                                        {
                                            if (c.targetCommand.Equals(SensActConnection.commands.ON))
                                            {
                                                StartCoroutine(target.sendON());
                                                ConnectionManager.getInstance().stopConnection(c.id);
                                                SenActFenster.transform.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                                SenActFenster.transform.GetChild(1).transform.localScale = new Vector3(0, 0, 0);

                                            }
                                            else
                                            {
                                                StartCoroutine(target.sendOFF());
                                                ConnectionManager.getInstance().stopConnection(c.id);
                                                SenActFenster.transform.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                                SenActFenster.transform.GetChild(1).transform.localScale = new Vector3(0, 0, 0);

                                            }
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }

                        }
                    }
                    else
                    {
                        if (nc != null)
                        {
                            if (nc.connState.Equals(Connection.States.Active))
                            {
                                Item source = ItemManager.getInstance().getItem(SourceNamelong);
                                Actuator target = (Actuator)ItemManager.getInstance().getItem(TargetNamelong);

                                NfcReader nfcr = (NfcReader)source;
                                string leftSide = nfcr.niceState;

                                if (nc.condOperator.Equals(LogicSenActInput.Operatoren.gleich))
                                {
                                    if (leftSide.Equals(nc.rightSide))
                                    {
                                        if (nc.targetCommand.Equals(SensActConnection.commands.ON))
                                        {
                                            StartCoroutine(target.sendON());
                                            ConnectionManager.getInstance().stopConnection(nc.id);
                                            SenActFenster.transform.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                            SenActFenster.transform.GetChild(1).transform.localScale = new Vector3(0, 0, 0);

                                        }
                                        else
                                        {
                                            StartCoroutine(target.sendOFF());
                                            ConnectionManager.getInstance().stopConnection(nc.id);
                                            SenActFenster.transform.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                            SenActFenster.transform.GetChild(1).transform.localScale = new Vector3(0, 0, 0);

                                        }

                                    }
                                }
                            }
                        } else
                        {
                            if (mc != null)
                            {
                                if (mc.connState.Equals(Connection.States.Active))
                                {
                                    Item source = ItemManager.getInstance().getItem(SourceNamelong);
                                    Actuator target = (Actuator)ItemManager.getInstance().getItem(TargetNamelong);

                                    MyoArmband myoa = (MyoArmband)source;

                                    string leftSide = myoa.niceState;

                                    if (mc.condOperator.Equals(LogicSenActInput.Operatoren.gleich))
                                    {
                                        if (leftSide.Equals(mc.rightSide))
                                        {
                                            if (mc.targetCommand.Equals(SensActConnection.commands.ON))
                                            {
                                                StartCoroutine(target.sendON());
                                                ConnectionManager.getInstance().stopConnection(mc.id);
                                                SenActFenster.transform.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                                SenActFenster.transform.GetChild(1).transform.localScale = new Vector3(0, 0, 0);

                                            }
                                            else
                                            {
                                                StartCoroutine(target.sendOFF());
                                                ConnectionManager.getInstance().stopConnection(mc.id);
                                                SenActFenster.transform.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                                SenActFenster.transform.GetChild(1).transform.localScale = new Vector3(0, 0, 0);

                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    updateCounter = 0;
                }
            }
                

            }

        public void Start()
        {
            updateCounter = 0;
        }
    }
}
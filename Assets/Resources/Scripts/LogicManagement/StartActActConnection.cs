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
    public class StartActActConnection : MonoBehaviour, IInputClickHandler, IFocusable

    {
        private bool isFocused;
        private string ButtonName;

        private string ActActFensterName;
        private GameObject ActActFenster;

        [Tooltip("Set to true if gestures update (ManipulationUpdated, NavigationUpdated) should be logged. Note that this can impact performance.")]
        [HideInInspector]
        public bool LogGesturesUpdateEvents = false;
        [HideInInspector]
        public string LineID;
        [HideInInspector]
        public string SourceNamelong; // ohne "Actuator_"  vorne
        [HideInInspector]
        public string TargetNamelong; // ohne "Actuator_"  vorne

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
            //Needs: LineID, Source Name, Target Name, Source Operation, Target Operation

           bool exists=false;

           if (ConnectionManager.getInstance().getConnections().ContainsKey(LineID))
                exists = true;

            ActActFensterName = this.gameObject.transform.parent.name;
            ActActFenster = this.gameObject.transform.parent.gameObject;

            ActActConnection.commands sourceCommand;
            ActActConnection.commands targetCommand;

            bool opSource = ActActFenster.GetComponent<LogicActActInput>().GetSourceStatus(); //false = off, true = on
            bool opTarget = ActActFenster.GetComponent<LogicActActInput>().GetTargetStatus(); //false = off, true = on

            if (!opSource)
                sourceCommand = ActActConnection.commands.OFF;
            else
                sourceCommand = ActActConnection.commands.ON;
            if (!opTarget)
                targetCommand = ActActConnection.commands.OFF;
            else
                targetCommand = ActActConnection.commands.ON;

            Item source = ItemManager.getInstance().getItem(SourceNamelong);
            Item target = ItemManager.getInstance().getItem(TargetNamelong);

            if (source is Actuator && target is Actuator)
            {
                Actuator aSource = (Actuator)source;
                Actuator aTarget = (Actuator)target;
                if (!exists)
                {
                    ActActConnection c = ConnectionCreator.createActActConnection(LineID, SourceNamelong, TargetNamelong, sourceCommand, targetCommand);
                }

                ConnectionManager.getInstance().startConnection(LineID);
                if (sourceCommand.Equals(ActActConnection.commands.ON))
                {
                    StartCoroutine(aSource.sendON());
                    ConnectionManager.getInstance().stopConnection(LineID);
                }
                else if (sourceCommand.Equals(ActActConnection.commands.OFF))
                {
                    StartCoroutine(aSource.sendOFF());
                    ConnectionManager.getInstance().stopConnection(LineID);
                }

                if (targetCommand.Equals(ActActConnection.commands.ON))
                {
                    StartCoroutine(aTarget.sendON());
                    ConnectionManager.getInstance().stopConnection(LineID);
                }
                else if (targetCommand.Equals(ActActConnection.commands.OFF))
                {
                    StartCoroutine(aTarget.sendOFF());
                    ConnectionManager.getInstance().stopConnection(LineID);
                }
            }

            //ActActFenster.transform.GetChild(1).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //this.transform.localScale = new Vector3(0, 0, 0);


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

        private void Update()
        {
            
        }

    }
}
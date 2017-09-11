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
    public class ToggleSenActOnOff : MonoBehaviour, IInputClickHandler, IFocusable

    {
        private bool isFocused;
        private string ButtonName;

        private string SenActFensterName;
        private GameObject SenActFenster;

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
            SenActFensterName = this.gameObject.transform.parent.parent.parent.name;
            SenActFenster = this.gameObject.transform.parent.parent.parent.gameObject;
            ButtonName = this.name;

            if (ButtonName == "Button_On")
            {
                SenActFenster.GetComponent<LogicSenActInput>().SetTargetStatus(true);
                this.gameObject.SendMessageUpwards("SetOn");
                Debug.Log("I clicked " + ButtonName);
            }
            
            if (ButtonName == "Button_Off")
            {
                SenActFenster.GetComponent<LogicSenActInput>().SetTargetStatus(false);
                this.gameObject.SendMessageUpwards("SetOff");
                Debug.Log("I clicked " + ButtonName);
            }
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
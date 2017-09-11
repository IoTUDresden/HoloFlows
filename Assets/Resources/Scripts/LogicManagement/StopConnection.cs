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
    public class StopConnection : MonoBehaviour, IInputClickHandler, IFocusable

    {
        private bool isFocused;
        private string ButtonName;

        private string ActActFensterName;
        private GameObject ActActFenster;

        private string SenActFensterName;
        private string SenActFenster;

        [Tooltip("Set to true if gestures update (ManipulationUpdated, NavigationUpdated) should be logged. Note that this can impact performance.")]
        [HideInInspector]
        public bool LogGesturesUpdateEvents = false;
        public string LineID;




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
            ConnectionManager.getInstance().stopConnection(LineID);
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
            transform.parent.GetChild(3).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

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

        }


    }
}

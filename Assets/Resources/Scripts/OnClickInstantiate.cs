// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// mit kreativen Erweiterungen von MGohlke :D

using UnityEngine;


namespace HoloToolkit.Unity.InputModule.Tests
{
    /// <summary>
    /// Test behaviour that simply prints out a message very time a supported event is received from the input module.
    /// This is used to make sure that the input module routes events appropriately to game objects.
    /// </summary>
    public class OnClickInstantiate : MonoBehaviour, IInputClickHandler, IFocusable
        
    {
        // properties for highlighting behaviour
        private Style style;
        private MeshRenderer cubeMeshRenderer;
        private Material cubeDefaultMat;
        private bool isFocused;

        [HideInInspector]
        public bool isSelected; // used while creating new connections

        [Tooltip("Set to true if gestures update (ManipulationUpdated, NavigationUpdated) should be logged. Note that this can impact performance.")]
        [HideInInspector]
        public bool LogGesturesUpdateEvents = false;

        void Start()
        {
            style = GameObject.Find("GlobalStyle").GetComponent<Style>(); // refence to GameObject GlobalStyle 
            cubeMeshRenderer = this.GetComponent<MeshRenderer>(); // reference to MeshRenderer for implementing a focus and select effect (highlighting cube)
            cubeDefaultMat = this.GetComponent<MeshRenderer>().material; // remember the original material

            isSelected = false;
            isFocused = false;
        }

        void Update()
        {
            if (isSelected)
            {
                cubeMeshRenderer.material = style.ClickedOnMat;
            }
            if (!isSelected && !isFocused)
            {
                cubeMeshRenderer.material = cubeDefaultMat;
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
            string Plopp = this.name;

            GameObject.Find("Managers").transform.GetComponent<InstantiatePrefab>().InstantiateSensor(Plopp);

            //isSelected = !isSelected;
        }

        public void OnFocusEnter()
        {
            //Debug.Log("OnFocusEnter");
            isFocused = true;

            cubeMeshRenderer.material = style.highlightMat;
        }

        public void OnFocusExit()
        {
            //Debug.Log("OnFocusExit");
            isFocused = false;

            if (!isSelected)
                cubeMeshRenderer.material = cubeDefaultMat;
        }


    }
}
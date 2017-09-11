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
    public class ButtonConnect : MonoBehaviour, IInputClickHandler, IFocusable


    
    {
        [HideInInspector]
        public bool ClickWurf1; // is Würfel1 Selected
        [HideInInspector]
        public bool ClickWurf2; // is Würfel2 Selected
        [HideInInspector]
        public GameObject Wurfell1;
        [HideInInspector]
        public GameObject Wurfell2;
        [HideInInspector]
        public string Wurfell1ParentName;
        [HideInInspector]
        public string Wurfell2ParentName;
        [HideInInspector]
        public string Wurfell1ParentTyp;
        [HideInInspector]
        public string Wurfell2ParentTyp;
        [HideInInspector]
        public string Wurfell1ParentNameName;
        [HideInInspector]
        public string Wurfell2ParentNameName;

        [HideInInspector]
        public string Wurfell1ParentNameComplete;
        [HideInInspector]
        public string Wurfell2ParentNameComplete;

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

        private string Vergleichsname;
        private string Vergleichstyp;

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
            //Debug.LogFormat("OnInputClicked\r\nSource: {0}  SourceId: {1} TapCount: {2}", eventData.InputSource, eventData.SourceId, eventData.TapCount);
            ClickWurf1 = GameObject.Find("Managers").GetComponent<Linedraw>().ClickonWurfel1;
            ClickWurf2 = GameObject.Find("Managers").GetComponent<Linedraw>().ClickonWurfel2;

            if (ClickWurf1 == false)
            {
                Wurfell1 = this.gameObject;
                GameObject.Find("Managers").GetComponent<Linedraw>().toggleClickonWurfel1();
                GameObject.Find("Managers").GetComponent<Linedraw>().Wurfel1 = Wurfell1;
                Wurfell1ParentName = Wurfell1.transform.parent.name;
                string [] Wurfell1ParentSplit  = Wurfell1ParentName.Split( new char[] { '_'},2);

                if (this.name == "Button_Actuator")
                {
                    Wurfell1ParentTyp = "Actuator";
                }
                if (this.name == "Button_Sensor")
                {
                    Wurfell1ParentTyp = "Sensor";
                }
                else
                {
                    Wurfell1ParentTyp = Wurfell1ParentSplit[0];
                }               
                                
                Wurfell1ParentNameName = Wurfell1ParentSplit[1];
                Debug.Log("Mein erster Würfel wurde als >> " + Wurfell1ParentTyp + " << erkannt.");
                Debug.Log("Ich habe einen Wurfel 1 geclickt, dessen Parent ist: " + Wurfell1ParentNameName);

                Item source = ItemManager.getInstance().getItem(Wurfell1ParentNameName);
                GameObject.Find("Managers").GetComponent<Linedraw>().Wurfel1ParentCompleteName = source.shortName;
                GameObject.Find("Managers").GetComponent<Linedraw>().Wurfel1ParentName = Wurfell1ParentNameName;
                GameObject.Find("Managers").GetComponent<Linedraw>().Wurfel1ParentTyp = Wurfell1ParentTyp;

            }

            if (ClickWurf1 ==true && ClickWurf2 == false)
            {
                string vergleichsparent;
                string aktuellertyp;
                aktuellertyp = "";
                if (this.name == "Button_Sensor")
                {
                    aktuellertyp = "Sensor";
                }
                //    Vergleichstyp = "Act"
                //if (this.name== "Button_Sensor" || this.name == "Button_Actuator")
                //{
                //    Vergleichstyp = "Actuator";
                //}
                //else
                //{
                Vergleichstyp = GameObject.Find("Managers").GetComponent<Linedraw>().Wurfel1ParentTyp;
                //}                
                Vergleichsname = GameObject.Find("Managers").GetComponent<Linedraw>().Wurfel1ParentName;
                vergleichsparent = Vergleichstyp + "_" + Vergleichsname;
                if (this.gameObject.transform.parent.name.Contains("homematic_dimmer"))
                {
                    vergleichsparent = "Actuator_" + Vergleichsname;                    
                }

                
                if ((this.transform.parent.name == vergleichsparent && this.transform.parent.name.Contains(Vergleichstyp )) || (this.transform.parent.name == vergleichsparent && Vergleichstyp == aktuellertyp && aktuellertyp == "Sensor"))
                {
                    
                    GameObject.Find("Managers").GetComponent<Linedraw>().toggleClickonWurfel1();
                    Debug.Log("Zweimal das gleiche ausgewählt, Würfel wurde deselektiert");
                }
                else
                {
                    Wurfell2 = this.gameObject;
                    GameObject.Find("Managers").GetComponent<Linedraw>().toggleClickonWurfel2();
                    GameObject.Find("Managers").GetComponent<Linedraw>().Wurfel2 = Wurfell2;
                    Wurfell2ParentName = Wurfell2.transform.parent.name;
                    string[] Wurfell2ParentSplit = Wurfell2ParentName.Split(new char[] { '_' }, 2);
                    Wurfell2ParentTyp = Wurfell2ParentSplit[0];

                    //falls ,2 nichts nützt beim splitten
                    //Wurfell2ParentNameComplete = Wurfell2ParentSplit[1];
                    //for (int i = 2; i < Wurfell2ParentSplit.Length; i++)
                    //{
                    //    Wurfell2ParentNameComplete = Wurfell2ParentNameComplete +"_"+ Wurfell2ParentSplit[i];
                    //}

                    if (this.name == "Button_Actuator")
                    {
                        Wurfell2ParentTyp = "Actuator";
                    }
                    if (this.name == "Button_Sensor")
                    {
                        Wurfell2ParentTyp = "Sensor";
                    }
                    else
                    {
                        Wurfell1ParentTyp = Wurfell2ParentSplit[0];
                    }

                    Wurfell2ParentNameName = Wurfell2ParentSplit[1];
                    Debug.Log("Mein zweiter Würfel wurde als >> " + Wurfell2ParentTyp + " << erkannt.");
                    Debug.Log("Ich habe einen Wurfel 2 gecliiickt, dessen Parent ist: " + Wurfell2ParentNameName);

                    Item target = ItemManager.getInstance().getItem(Wurfell2ParentNameName);
                    GameObject.Find("Managers").GetComponent<Linedraw>().Wurfel2ParentCompleteName = target.shortName;
                    GameObject.Find("Managers").GetComponent<Linedraw>().Wurfel2ParentName = Wurfell2ParentNameName;
                    GameObject.Find("Managers").GetComponent<Linedraw>().Wurfel2ParentTyp = Wurfell2ParentTyp;
                }                                
            }

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
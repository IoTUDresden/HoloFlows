using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule.Tests;

public class Linedraw : MonoBehaviour {

    [HideInInspector] 
    public GameObject Wurfel1;
    [HideInInspector]
    public GameObject Wurfel2;

    private Vector3 Wurfel1Cent;
    private Vector3 Wurfel2Cent;
    [HideInInspector]
    public bool ClickonWurfel1;
    [HideInInspector]
    public bool ClickonWurfel2;
    public Transform Line;
    private Transform CubeLine;
    private LineRenderer CubeLineRenderer;

    public Transform LogicYesPrefab;
    private Transform LogicInitializer;

    public Transform LogicNoPrefab;
    private Transform LogicCanceller;

    public Transform LogicSenActPrefab;   

    public Transform LogicActActPrefab;   

    [HideInInspector]
    public string Wurfel1ParentName;
    [HideInInspector]
    public string Wurfel2ParentName;
    [HideInInspector]
    public string Wurfel1ParentTyp;
    [HideInInspector]
    public string Wurfel2ParentTyp;

    [HideInInspector]
    public string Wurfel1ParentCompleteName;
    [HideInInspector]
    public string Wurfel2ParentCompleteName;

    
    private Vector3 FensterPos;

    [HideInInspector]
    public string FensterName;

    private string LineID;
    private int zaehler;
    private float FensterPosFaktor;


    // Use this for initialization
    void Start () {

        ClickonWurfel1 = false;
        ClickonWurfel2 = false;        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (ClickonWurfel1 == true && ClickonWurfel2 == false)
        {
            
            //Debug.Log("Ich habe einen Wurfel AAins gecliiickt " + Wurfel1.name);

        }

        if (ClickonWurfel1 == true && ClickonWurfel2 == true)
        {
            
            Debug.Log("Ich habe Wuurfel Zwaaai gecliiickt " + Wurfel2.name);
           
            //Koordianten des Würfelzentrums 
            Wurfel1Cent = Wurfel1.transform.position;
            Wurfel2Cent = Wurfel2.transform.position;

            // Line aus Prefab instantisieren und Linerenderer Komponente aufrufen
            CubeLine = Instantiate(Line) as Transform;
            zaehler = 1;
            LineID = "Line_" + Wurfel1ParentName + "-" + Wurfel2ParentName + zaehler;
            while (ConnectionManager.getInstance().getConnection(LineID) != null)
            {
                zaehler++;
                LineID = "Line_" + Wurfel1ParentName + "-" + Wurfel2ParentName + zaehler;
            }
            CubeLine.name = LineID;

            //CubeLine.name = "Line_" + Wurfel1ParentName + "-" + Wurfel2ParentName;
            //LineID = "Line_" + Wurfel1ParentName + "-" + Wurfel2ParentName;

            CubeLineRenderer = CubeLine.GetComponent<LineRenderer>();


            //Start- und Endpunkt der Linie festlegen (bzw. index gibt eckpunkte an...)
            CubeLineRenderer.SetPosition(0, Wurfel1Cent);
            CubeLineRenderer.SetPosition(1, Wurfel2Cent);
            FensterPosFaktor = 0.2f * zaehler;

            FensterPos = Vector3.Lerp(Wurfel1Cent, Wurfel2Cent,FensterPosFaktor);
            
            //rufe Fenster auf

            if (Wurfel1ParentTyp == "Sensor" && Wurfel2ParentTyp == "Sensor" || Wurfel1ParentTyp == "Actuator" && Wurfel2ParentTyp == "Sensor" || (Wurfel1ParentName.Contains("adafruit") && Wurfel2ParentTyp == "Actuator" && !Wurfel2ParentName.Contains("bulb210_dimmer")))
            {
                LogicCanceller = Instantiate(LogicNoPrefab) as Transform;
                LogicCanceller.transform.position = FensterPos;
                FensterName = LogicCanceller.name;
                LogicCanceller.GetChild(0).GetChild(1).GetComponent<Text>().text = Wurfel1ParentCompleteName;
                LogicCanceller.GetChild(0).GetChild(2).GetComponent<Text>().text = Wurfel2ParentCompleteName;
                LogicCanceller.GetChild(1).GetComponent<CancelButton>().LineName = LineID;
            }

            else
            {
                LogicInitializer = Instantiate(LogicYesPrefab) as Transform;
                LogicInitializer.transform.position = FensterPos;
                FensterName = LogicInitializer.name;
                LogicInitializer.GetChild(0).GetChild(1).GetComponent<Text>().text = Wurfel1ParentCompleteName; //übergibt shortname an textfeld
                LogicInitializer.GetChild(0).GetChild(2).GetComponent<Text>().text = Wurfel2ParentCompleteName; //übergibt shortname an textfeld
                //übergebe Quellen Name und Typ
                LogicInitializer.GetChild(1).GetComponent<StartLogic>().SourceName = Wurfel1ParentName; //übergibt langName (nur ohne Typ_)
                LogicInitializer.GetChild(1).GetComponent<StartLogic>().SourceNameShort = Wurfel1ParentCompleteName; //übergibt shortName
                LogicInitializer.GetChild(1).GetComponent<StartLogic>().SourceType = Wurfel1ParentTyp;
                //übergebe Ziel Name und Typ
                LogicInitializer.GetChild(1).GetComponent<StartLogic>().TargetName = Wurfel2ParentName; //übergibt langName (nur ohne Typ_)
                LogicInitializer.GetChild(1).GetComponent<StartLogic>().TargetNameShort = Wurfel2ParentCompleteName; //übergibt shortName
                LogicInitializer.GetChild(1).GetComponent<StartLogic>().TargetType = Wurfel2ParentTyp;
                //übergebe Linien-ID
                LogicInitializer.GetChild(1).GetComponent<StartLogic>().LineID = LineID;
                //übergebe Prefabs
                LogicInitializer.GetChild(1).GetComponent<StartLogic>().LogicSenActPrefabb = LogicSenActPrefab;
                LogicInitializer.GetChild(1).GetComponent<StartLogic>().LogicActActPrefabb = LogicActActPrefab;
                //übergebe Position
                LogicInitializer.GetChild(1).GetComponent<StartLogic>().FensterPoss = FensterPos;

                //übergebe LineName an CancelButton
                LogicInitializer.GetChild(2).GetComponent<CancelButton>().LineName = LineID;

                LogicInitializer.GetChild(3).GetComponent<CancelButton>().LineName = LineID;

                LogicInitializer.GetChild(4).GetComponent<StopPotiDimmer>().LineID = LineID;

                LogicInitializer.GetChild(1).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                LogicInitializer.GetChild(2).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                LogicInitializer.GetChild(3).transform.localScale = new Vector3(0, 0, 0);
                LogicInitializer.GetChild(4).transform.localScale = new Vector3(0, 0, 0);
            }


            //ab hier ist die Linie gemalt
            
            // am Ende werden beide Bools wieder auf false gesetzt :D
            ClickonWurfel1 = false;
            ClickonWurfel2 = false;
            
            
        }

        if (Wurfel1 != null)
        {
            if (ClickonWurfel1)
                Wurfel1.GetComponent<ButtonConnect>().isSelected = true;
            else
                Wurfel1.GetComponent<ButtonConnect>().isSelected = false;
        }

        if (Wurfel2 != null)
        {
            if (ClickonWurfel2)
                Wurfel2.GetComponent<ButtonConnect>().isSelected = true;
            else
                Wurfel2.GetComponent<ButtonConnect>().isSelected = false;
        }
    }

    public void toggleClickonWurfel1()
    {
        ClickonWurfel1 = !ClickonWurfel1;        
    }

    public void toggleClickonWurfel2()
    {
        ClickonWurfel2 = !ClickonWurfel2;
    }


}

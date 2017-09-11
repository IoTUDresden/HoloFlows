using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule.Tests;
using HoloToolkit.Unity.InputModule;

public class InstantiatePrefab : MonoBehaviour
{
    public Transform SensorPrefab;
    public Transform DimmerPrefab;
    public Transform ActuatorPrefab;



    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    public void InstantiateSensor(string SourceName)
    {
        string Name = ((Sensor)ItemManager.getInstance().getItem(SourceName)).id;
        string state = ((Sensor)ItemManager.getInstance().getItem(SourceName)).state;
        string unit = ((Sensor)ItemManager.getInstance().getItem(SourceName)).unit;

        string iconPath = ((Sensor)ItemManager.getInstance().getItem(SourceName)).icon;
        Sprite Icon = Resources.Load<Sprite>(iconPath);
        if (Icon == null)
            Debug.Log("icon was null (could not find " + iconPath + ")");
        // Instanziieren, Startposition festlegen, OberGameobjectNamen festlegen
        Transform SensorP = Instantiate(SensorPrefab) as Transform;
        SensorP.transform.position = new Vector3(0, 0, 0); // probieren was sinnvoller ist
        SensorP.name = "Sensor_" + Name;
        Debug.Log("instantiated " + SensorP.name);
        //Eigenschaften weitergeben
        GameObject.Find(SensorP.name).transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Icon;
        GameObject.Find(SensorP.name).transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = Name;
        string Value = state + unit;
        GameObject.Find(SensorP.name).transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = Value;
        GameObject.Find(SensorP.name).transform.GetChild(3).GetComponent<TapToPlaceParent>().SavedAnchorFriendlyName = SensorP.name;
    }

    public void InstantiateDimmer(string SourceName)
    {
        string Name = ((Actuator)ItemManager.getInstance().getItem(SourceName)).id;
        string state = ((Actuator)ItemManager.getInstance().getItem(SourceName)).state;

        // Instanziieren, Startposition festlegen, OberGameobjectNamen festlegen
        Transform DimmerP = Instantiate(DimmerPrefab) as Transform;
        DimmerP.transform.position = new Vector3(0, 0, 0); // probieren was sinnvoller ist
        DimmerP.name = "Actuator_" + Name;

        //Eigenschaften weitergeben
        //SensorP.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Icon;
        DimmerP.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = Name;
        string Value = state;
        DimmerP.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = Value;
        DimmerP.transform.GetChild(3).GetComponent<TapToPlaceParent>().SavedAnchorFriendlyName = DimmerP.name;
    }

    public void InstantiateActuator(string SourceName)
    {
        string Name = ((Actuator)ItemManager.getInstance().getItem(SourceName)).id;
        string state = ((Actuator)ItemManager.getInstance().getItem(SourceName)).state;
        Sprite Icon = Resources.Load<Sprite>(((Actuator)ItemManager.getInstance().getItem(SourceName)).icon);
        string On = ((Actuator)ItemManager.getInstance().getItem(SourceName)).On;
        string Off = ((Actuator)ItemManager.getInstance().getItem(SourceName)).Off;

        //string On = "On";
        //string Off = "Off"; 
        // Instanziieren, Startposition festlegen, OberGameobjectNamen festlegen
        Transform ActuatorP = Instantiate(ActuatorPrefab) as Transform;
        ActuatorP.transform.position = new Vector3(0, 0, 0); // probieren was sinnvoller ist
        ActuatorP.name = "Actuator_" + Name;

        //Eigenschaften weitergeben
        ActuatorP.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Icon;
        ActuatorP.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = Name;
        string Value = state;
        ActuatorP.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = Value;

        ActuatorP.transform.GetChild(3).GetComponent<TapToPlaceParent>().SavedAnchorFriendlyName = ActuatorP.name;

        //Abhängig von string inhalt füllen von Buttons etc.
        if (Off == null) // d.h. kein Off Button
        {
            //Blende einen Button aus
            ActuatorP.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0, 0, 0);
            //Verschiebe den Rest
            ActuatorP.transform.GetChild(0).GetChild(0).transform.localPosition = new Vector3(0, 0.35f, 0);
            ActuatorP.transform.GetChild(0).GetChild(2).transform.localPosition = new Vector3(0, 0.05f, -0.025f);
            //gib Namen zu Button
            ActuatorP.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMesh>().text = On;
        }
        else
        {
            ActuatorP.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMesh>().text = Off;
            ActuatorP.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMesh>().text = On;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicSenActInput : MonoBehaviour
{

    

    private bool TargetToggleStatus;

    [HideInInspector]
    public string Fenstername;

    [HideInInspector]
    public enum Operatoren { kleiner, gleich, groesser};

    Operatoren Operator;

    private string SenValue;
    private float SenValueFloat;

    [HideInInspector]
    public string SourceName;
    [HideInInspector]
    public string TargetName;
    [HideInInspector]
    public string LineID;

    [HideInInspector]
    public int CurrentIndex;

    // Use this for initialization
    void Start()
    {
        
        
        Fenstername = this.gameObject.name;

        Operator = Operatoren.gleich;

        SenValue = this.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Text>().text;
        Item source = ItemManager.getInstance().getItem(SourceName);
        if (!(source is NfcReader)&&!(source is MyoArmband))
        {
            SenValueFloat = float.Parse(SenValue);
        }
        TargetToggleStatus = false;
        CurrentIndex = 0;
    }

    //hier wird der Operator hingeschickt
    public Operatoren GetOperator()
    {
        return this.Operator;
    }

    public void  SetOperator(Operatoren Operator)
    {
        this.Operator = Operator;
    }

    public void SetSenValue(float SenValueFloat)
    {
        this.SenValueFloat = SenValueFloat;
    }

    //hier wird der neue SensorVergleichswert hingeschickt !! aber noch nicht angezeigt
    public float GetSenValue()
    {
        return this.SenValueFloat;
    }
   
    public string GetSenValueString()
    {
        return this.SenValue;
    }

    public void SetSenValueString(string SenValue)
    {
        this.SenValue = SenValue;
    }


    // Fall NFC-Reader

    public void SetDictIndex(int CurrentIndex)
    {
        this.CurrentIndex = CurrentIndex;
    }

    //hier wird der neue Indexvergleichswert hingeschickt !! aber noch nicht angezeigt
    public int GetDictIndex()
    {
        return this.CurrentIndex;
    }

    // hier wird der gewünschte Zustand für den Actuator hingeschickt
    public bool GetTargetStatus()
    {
        return this.TargetToggleStatus;
    }

    public void SetTargetStatus(bool TargetToggleStatus)
    {
        this.TargetToggleStatus = TargetToggleStatus;
    }

}

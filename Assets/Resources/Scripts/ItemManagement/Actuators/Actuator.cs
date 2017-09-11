using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actuator : Item{

    public string icon;
    public string On;
    public string Off;
   


    public Actuator(string id, string state, string shortName, string icon = null, string On = "On", string Off = "Off")
    {
        this.id = id;
        this.state = state;
        this.shortName = shortName;
        this.type = "Actuator";

        if (icon == null)
            this.icon = "Icons/Actuator";
        else this.icon = icon;
        this.On = On;
        this.Off = Off;
        
    }

    public abstract IEnumerator sendON();

    public abstract IEnumerator sendOFF();

    public override void update(string id, string state, string shortName, string unit = null)
    {
        this.id = id;
        this.state = state;
        this.shortName = shortName;
    }
}

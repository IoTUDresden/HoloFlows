using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : Item {

    public string unit;
    public float interval;
    public string icon;

    public Sensor(string name, string state, string unit, string shortName, float interval=0, string icon=null)
    {
        this.id = name;
        this.state = state;
        this.unit = unit;
        this.shortName = shortName;
        this.interval = interval;
        this.type = "Sensor";
        if (icon == null)
            this.icon = "Icons/Sensor";
        else this.icon = icon;
    }

    public override void update(string id, string state, string shortName, string unit = null)
    {
        this.id = id;
        this.state = state;
        this.shortName = shortName;
        this.unit = unit;
    }
}

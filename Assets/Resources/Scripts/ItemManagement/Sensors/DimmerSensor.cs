using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimmerSensor : Sensor
{

    public DimmerSensor(string name, string state, string unit, string shortName) : base(name, state, unit, shortName, 10f, "Icons/Dimmer")
    {

    }
}

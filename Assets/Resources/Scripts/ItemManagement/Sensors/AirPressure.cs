using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPressure : Sensor {

    public AirPressure(string name, string state, string unit, string shortName) : base(name, state, unit, shortName, 10f, "Icons/Airpressure")
    {

    }
}

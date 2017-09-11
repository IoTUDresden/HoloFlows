using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humidity : Sensor {

    public Humidity(string name, string state, string unit, string shortName) : base(name, state, unit, shortName, 5f, "Icons/Humidity")
    {

    }
}

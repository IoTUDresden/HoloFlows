using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poti : Sensor
{
    public Poti(string name, string state, string unit, string shortName) : base(name, state, unit, shortName, 5f, "Icons/LinPoti")
    {
    }
}


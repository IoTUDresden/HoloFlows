using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLight : Sensor {

    public AmbientLight(string name, string state, string unit, string shortName) : base(name, state, unit, shortName, 50f, "Icons/Luminance")
    {
        
    }
}

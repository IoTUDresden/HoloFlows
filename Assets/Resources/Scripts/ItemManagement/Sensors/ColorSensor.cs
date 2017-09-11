using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ColorSensor : Sensor
{

    public int r;
    public int g;
    public int b;

    public ColorSensor(string name, string state, string shortName) : base(name, state, "", shortName, 0, "Icons/ColourSensor2")
    {
            string[] colors = state.Split(',');
            int.TryParse(colors[0], out r);
            int.TryParse(colors[1], out g);
            int.TryParse(colors[2], out b);

     

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueColor : Actuator
{

    private RESTRequest Colorrequest;

    public int h;
    public int s;
    public int v;


    public HueColor(string id, string state, string shortName) : base(id, state, shortName, "Icons/LampHue")
    {
        string[] colors = state.Split(',');
        int.TryParse(colors[0], out h);
        int.TryParse(colors[1], out s);
        int.TryParse(colors[2], out v);

    }

    public override IEnumerator sendON()
    {
        return null;
    }

    public override IEnumerator sendOFF()
    {
        return null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHueConnection : Connection
{
    string id;
    ColorSensor source;
    HueDimmer target;

    public ColorHueConnection(string id, ColorSensor source, HueDimmer target) : base(id, source, target)
    {
        this.id = id;
        this.source = source;
        this.target = target;
    }

    public override void play()
    {


    }

}

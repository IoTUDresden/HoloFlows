using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotiLightConnection : Connection
{
    string id;
    Poti source;
    DimmerActuator target;

    public PotiLightConnection(string id, Poti source, DimmerActuator target) : base(id, source, target)
    {
        this.id = id;
        this.source = source;
        this.target = target;
    }

    public override void play()
    {

       
    }

}

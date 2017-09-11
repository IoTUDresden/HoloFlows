using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotiHueConnection : Connection
{
    string id;
    Poti source;
    HueDimmer target;

    public PotiHueConnection(string id, Poti source, HueDimmer target) : base(id, source, target)
    {
        this.id = id;
        this.source = source;
        this.target = target;
    }

    public override void play()
    {


    }

}

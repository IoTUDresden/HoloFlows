using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyoActConnection : Connection
{

    String id;
    Sensor source;
    Actuator target;

    public LogicSenActInput.Operatoren condOperator;

    public string rightSide;

    public SensActConnection.commands targetCommand;

    public MyoActConnection(string id, Sensor source, Actuator target, string rightSide, LogicSenActInput.Operatoren condOperator, SensActConnection.commands targetCommand) : base(id, source, target)
    {
        this.id = id;
        this.source = source;
        this.target = target;
        this.rightSide = rightSide;
        this.condOperator = LogicSenActInput.Operatoren.gleich;
        this.targetCommand = targetCommand;
    }

    public override void play()
    {


    }

}

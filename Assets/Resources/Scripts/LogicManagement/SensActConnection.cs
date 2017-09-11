using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensActConnection : Connection
{

    String id;
    Sensor source;
    Actuator target;

    public LogicSenActInput.Operatoren condOperator;

    public float rightSide;

    public enum commands { ON, OFF };

    public commands targetCommand;

    public SensActConnection(string id, Sensor source, Actuator target, float rightSide, LogicSenActInput.Operatoren condOperator, commands targetCommand) : base(id, source, target)
    {
        this.id = id;
        this.source = source;
        this.target = target;
        this.rightSide = rightSide;
        this.condOperator = condOperator;
        this.targetCommand = targetCommand;
    }

    public override void play()
    {

      
    }

}

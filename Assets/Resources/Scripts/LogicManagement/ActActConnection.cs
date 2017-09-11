using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActActConnection : Connection
{
    Actuator source;
    Actuator target;

    public enum commands {ON, OFF };

    public commands sourceCommand;
    public commands targetCommand;

    String id;

    public ActActConnection(string id, Actuator source, Actuator target, commands sourceCommand, commands targetCommand) : base(id, source, target)
    {
        this.id = id;
        this.source = source;
        this.target = target;
        this.targetCommand = targetCommand;
        this.sourceCommand = sourceCommand;
    }


    public override void play()
    {

     
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Connection
{

    public string id;

    public Item source;
    public Item target;
    public States connState;

    public enum States { Active, Inactive, Deleted };

    public Connection(string id, Item source, Item target)
    {
        this.id = id;
        this.source = source;
        this.target = target;
        this.connState = States.Inactive;
    }

    public abstract void play();

    public void stop()
    {
        this.connState = States.Inactive;
    }

    public void delete()
    {
        this.connState = States.Deleted;
        this.target = null;
        this.source = null;
    }


}

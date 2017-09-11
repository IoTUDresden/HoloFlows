using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item {
    public string id;
    public string state;
    public string shortName;
    public string type;

    public abstract void update(string id, string state, string shortName, string unit=null);
}

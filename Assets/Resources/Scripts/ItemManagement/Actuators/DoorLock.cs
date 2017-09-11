using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : NormalItemActuator
{

    public DoorLock(string id, string state, string shortName) :base(id, state, shortName, "Icons/Door", "Open", "Lock")
    {

    }
}

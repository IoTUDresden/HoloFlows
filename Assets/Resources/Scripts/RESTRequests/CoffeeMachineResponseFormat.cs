using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachineResponseFormat {

    public Status status;
    public Sensor sensors;
    public Appliance appliance;
    
    public class Status
    {
        public bool working;
        public string enoughwater;
        public bool carafe;
        public bool timerevent;
        public bool ready;
        public int cups;
    }

    public class Appliance
    {
        public string model;
    }

    public class Sensor
    {
        public bool heater;
        public int waterlevel;
        public bool grinder;
        public bool hotplate;
    }

}

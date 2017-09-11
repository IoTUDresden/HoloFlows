using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachineActuator : Actuator {

    private string waterLevel;
    private RESTRequest request;

    // Use this for initialization
    public CoffeeMachineActuator(string id, string state, string shortName) : base(id, state, shortName, "Icons/Coffeemaker", "Brew", null)
    {
        this.id = id;
        this.state = state;
        this.shortName = shortName;
        request = RESTRequestFactory.createGETRequest(Settings.URL_CoffeeMachine_GET_Brew);
        
    }

    public string getWaterLevel()
    {
        return this.waterLevel;
    }

    public void setWaterLevel(string level)
    {
        this.waterLevel = level;
    }

    public override IEnumerator sendON()
    {
        Debug.Log("brew it! ");
        return request.performAction();
    }

    public override IEnumerator sendOFF()
    {
        return null;
    }
}

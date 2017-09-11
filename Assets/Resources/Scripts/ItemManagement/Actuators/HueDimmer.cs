using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HueDimmer : Actuator
{

    private RESTRequest INCREASErequest;
    private RESTRequest DECREASErequest;
    private RESTRequest ONrequest;
    private RESTRequest OFFrequest;
    private RESTRequest stateRequest;
    private RESTRequest Colorrequest;

    public HueColor hueColor;


    public HueDimmer(string id, string state, string shortName) : base(id, state, shortName, "Icons/Lamp")
    {
        this.ONrequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + id, 100.ToString());
        this.OFFrequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + id, 0.ToString());
    }

    public IEnumerator sendINCREASE()
    {
        int value;
        int currentValue;

        int.TryParse(state,out currentValue);

        if (currentValue > 90)  {
            value = 100;
        } else
        {
            value = currentValue + 10;
        }    

        this.INCREASErequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + id, value.ToString());
        return INCREASErequest.performAction();
    }

    public IEnumerator sendDECREASE()
    {
        int value;
        int currentValue;

        int.TryParse(state, out currentValue);

        if (currentValue < 10)
        {
            value = 0;
        }
        else
        {
            value = currentValue - 10;
        }

        this.DECREASErequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + id, value.ToString());
        return DECREASErequest.performAction();
    }
    public override IEnumerator sendON()
    {
        return ONrequest.performAction();
    }

    public override IEnumerator sendOFF()
    {
        return OFFrequest.performAction();
    }

    public IEnumerator sendState(string state)
    {
        stateRequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + id, state);
        return stateRequest.performAction();
    }

    public IEnumerator sendColor(int h, int s, int v)
    {
        string color = h + "," + s + "," + v;
        this.Colorrequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + hueColor.id, color);
        
        return Colorrequest.performAction();
    }
}

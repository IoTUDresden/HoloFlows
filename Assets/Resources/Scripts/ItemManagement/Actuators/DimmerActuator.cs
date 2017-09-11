using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimmerActuator : Actuator {

    private RESTRequest INCREASErequest;
    private RESTRequest DECREASErequest;
    private RESTRequest ONrequest;
    private RESTRequest OFFrequest;


    public DimmerActuator(string id, string state, string shortName) : base(id, state, shortName, "Icons/Dimmer")
    {
        this.INCREASErequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + id, Settings.POST_INCREASE);
        this.DECREASErequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + id, Settings.POST_DECREASE);
        this.ONrequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + id, Settings.POST_ON);
        this.OFFrequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + id, Settings.POST_OFF);
    }

    public IEnumerator sendINCREASE()
    {
        return INCREASErequest.performAction();
    }

    public IEnumerator sendDECREASE()
    {
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
}

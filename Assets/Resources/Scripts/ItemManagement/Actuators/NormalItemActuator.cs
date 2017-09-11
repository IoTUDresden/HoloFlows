using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItemActuator : Actuator {

    private RESTRequest ONrequest;
    private RESTRequest OFFrequest;

    // Use this for initialization
    public NormalItemActuator (string id, string state, string shortName, string icon, string On= "On", string Off = "Off") : base(id, state, shortName, icon){
        this.id = id;
        this.state = state;
        this.shortName = shortName;
        this.ONrequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + id, Settings.POST_ON);
        this.OFFrequest = RESTRequestFactory.createPOSTRequest(Settings.RestBaseURL + id, Settings.POST_OFF);
    }


    public override IEnumerator sendON()
    {
        Debug.Log("send ON request to " + id);
        return ONrequest.performAction();
    }

    public override IEnumerator sendOFF()
    {
        Debug.Log("send OFF request to " + id);
        return OFFrequest.performAction();
    }
}

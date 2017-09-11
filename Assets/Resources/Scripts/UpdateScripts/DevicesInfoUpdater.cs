using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevicesInfoUpdater : MonoBehaviour {

    private int updateCounter;
    private int updateCounterCoffee;
    private AllItemsGETRequest request;
    private CoffeemachineGETStatusRequest coffeeRequest;
    private MyoGetStatusRequest myoRequest;

    // Use this for initialization
    void Start () {
        request = RESTRequestFactory.createAllItemsGETRequest(Settings.RestBaseURL);
        myoRequest = RESTRequestFactory.createMyoGETRequest(Settings.URL_MyoArmband_Status);
        coffeeRequest = RESTRequestFactory.createCoffeemachineGETRequest(Settings.URL_CoffeeMachine_Status);
        updateCounter = 0;
        updateCounterCoffee = 0;
    }
	
	// Update is called once per frame
	void Update () {

        updateCounter++;
        updateCounterCoffee++;

        if (updateCounter >= Settings.requestUpdateNumber)
        {
            StartCoroutine(request.performAction());
            StartCoroutine(myoRequest.performAction());
            updateCounter = 0;
        }

        if (updateCounterCoffee >= Settings.coffeStatusUpdateNumber)
        {
            StartCoroutine(coffeeRequest.performAction());
            updateCounterCoffee = 0;
        }
    }
}

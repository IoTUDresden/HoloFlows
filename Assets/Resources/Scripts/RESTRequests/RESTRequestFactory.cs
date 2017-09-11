public class RESTRequestFactory {
    
    public static GETRequest createGETRequest(string requestURL)
    {
        return new GETRequest(requestURL);
    }
    public static AllItemsGETRequest createAllItemsGETRequest(string requestURL)
    {
        return new AllItemsGETRequest(requestURL);
    }
    public static CoffeemachineGETStatusRequest createCoffeemachineGETRequest(string requestURL)
    {
        return new CoffeemachineGETStatusRequest(requestURL);
    }
    public static RESTRequest createPOSTRequest(string requestURL, string requestBody)
    {
        return new POSTRequest(requestURL,requestBody);
    }

    public static MyoGetStatusRequest createMyoGETRequest(string requestURL)
    {
        return new MyoGetStatusRequest(requestURL);
    }
}

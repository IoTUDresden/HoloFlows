using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public interface RESTRequest
{
    string getStatus();
    IEnumerator performAction();
}

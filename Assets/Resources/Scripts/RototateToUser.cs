using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RototateToUser : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        try
        {
            this.transform.rotation = Quaternion.LookRotation(this.transform.position - Camera.current.transform.position);
        }
        catch(System.Exception e) { Debug.Log(e); }
    }
}

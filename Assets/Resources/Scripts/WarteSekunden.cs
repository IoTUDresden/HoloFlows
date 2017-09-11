using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarteSekunden : MonoBehaviour {

    public float Wartezeit;
     
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(SekundenWarten(Wartezeit));
	}
	
	public IEnumerator SekundenWarten(float Wartezeit)
    {
        yield return new WaitForSecondsRealtime(Wartezeit);
    }
}

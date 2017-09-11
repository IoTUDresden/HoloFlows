using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicActActInput : MonoBehaviour {
    
    private bool SourceToggleStatus;
    
    private bool TargetToggleStatus;

    [HideInInspector]
    public string Fenstername;

	// Use this for initialization
	void Start () {
        SourceToggleStatus = false;
        TargetToggleStatus = false;
        Fenstername = this.gameObject.name;
	}
	
	// Update is called once per frame
	public bool GetSourceStatus()
    {
        return this.SourceToggleStatus;
    }

    public void SetSourceStatus(bool SourceToggleStatus)
    {
        this.SourceToggleStatus = SourceToggleStatus;
    }
    public bool GetTargetStatus()
    {
        return this.TargetToggleStatus;
    }

    public void SetTargetStatus(bool TargetToggleStatus)
    {
        this.TargetToggleStatus = TargetToggleStatus;
    }

}

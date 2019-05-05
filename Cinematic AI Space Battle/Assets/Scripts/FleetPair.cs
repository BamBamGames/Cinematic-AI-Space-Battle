using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetPair{

    private Fleet _pair1;
    private Fleet _pair2;
    private bool _engaged = false;

    public FleetPair(Fleet first, Fleet second) {
        _pair1 = first;
        _pair2 = second;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Fleet GetFirstFleet() {
        return _pair1;
    }

    public Fleet GetSecondFleet() {
        return _pair2;
    }

    public bool IsEngaged() {
        return _engaged;
    }

    public void Engaged() {
        _engaged = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : Ship {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void SetShipStats() {
        _life = 300;
        _armomr = 200f;
        _fuel = 250;
        _primaryAmmo = 250f;
        _secondaryAmmo = 200f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : Ship {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void SetShipStats() {
        _life = 1000f;
        _armomr = 500f;
        _fuel = 1000f;
        _primaryAmmo = 500f;
        _secondaryAmmo = 500f;
        _attackPowerMultiplier = 3f;
        _fieldOfView = 70f;
    }

    public override void Fire(Transform enemy) {
        
    }

    public override void CleanUpBeforeDestroy() {
        throw new System.NotImplementedException();
    }
}

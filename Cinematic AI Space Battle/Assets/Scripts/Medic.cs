using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will be implented later for rts purposes
public class Medic : Ship {

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void SetShipStats() {
        _life = 300f;
        _armomr = 100;
        _fuel = 400f;
        _primaryAmmo = 100f;
        _secondaryAmmo = 0f;
        _attackPowerMultiplier = 1f;
        _fieldOfView = 20f;
    }

    public override void Fire(Transform enemy) {

    }

    public override void CleanUpBeforeDestroy() {
        throw new System.NotImplementedException();
    }
}

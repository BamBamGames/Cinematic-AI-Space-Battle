﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figther : Ship {

    private GameObject _leader;
    private GameObject _enemyTarget;
    public Fleet _fleet;
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool LeaderAlive() {
        if (_leader != null) {
            return true;
        } else {
            return false;
        }
    }

    public bool FightingEnemy() {
        if (_enemyTarget) {
            return true;
        } else {
            return false;
        }
    }

    public void SearchForEnemiesToFight() {
        
    }

    public void StopSearchForEnemy() {
    }

    public void SetFormationTransform(Transform formPos) {
        _targetToFollow = formPos;
    }

    protected override void SetShipStats() {
        _life = 100f;
        _armomr = 50f;
        _fuel = 100f;
        _primaryAmmo = 100f;
        _secondaryAmmo = 0f;
        _attackPowerMultiplier = 1.5f;
    }
}

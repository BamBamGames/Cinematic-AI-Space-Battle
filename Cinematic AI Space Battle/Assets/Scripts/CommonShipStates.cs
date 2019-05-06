using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecidingRole : State {
    public override void Enter() {

        Ship ship;

        if ((ship = owner.GetComponent<Ship>()) is Figther) {
            ship.GetComponent<StateMachine>().ChangeState(new FollowingLeader());
        }

        if ((ship = owner.GetComponent<Ship>()) is Leader) {
            ship.GetComponent<Evasive>().enabled = true;
            ship.GetComponent<Evasive>().ChooseDirectionOnce();
        }

        if ((ship = owner.GetComponent<Ship>()) is Commander) {

        }
    }

    public override void Exit() {
        Ship ship;

        if ((ship = owner.GetComponent<Ship>()) is Leader) {
            ship.GetComponent<Leader>().EndSearchForFleet();
        }
    }
    public override void Think() {

    }
}

public class Escaping : State {
    public override void Enter() {

        try {
            Debug.Log("Entered Escaping State" + owner);
            owner.GetComponent<Evasive>().enabled = true;
            owner.GetComponent<Escape>().enabled = true;
            owner.GetComponent<Escape>().weight = 0.1f;
            owner.GetComponent<Evasive>().weight = 0.8f;
            owner.GetComponent<Evasive>().radius = 20f;
            owner.GetComponent<Boid>().maxSpeed = owner.GetComponent<Ship>()._maxSpeed * 0.5f;
            owner.GetComponent<Evasive>().StartEvasiveManuevers(8);
            owner.GetComponent<Escape>().targetGameObject = owner.GetComponent<Ship>()._attackingFleet._fleetLeader.gameObject;
        } catch {
            Debug.Log("Owner is dead");
        }
    }
    public override void Exit() {
        try {
            owner.GetComponent<Escape>().enabled = false;
            owner.GetComponent<Evasive>().enabled = false;
        } catch {
            Debug.Log("Owner is dead");
        }
    }
    public override void Think() {

    }
}

public class SeekingHQ : State {
    public override void Enter() {
        owner.GetComponent<Seek>()._target = BattleFieldManager.Instance.GetOpponentHQ(owner.GetComponent<Ship>()._team).transform;
    }
    public override void Exit() {

    }
    public override void Think() {

    }
}

/// <summary>
/// Going to enemy base to kill self and be a hero
/// </summary>
public class SuicideMission : State {
    public override void Enter() {

    }
    public override void Exit() {

    }
    public override void Think() {

    }
}

/// <summary>
/// Refueling ammo, health and armor
/// </summary>
public class Refuelling : State {
    public override void Enter() {

    }
    public override void Exit() {

    }
    public override void Think() {

    }
}


/// <summary>
/// Trying to get away from enemy
/// </summary>
public class Evading : State {
    public override void Enter() {
        owner.GetComponent<Evasive>().enabled = true;
    }
    public override void Exit() {
        owner.GetComponent<Evasive>().enabled = false;
    }
    public override void Think() {

    }
}
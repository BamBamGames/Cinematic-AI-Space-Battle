
    
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
/// Following leader of the fleet
/// </summary>
public class FollowingLeader : State {
    public override void Enter() {
        Debug.Log("Entered Following leader State" + owner);
        //owner.GetComponent<Figther>().PickTargetBase();
        owner.GetComponent<Arrive>().targetGameObject = owner.GetComponent<Figther>()._targetToFollow.gameObject;
        owner.GetComponent<Arrive>().enabled = true;
    }
    public override void Exit() {
        Debug.Log("Exited Following leader State" + owner);
        //owner.GetComponent<Figther>().PickTargetBase();
        owner.GetComponent<Arrive>().enabled = false;
    }
    public override void Think() {
        if (!owner.GetComponent<Figther>().LeaderAlive()) {

        }
    }
}

public class Chasing : State {
    public override void Enter() {
        Debug.Log("Entered Chasing State" + owner);
        //owner.GetComponent<Figther>().PickTargetBase();
        owner.GetComponent<Seek>()._target = owner.GetComponent<Ship>()._targetToFollow.gameObject.transform;
        owner.GetComponent<Seek>().enabled = true;
    }
    public override void Exit() {
        Debug.Log("Exited Chasing State State" + owner);
        //owner.GetComponent<Figther>().PickTargetBase();
        owner.GetComponent<Seek>().enabled = false;
    }
    public override void Think() {
        
    }
}

public class ChasingFleet : State {
    public override void Enter() {
        Debug.Log("Entered Chasing Fleet State" + owner);
        //owner.GetComponent<Figther>().PickTargetBase();
        owner.GetComponent<Seek>().enabled = true;
        owner.GetComponent<Seek>()._target = owner.GetComponent<Ship>()._targetToFollow.gameObject.transform;  
    }
    public override void Exit() {
        Debug.Log("Exited Travelling State" + owner);
        //owner.GetComponent<Figther>().PickTargetBase();
        owner.GetComponent<Seek>().enabled = false;
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



/// <summary>
/// Coming back to leader
/// </summary>
public class Regrouping : State {
    public override void Enter() {

    }
    public override void Exit() {

    }
    public override void Think() {

    }
}

/// <summary>
/// Persuing and fighting enemy
/// </summary>
public class Engaging : State {
    public override void Enter() {
        //owner.GetComponent<Ship>().Targeting(); ;
    }
    public override void Exit() {

    }
    public override void Think() {

    }
}


/// <summary>
/// Coming back to base to refuel
/// </summary>
public class FallingBack : State {
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
/// Searching for new enemies to fight
/// </summary>
public class SearchingForBattle : State {
    public override void Enter() {
        owner.GetComponent<Figther>().SearchForEnemiesToFight();
    }
    public override void Exit() {
        owner.GetComponent<Figther>().StopSearchForEnemy();
    }
    public override void Think() {
        if (owner.GetComponent<Figther>().FightingEnemy()) {
            owner.GetComponent<StateMachine>().ChangeState(new Engaging());
        }
    }
}

public class SearchingForEnemyFleet : State {
    public override void Enter() {
        owner.GetComponent<Seek>().enabled = false;
        owner.GetComponent<Leader>().StartFleetSearch();
    }
    public override void Exit() {
        owner.GetComponent<Evasive>().enabled = false;
        owner.GetComponent<Leader>().EndSearchForFleet();
    }
    public override void Think() {
        if (owner.GetComponent<Leader>()._attackingFleet != null) {
            owner.GetComponent<Leader>().EndSearchForFleet();
        }
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
/*
public class Travelling : State {
    //public StateMachine owner;
    public override void Enter() {
        Debug.Log("Entered Travelling State" + owner);
        owner.GetComponent<FighterScript>().PickTargetBase();
        owner.GetComponent<Arrive>().enabled = true;
    }
    public override void Exit() {
        Debug.Log("Exited Travelling State" + owner);
        owner.GetComponent<Arrive>().enabled = false;
        owner.GetComponent<Boid>().velocity = Vector3.zero;
        //Turn off seek behaviour
    }
    public override void Think() {
        //If target is < 20 away from base transition to attacking state
        Vector3 target = owner.GetComponent<FighterScript>().target.position;
        if (Vector3.Distance(owner.transform.position, target) < 20) {
            owner.GetComponent<StateMachine>().ChangeState(new Attacking());
        }
    }
}

public class Refueling : State {
    public override void Enter() {
        owner.GetComponent<FighterScript>().AttemptRefuel();
    }
    public override void Exit() {
        owner.GetComponent<FighterScript>().StopRefueling();
    }
    public override void Think() {
        if (owner.GetComponent<FighterScript>().GetTiribium() >= 7) {
            owner.GetComponent<StateMachine>().ChangeState(new Travelling());
        }
    }
}

public class Attacking : State {
    //public StateMachine owner;
    public override void Enter() {
        owner.GetComponent<FighterScript>().StartShootingBase();
    }
    public override void Exit() {
        owner.GetComponent<FighterScript>().StopShootingBase();
    }
    public override void Think() {
        if (owner.GetComponent<FighterScript>().GetTiribium() <= 0) {
            owner.GetComponent<StateMachine>().ChangeState(new Retrieving());
        }
    }
}

public class Retrieving : State {
    //public StateMachine owner;
    public override void Enter() {
        owner.GetComponent<FighterScript>().SetParentTarget();
        owner.GetComponent<Arrive>().enabled = true;
    }
    public override void Exit() {
        owner.GetComponent<Arrive>().enabled = false;
        // finish seek
    }
    public override void Think() {
        //keep checking if at the base or not, if hit the base despawn
        Vector3 target = owner.GetComponent<FighterScript>().target.position;
        if (Vector3.Distance(owner.transform.position, target) < 1) {
            owner.GetComponent<StateMachine>().ChangeState(new Refueling());
        }
    }
}
*/

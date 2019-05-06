
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//States for fighter FSM, fighter specific states

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
            owner.GetComponent<Figther>()._stateMachine.ChangeState(new SearchingForBattle());
        }
    }
}

public class Chasing : State {
    public override void Enter() {
        Debug.Log("Entered Chasing State" + owner);
        //owner.GetComponent<Figther>().PickTargetBase();
        try {
            owner.GetComponent<Arrive>().targetGameObject = owner.GetComponent<Ship>()._targetToFollow.gameObject;
            owner.GetComponent<Arrive>().enabled = true;
        } catch {
            Debug.Log("owner is dead");
        }
    }
    public override void Exit() {
        Debug.Log("Exited Chasing State State" + owner);
        //owner.GetComponent<Figther>().PickTargetBase();
        owner.GetComponent<Seek>().enabled = false;
    }
    public override void Think() {
        
    }
}


/// <summary>
/// Searching for new enemies to fight
/// </summary>
public class SearchingForBattle : State {
    public override void Enter() {
        Debug.Log("Leader Alive");
        owner.GetComponent<Figther>().SearchForEnemiesToFight();
        if (!owner.GetComponent<Figther>().LeaderAlive()) {
            owner.GetComponent<Figther>()._stateMachine.ChangeState(new Escaping());
        }
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


///////////////Will be implemented later for RTS game purposes , skeleton established


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







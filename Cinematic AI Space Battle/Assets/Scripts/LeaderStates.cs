using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (owner != null) {
            owner.GetComponent<Seek>().enabled = false;
        }
    }
    public override void Think() {

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeFight : State {

    public override void Enter() {
        FleetPair pair = owner.GetComponent<FleetPair>();
        pair._escaping._fleetLeader._stateMachine.ChangeState(new Escaping());
        pair._chasing._fleetLeader._regulateSpeedDist = 30f;
        //pair._chasing._fleetLeader._stateMachine.ChangeState(new)
    }

    public override void Exit() {
        
    }
    public override void Think() {
        

        FleetPair pair = owner.GetComponent<FleetPair>();

        if (pair.GetFirstFleet()._fleetLeader == null) {
            foreach (Figther f in pair.GetFirstFleet()._fleetFighters) {
                f._stateMachine.ChangeState(new SearchingForBattle());
               
            }
        } else if(pair.GetSecondFleet()._fleetLeader == null) {
            pair.GetFirstFleet().ChaseFighters((pair.GetSecondFleet()._fleetFighters));
            pair.GetFirstFleet()._fleetLeader.GetRandomTarget(pair.GetSecondFleet()._fleetFighters);
        }

        if (pair.GetSecondFleet()._fleetLeader == null) {
            foreach (Figther f in pair.GetFirstFleet()._fleetFighters) {
                f._stateMachine.ChangeState(new SearchingForBattle());
            }
        } else if (pair.GetFirstFleet()._fleetLeader == null) {
            pair.GetSecondFleet().ChaseFighters((pair.GetFirstFleet()._fleetFighters));
            pair.GetSecondFleet()._fleetLeader.GetRandomTarget(pair.GetFirstFleet()._fleetFighters);
        }

        owner.GetComponent<StateMachine>().ChangeState(new Seperation());

        if (Vector3.Distance(pair._escaping.GetAveragePosition(), pair._chasing.GetAveragePosition()) > 30) {
            pair._escaping._fleetLeader.GetComponent<Boid>().maxSpeed -= 0.01f;
            
        } else {
            if (pair._escaping._fleetLeader.GetComponent<Boid>().maxSpeed < pair._escaping._fleetLeader._maxSpeed) {
                try {
                    pair._escaping._fleetLeader.GetComponent<Boid>().maxSpeed = pair._escaping.GetComponent<Ship>()._maxSpeed * 0.4f;
                } catch {
                }
            }
            pair._chasing._fleetLeader.GetComponent<Boid>().maxSpeed = pair._chasing._fleetLeader._maxSpeed;
        }
    }
}


public class ScrambleFight : State {
    public override void Enter() {
    }

    public override void Exit() {

    }
    public override void Think() {

    }
}

public class DogFight : State {
    public override void Enter() {
    }

    public override void Exit() {

    }
    public override void Think() {

    }
}

public class Seperation : State {
    public override void Enter() {
    }

    public override void Exit() {

    }
    public override void Think() {

    }
}

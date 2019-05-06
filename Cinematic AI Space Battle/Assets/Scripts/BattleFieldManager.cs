using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldManager : MonoBehaviour {

    public List<Leader> _greenLeaders = new List<Leader>();
    public List<Leader> _redLeaders = new List<Leader>();
    public Commander _greenCommander;
    public Commander _redCommander;
    public List<Fleet> _greenFleets = new List<Fleet>();
    public List<Fleet> _redFleets = new List<Fleet>();
    public float _distanceWeightDecision = 10f;
    public float _distanceBeforeFleetEngage = 80f;

    public GameObject _greenHQ;
    public GameObject _redHQ;

    public List<FleetPair> _fleetPairs = new List<FleetPair>();

    public static BattleFieldManager Instance;
	// Use this for initialization
	void Start () {
        Instance = this;
	}

    public Fleet FindNewFleetToJoin(Figther fighter) {
        Fleet newFleet = null;
        float _totalHealth = float.MaxValue;

        if (fighter._team == Team.red) {
            foreach (Fleet f in _redFleets) {
                //Checks if fleet is not in battle
                if (f._totalHealth < _totalHealth) {
                    newFleet = f;
                }
            }
        }

        return newFleet;
    }

    public Fleet ChooseNewFleet(Leader choosingLeader, bool battleState) {

        Fleet optimalFleet = null;
        float sum = float.MaxValue;

        if (choosingLeader._team == Team.red) {

            foreach (Fleet f in _greenFleets) {
                //Checks if fleet is not in battle
                if (f._inBattle == battleState) {
                    if (f.GetTotalHealth() + (Vector3.Distance(choosingLeader._fleet.GetAveragePosition(), f.GetAveragePosition()) / 100f) < sum) {
                        sum = (f.GetTotalHealth() + (Vector3.Distance(choosingLeader._fleet.GetAveragePosition(), f.GetAveragePosition()) / 100f));
                        optimalFleet = f;
                    }
                }
            }

        } else {

            foreach (Fleet f in _redFleets) {
                //Checks if fleet is not in battle
                if (f._inBattle == battleState) {
                    if (f.GetTotalHealth() + (Vector3.Distance(choosingLeader._fleet.GetAveragePosition(), f.GetAveragePosition()) / 100f) < sum) {
                        sum = (f.GetTotalHealth() + (Vector3.Distance(choosingLeader._fleet.GetAveragePosition(), f.GetAveragePosition()) / 100f));
                        optimalFleet = f;
                    }
                }
            }

        }

        //Adds a fleet pair for the battle manager to store
        if (optimalFleet != null) {
            Debug.Log("Added fleet pair");
            _fleetPairs.Add(FleetPair.Create(choosingLeader._fleet, optimalFleet));
        } else {
            Debug.Log("Could not add fleets to pair");
        }

        if (optimalFleet != null) {
            optimalFleet._inBattle = true;
            //choosingLeader._fleet._inBattle = true;
        }

        return optimalFleet;
    }
    

    public void AddCommander(Commander commander, Team team) {
        if (team == Team.red) {
            _redCommander = commander;
        } else {
            _greenCommander = commander;
        }
    }

    public void AddLeader(Leader leader, Team team) {
        if (team == Team.red) {
            _redLeaders.Add(leader);
        } else {
            _greenLeaders.Add(leader);
        }
    }

    public void AddFleet(Fleet fleet, Team team) {
        if (team == Team.red) {
            _redFleets.Add(fleet);
        } else {
            _greenFleets.Add(fleet);
        }
    }

    public GameObject GetOpponentHQ(Team team) {

        if (team == Team.red) {
            return _greenHQ;
        } else {
            return _redHQ;
        }
    }
}

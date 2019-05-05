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
    public float _distanceBeforeFleetEngage = 50f;

    public GameObject _greenHQ;
    public GameObject _redHQ;

    private List<FleetPair> _fleetPairs = new List<FleetPair>();

    public static BattleFieldManager Instance;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        CheckFleetPairState();
	}

    private void CheckFleetPairState() {
        foreach (FleetPair pair in _fleetPairs) {
            if (!pair.IsEngaged()) {
                if (Vector3.Distance(pair.GetFirstFleet().GetAveragePosition(), pair.GetSecondFleet().GetAveragePosition()) < _distanceBeforeFleetEngage) {
                    pair.Engaged();
                    InitiateEngagementBetweenFleets(pair);
                }
            }
        }
    }

    private void InitiateEngagementBetweenFleets(FleetPair pair) {
        float fOneFightersCount = pair.GetFirstFleet()._fleetFighters.Count;
        float fTwoFightersCount = pair.GetSecondFleet()._fleetFighters.Count;

        float minFighers = Mathf.Min(fOneFightersCount, fTwoFightersCount);

        //for(int i = 0; i <)
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
        if (choosingLeader._fleet != null && choosingLeader._attackingFleet != null) {
            _fleetPairs.Add(new FleetPair(choosingLeader._fleet, choosingLeader._attackingFleet));
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

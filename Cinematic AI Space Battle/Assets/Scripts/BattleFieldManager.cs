using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldManager : MonoBehaviour {

    private List<Leader> _greenLeaders = new List<Leader>();
    private List<Leader> _redLeaders = new List<Leader>();
    private Commander _greenCommander;
    private Commander _redCommander;
    private List<Fleet> _greenFleets = new List<Fleet>();
    private List<Fleet> _redFleets = new List<Fleet>();
    public float _distanceWeightDecision = 10f;

    public GameObject _greenHQ;
    public GameObject _redHQ;

    public static BattleFieldManager Instance;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
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

    public void GetOpponentHQ(Team team) {
    }
}

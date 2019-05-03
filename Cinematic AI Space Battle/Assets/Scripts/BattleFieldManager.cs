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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public Fleet ChooseNewFleet(Leader choosingLeader) {

        Fleet fleet;
        float _lowestHealth;
        float _distance;


        if (choosingLeader._team == Team.red) {

            foreach (Fleet f in _greenFleets) {
                //Checks if fleet is not in battle
                if (!f._inBattle) {
                }
            }

        } else {


        }

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour {

    private List<GameObject> _allShips = new List<GameObject>();
    private Leader _fleetLeader;
    private List<Figther> _fleetFighters = new List<Figther>();
    public bool _inBattle = false;
    private Vector3 _averagePosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_allShips.Count > 0) {
            _averagePosition = VectorMaths.GetMeanVector(_allShips)
        }
	}

    public Fleet AddFighters(List<Figther> fighters) {
        _fleetFighters = new List<Figther>(fighters);

        foreach (Figther f in fighters) {
            _allShips.Add(f.gameObject);
        }

        return this;
    }

    public Fleet AddLeader(Leader leader) {

        _fleetLeader = leader;
        _allShips.Add(leader.gameObject);
        return this;
    }

    public Leader GetLeader() {
        return _fleetLeader;
    }

}

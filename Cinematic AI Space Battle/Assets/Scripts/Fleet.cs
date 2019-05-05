using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour {

    public List<GameObject> _allShips = new List<GameObject>();
    public Leader _fleetLeader;
    public List<Figther> _fleetFighters = new List<Figther>();
    public bool _inBattle = false;
    public bool _fleeing = false;
    public Vector3 _averagePosition;
    public float _totalHealth;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (_allShips.Count > 0) {
            _averagePosition = VectorMaths.GetMeanVector(_allShips);
            transform.position = _averagePosition;
        }

        UpdateTotalHealth();
	}

    public void RemoveShip(GameObject go) {
        _allShips.Remove(go);
        if (go.GetComponent<Ship>() is Figther) {
            //_fleetLeader = null;
            _fleetFighters.Remove(go.GetComponent<Figther>());
        }

        if (go.GetComponent<Ship>() is Leader) {

        }
    }

    private void UpdateTotalHealth() {
        _totalHealth = 0;

        foreach (GameObject go in _allShips) {
            _totalHealth += go.GetComponent<Ship>()._life;
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

    public float GetTotalHealth() {
        return _totalHealth;
    }

    public Vector3 GetAveragePosition() {
        return _averagePosition;
    }

    public Transform GetAveragePositionAsTransform() {
        return transform;
    }

    public List<GameObject> GetAllShips() {
        return _allShips;
    }

    public void AddFleetCamerasToManager() {
        foreach (GameObject g in _allShips) {
            g.GetComponent<Ship>().AddCameraToManager(); 
        }
    }

    public void StartFleeing() {
        _fleetLeader._stateMachine.ChangeState(new Escaping());
    }

}

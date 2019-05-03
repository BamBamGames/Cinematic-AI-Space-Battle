using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetSpawner : MonoBehaviour {

    public Team team;
    string resourceFolder = "";
	// Use this for initialization
	void Start () {
        if (team == Team.green) {
            resourceFolder = "TeamGreen";
        }

        if (team == Team.red) {
            resourceFolder = "TeamRed";
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            
            SpawnFleet(transform.position);
        }
	}

    bool _refreshCameras = false;

    private void SpawnFleet(Vector3 pos) {
        
        GameObject[] leaders = Resources.LoadAll<GameObject>(resourceFolder+"/Leaders");

        GameObject leaderOb = Instantiate(leaders[Random.Range(0, leaders.Length)]);
        Leader leader = leaderOb.GetComponent<Leader>();
        leader.SetTeam(team);

        leader.transform.position = pos;

        leader.transform.localRotation = transform.localRotation;

        leader.SpawnFleet(5);

        _refreshCameras = true;

        
    }

    private void LateUpdate() {
        if (_refreshCameras) {
            CameraManager.Instance.GetAllCameras();
            _refreshCameras = false;
        }
    }
}

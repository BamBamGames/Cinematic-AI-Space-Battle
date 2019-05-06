using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to manage a base
public class HQScript : MonoBehaviour {

    public float _resources;
    public float _health;
    private List<GameObject> _spawnPoints = new List<GameObject>();
    private List<Camera> _defendingCameras = new List<Camera>();
    private List<GameObject> _guns = new List<GameObject>();
    private Camera _camera;
    private FleetSpawner _fleetSpawner;
    IEnumerator _spawning;
    public float _trySpawnInterval = 5f;
    public float _minFleetResouces = 5f;
    public float _minMedicResource = 5f;
    public Team team;

    public float _totalPoints = 0;
	// Use this for initialization
	void Start () {
        GetSpawnPoints();
        GetDefendingCameras();
        GetGuns();
        _fleetSpawner = GetComponent<FleetSpawner>();
        _fleetSpawner.transform.localRotation = transform.localRotation;
        _fleetSpawner.team = team;
        _spawning = Spawning();
        StartCoroutine(_spawning);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Spawning() {
        while (true) {

            yield return new WaitForSeconds(_trySpawnInterval);

            if (_resources > _minFleetResouces) {
                _resources -= 5f;
                _fleetSpawner.SpawnFleet(_spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position, this);
            }
        }
    }

    public void AddPoints(float points) {
        _totalPoints += points;
    }

    private void GetMainCamera() {
        foreach (Transform t in transform) {
            if (t.name.Contains("MainCam")) {
                _camera = t.GetComponent<Camera>();
            }
        }
    }

    private void GetSpawnPoints() {
        foreach (Transform t in transform) {
            if (t.name.Contains("Spawn")) {
                _spawnPoints.Add(t.gameObject);
            }
        }
    }

    private void GetDefendingCameras() {
        foreach (Transform t in transform) {
            if (t.name.Contains("DefendCam")) {
                _defendingCameras.Add(t.GetComponent<Camera>());
            }
        }
    }

    private void GetGuns() {
        foreach (Transform t in transform) {
            if (t.name.Contains("Gun")) {
                _guns.Add(t.gameObject);
            }
        }
    }
}

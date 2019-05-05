using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQScript : MonoBehaviour {

    public float _resources;
    public float _health;
    private List<GameObject> _spawnPoints = new List<GameObject>();
    private List<Camera> _defendingCameras = new List<Camera>();
    private List<GameObject> _guns = new List<GameObject>();
    private Camera _camera;
	// Use this for initialization
	void Start () {
        GetSpawnPoints();
        GetDefendingCameras();
        GetGuns();


	}
	
	// Update is called once per frame
	void Update () {
		
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

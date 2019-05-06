using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraManager : MonoBehaviour {

    public List<Camera> _cameras = new List<Camera>();
    public List<Camera> _battleCameras = new List<Camera>();
    public Camera _enabled = null;
    public bool _startFromPResetCamera = false;
    public filmTypes _filmType = filmTypes.firstCamera;
    public int _randomInterval = 10;
    IEnumerator _cameraCouroutine;

    public static CameraManager Instance;

    public enum filmTypes {
        firstCamera,
        Random,
        DynamicAction,
        DynamicActionAndFirstPerson,
        FirstPerson
    }
	// Use this for initialization
	void Start () {
        _cameraCouroutine = RandomCameras();
        Instance = this;

        if (Camera.allCameras.Length > 0) {
            GetAllCameras();
        }
        if (_startFromPResetCamera) {
           
            DisableAllButEnalbed();
        }

        if (_filmType == filmTypes.Random) {

            if (_cameras.Count > 0) {
                StartCoroutine(_cameraCouroutine);
            } else {
                Debug.Log("no cameras found");
            }
        }
	}

    IEnumerator RandomCameras() {
        while (true) {

            yield return new WaitForSeconds(_randomInterval);
            if (!breakingFromCorouting) {
                SwitchRandomCamera();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    bool breakingFromCorouting = false;

    public void GetAllCameras() {
        Debug.Log("Got all camera");
        breakingFromCorouting = true;
        StopCoroutine(_cameraCouroutine);
        breakingFromCorouting = false;
        //_cameras.Clear();
        Camera[] cams = Camera.allCameras;
        Debug.Log(cams.Length);

        foreach (Camera cam in cams) {
            _cameras.Add(cam);
        }
        DisableAllButEnalbed();
        StartCoroutine(_cameraCouroutine);
    }

    private void DisableAllButEnalbed() {
        foreach (Camera cam in _cameras) {
            if (!cam.Equals(_enabled)) {
                
                cam.gameObject.SetActive(false);
            }
        }

        foreach (Camera cam in _battleCameras) {
            if (!cam.Equals(_enabled)) {

                cam.gameObject.SetActive(false);
            }
        }
    }

    public void SwitchRandomCamera() {
        try {
            _enabled.gameObject.SetActive(false);
        } catch {
        }

        if (Random.Range(0, 8) < 6) {
            _enabled = _cameras[Random.Range(0, _cameras.Count)];
            _enabled.gameObject.SetActive(true);
        } else if(_battleCameras.Count > 0){
            _enabled = _battleCameras[Random.Range(0, _battleCameras.Count)];
            _enabled.gameObject.SetActive(true);
        } else {
            _enabled = _cameras[Random.Range(0, _cameras.Count)];
            _enabled.gameObject.SetActive(true);
        }
        DisableAllButEnalbed();
    }

    public void RemoveCamera(Camera cam) {
        //StopCoroutine(_cameraCouroutine);
        _cameras.Remove(cam);
        //StartCoroutine(_cameraCouroutine);
    }

    public void AddCamera(Camera cam) {
        _cameras.Add(cam);
        DisableAllButEnalbed();
    }

    public void AddBattleCamera(Camera cam) {
        _battleCameras.Add(cam);
        DisableAllButEnalbed();
    }

    public void RemoveBattleCamera(Camera cam) {
        _battleCameras.Remove(cam);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public List<Camera> _cameras = new List<Camera>();
    public Camera _enabled;
    public filmTypes _filmType = filmTypes.firstCamera;
    public int _randomInterval = 10;

    public enum filmTypes {
        firstCamera,
        Random,
        DynamicAction,
        DynamicActionAndFirstPerson,
        FirstPerson
    }
	// Use this for initialization
	void Start () {
        GetAllCameras();
        if (_enabled) {
           
            DisableAllButEnalbed();
        }

        if (_filmType == filmTypes.Random) {
            StartCoroutine(RandomCameras());
        }
	}

    IEnumerator RandomCameras() {
        while (true) {

            yield return new WaitForSeconds(_randomInterval);
            SwitchRandomCamera();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GetAllCameras() {
        Camera[] cams = Camera.allCameras;

        foreach (Camera cam in cams) {
            _cameras.Add(cam);
        }
    }

    private void DisableAllButEnalbed() {
        foreach (Camera cam in _cameras) {
            if (!cam.Equals(_enabled)) {
                cam.gameObject.SetActive(false);
            }
        }
    }

    public void SwitchRandomCamera() {
        _enabled.gameObject.SetActive(false);
        _enabled = _cameras[Random.Range(0, _cameras.Count)];
        _enabled.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneDirector : MonoBehaviour {

    public static FirstSceneDirector Instance;

    public GameObject _camera;
    public GameObject _directionlight;
    public GameObject _earthSparkEffects;
    public GameObject _HQ;
    public GameObject _earth;
    //public GameObject _lightningEffects;

    public float _lightRotationInterval = 0.01f;
    public float _lightRotationIntensity = 0.5f;
    public float _cameraRotationInterval = 0;

    public float _waitBeforeShipAfterLightining = 15f;
    public float _waitBeforeCameraComesToShip = 10f;

    public float _rotateLightBy = 250;
    public float _rotateCameraBy = 15;

    public float _timePassed = 0;

    public AudioClip _spottedEnemy;
    public AudioClip _alienVoice;

    public AudioSource audioSource;

    IEnumerator _earthLight;
    IEnumerator _cameraRotation;
    IEnumerator _enableLightning;
	// Use this for initialization
	void Start () {

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = _spottedEnemy;
        audioSource.volume = 0.5f;
        

        Instance = this;
        _earthLight = LightRotation();
        StartCoroutine(_earthLight);
        _cameraRotation = RotateCamera();
        _enableLightning = EnableLightning();
        StartCoroutine(CountTime());

	}

    IEnumerator CountTime() {
        while(true){
            yield return new WaitForSeconds(1);
            _timePassed++;
        }
    }


    public bool _endedLightRotation = false;
    public bool _endedCameraRotation = false;
	// Update is called once per frame
	void Update () {
        if (!_endedLightRotation && _rotateLightBy <= 0) {
            StopCoroutine(_earthLight);
            StartCoroutine(_cameraRotation);
            StartCoroutine(_enableLightning);
            _endedLightRotation = true;
        }

        if (!_endedCameraRotation && _rotateCameraBy <= 0) {
            audioSource.Play(0);
            Debug.Log("EndedCameraRotation");
            StopCoroutine(_cameraRotation);
          
            _endedCameraRotation = true;
        }

        if (_cameraMoving) {
            StopCoroutine(_enableLightning);
            _cameraMoving = false;
        }

        if (!_sceneEnded && _timePassed > 76) {
            _sceneEnded = true;
        }
	}

    private bool _sceneEnded = false;

    private void EnableShipArrival() {
        _HQ.GetComponent<FollowPath>().enabled = true;
    }

    private void EnableCameraPath() {
        _camera.GetComponent<FollowPath>().enabled = true;
    }

    public bool _cameraMoving = false;
    IEnumerator EnableLightning() {
        while (true) {
            _earthSparkEffects.SetActive(true);
            yield return new WaitForSeconds(_waitBeforeShipAfterLightining);
            EnableShipArrival();
            yield return new WaitForSeconds(_waitBeforeCameraComesToShip);
            EnableCameraPath();
            _cameraMoving = true;
            //StopAllCoroutines();
        }
    }

    IEnumerator RotateCamera() {
        while (true) {
            _rotateCameraBy -= _cameraRotationInterval;
            _camera.transform.Rotate(Vector3.up, -_cameraRotationInterval);
            yield return new WaitForSeconds(_lightRotationInterval);
        }
    }

    IEnumerator LightRotation() {
        while (true) {
            _rotateLightBy -= _lightRotationIntensity;
            _directionlight.transform.Rotate(Vector3.up, _lightRotationIntensity);
                _earth.transform.Rotate(Vector3.up, _cameraRotationInterval);
            yield return new WaitForSeconds(_lightRotationInterval);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondSceneDirector : MonoBehaviour {

    public static SecondSceneDirector Instance;

    public List<GameObject> _fighters;
    public GameObject _shipLight;
    public GameObject _takeOffEffects;
    public GameObject _takeOffLight;
    public GameObject _hqShip;

    IEnumerator _shipTakeOff;

    public float _lightInterval;
	// Use this for initialization
	void Start () {
        Instance = this;
        StartCoroutine(MoveShipLight());
        _shipTakeOff = TakeOff();
        foreach (GameObject g in _fighters) {
            g.GetComponent<Arrive>().enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (_shipTookOff) {
            StopCoroutine(_shipTakeOff);
            _shipTookOff = false;
        }
	}

    public void TurnOnTakeOffEffects() {
        _takeOffEffects.SetActive(true);
        StartCoroutine(_shipTakeOff);
       
    }

    public bool _shipTookOff = false;

    IEnumerator TakeOff() {
        while (true) {
            yield return new WaitForSeconds(5);
            _hqShip.GetComponent<FollowPath>().enabled = true;
            _shipTookOff = true;
        }
    }

    IEnumerator MoveShipLight() {
        while (true) {

            if (_shipLight.transform.localPosition.z < 14) {
                _shipLight.transform.localPosition = new Vector3(_shipLight.transform.localPosition.x, _shipLight.transform.localPosition.y, _shipLight.transform.localPosition.z + 0.2f);
            }
            yield return new WaitForSeconds(_lightInterval);
        }
    }
}

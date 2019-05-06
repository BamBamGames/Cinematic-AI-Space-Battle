using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLoader : MonoBehaviour {

    public int shipLoadCount = 0;
    public int _maxShipLoadCount = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        shipLoadCount++;
            if(shipLoadCount >= _maxShipLoadCount) {
            SecondSceneDirector.Instance.TurnOnTakeOffEffects();
            }
            Destroy(other.gameObject);
            
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour {

    private ShipCamera _cameraFollowing;
    private ShipCamera _firstPersonCamera;

    public float _life;
    public float _armomr;
    public float _fuel;
    public float _primaryAmmo;
    public float _secondaryAmmo;

    public StateMachine _stateMachine;

    // Use this for initialization
    void Start () {
        SetShipStats();
        SetFpsCamera();
        SetThirdPersonCamera();
        _stateMachine = gameObject.AddComponent<StateMachine>();
        GetComponent<StateMachine>().ChangeState(new DecidingRole());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetThirdPersonCamera() {
        Vector3 cameraPosition = new Vector3(0,2,-7);
        ShipCamera cam = ShipCamera.Create(cameraPosition, transform);
    }

    private void SetFpsCamera() {
        foreach (Transform c in transform) {
            if (c.gameObject.name == "fpsCameraPoint") {
                Vector3 cameraPosition = c.transform.position;
                ShipCamera cam = ShipCamera.Create(cameraPosition, transform);
            }
        }
    }

    /// <summary>
    /// Set life, armor, fuel, primaryAmmo and secondaryAmmo here
    /// </summary>
    protected abstract void SetShipStats();
}

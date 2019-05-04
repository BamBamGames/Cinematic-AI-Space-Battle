using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour {

    private ShipCamera _cameraFollowing;
    private ShipCamera _firstPersonCamera;

    public List<GameObject> _blasters = new List<GameObject>();

    public float _life;
    public float _armomr;
    public float _fuel;
    public float _primaryAmmo;
    public float _secondaryAmmo;
    public float _attackPowerMultiplier;
    public float _fieldOfView;

    public float _maxSpeed = 10f;


    public Transform _targetToFollow;

    public Fleet _fleet;
    public Fleet _attackingFleet;


    public StateMachine _stateMachine;

    public Team _team;

    // Use this for initialization
    public void Start() {
        SetShipStats();
        SetFpsCamera();
        SetThirdPersonCamera();
        _stateMachine = gameObject.AddComponent<StateMachine>();
        GetComponent<StateMachine>().ChangeState(new DecidingRole());
    }

    // Update is called once per frame
    public void Update() {
        if (_attackingFleet != null) {
            Targeting();
        }
    }

    private void SetThirdPersonCamera() {
        Vector3 cameraPosition = new Vector3(0, 2, -7);
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

    public void SetTeam(Team team) {
        _team = team;
    }

    public void Targeting() {

        if (_attackingFleet != null) {

            foreach (GameObject enemy in _attackingFleet.GetAllShips()) {

                if (Vector3.Distance(enemy.transform.position, transform.position) < 40) {

                    Debug.Log("Enemy Closer than 40");
                    Vector3 targetDir = enemy.transform.position - transform.position;
                    float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

                    if (angleToPlayer >= -_fieldOfView && angleToPlayer <= _fieldOfView) {

                        Debug.Log("Player in sight!");
                        Fire();
                    }
                }

            }
        }
    }

    public void ShootBlaster(float multiplier) {

    }

    public void GetBlasters() {
        foreach (Transform child in transform) {
            if (child.name.Contains("Gun")) {
                _blasters.Add(child.gameObject);
            }
        }
    }

    public abstract void Fire();
}

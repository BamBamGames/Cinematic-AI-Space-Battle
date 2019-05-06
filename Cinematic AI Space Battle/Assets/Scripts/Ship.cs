using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour {

    private bool _addedCamerasToManager = false;
    public ShipCamera _cameraFollowing;
    public ShipCamera _firstPersonCamera;

    public List<GameObject> _blasters = new List<GameObject>();

    public float _life;
    public float _armomr;
    public float _fuel;
    public float _primaryAmmo;
    public float _secondaryAmmo;
    public float _attackPowerMultiplier;
    public float _fieldOfView;
    protected bool _fighting = false;

    public float _maxSpeed = 10f;

    public Transform _targetToFollow;

    public Fleet _fleet;
    public Fleet _attackingFleet;


    public StateMachine _stateMachine;

    public Team _team;

    public AudioSource _engineSource;
    public AudioSource _explosionSource;
    public AudioSource _blasterSource;

    public HQScript _hq;
    protected float _blasterForce;
    protected float _blasterDamage;
    // Use this for initialization
    public void Start() {
        _blasterForce = BattleSettings.Instance._bulletSpeed;
        _blasterDamage = BattleSettings.Instance._blasterDamage;
        if (GetComponent<AudioSource>() != null) {
            Destroy(GetComponent<AudioSource>());
        }

        Escape escape = gameObject.AddComponent<Escape>();
        escape.enabled = false;


        _engineSource = gameObject.AddComponent<AudioSource>();
        _engineSource.loop = true;
        _engineSource.clip = AudiSourceManager.Instance._engine;
        _engineSource.spatialBlend = 1f;
        _engineSource.volume = 0.4f;
        _engineSource.rolloffMode = AudioRolloffMode.Linear;
        _engineSource.minDistance = 1f;
        _engineSource.maxDistance = 15f;
        _engineSource.Play(0);

        _explosionSource = gameObject.AddComponent<AudioSource>();
        _explosionSource.loop = false;
        _explosionSource.clip = AudiSourceManager.Instance._explosion;
        _explosionSource.spatialBlend = 1f;
        _explosionSource.volume = 0.8f;
        _explosionSource.rolloffMode = AudioRolloffMode.Linear;
        _explosionSource.minDistance = 1f;
        _explosionSource.maxDistance = 400f;

        _blasterSource = gameObject.AddComponent<AudioSource>();
        _blasterSource.loop = false;
        _blasterSource.clip = AudiSourceManager.Instance._blaster;
        _blasterSource.spatialBlend = 1f;
        _blasterSource.volume = 0.4f;
        _blasterSource.rolloffMode = AudioRolloffMode.Linear;
        _blasterSource.minDistance = 1f;
        _blasterSource.maxDistance = 20f;

        GetBlasters();
        SetShipStats();
        SetFpsCamera();
        SetThirdPersonCamera();
        _stateMachine = gameObject.AddComponent<StateMachine>();
        GetComponent<StateMachine>().ChangeState(new DecidingRole());
        _targetingRoutine = Targeting();
        StartCoroutine(_targetingRoutine);

        SetSpeeds();
    }

    private void SetSpeeds() {
        Boid boid = GetComponent<Boid>();
        if (this is Figther) {
            _maxSpeed = BattleSettings.Instance._fighterSpeed;
            boid.maxSpeed = _maxSpeed;
        }
        if (this is Medic) {
            _maxSpeed = BattleSettings.Instance._medicSpeed;
           boid.maxSpeed = _maxSpeed;
        }
        if (this is Commander) {
            _maxSpeed = BattleSettings.Instance._commanderSpeed;
            boid.maxSpeed = _maxSpeed;
        }
        if (this is Leader) {
            _maxSpeed = BattleSettings.Instance._leaderSpeed;
            boid.maxSpeed = _maxSpeed;
        }
    }

    public void OnDisable() {
        StopAllCoroutines();
    }

    // Update is called once per frame
    public void Update() {

        if (!_addedCamerasToManager && _cameraFollowing != null) {
            AddCameraToManager();
            _addedCamerasToManager = true;
        }

        if (_attackingFleet != null) {
            Targeting();
        }

        CheckLife();

        
    }

    private bool _smoking = false;
    private void ShipSmoke() {
        GameObject smoke = Instantiate(VFXManager.Instance._shipSmoke);
        smoke.transform.parent = transform;
        smoke.transform.localPosition = new Vector3(0, 0, 0);

    }

    private bool _onFire = false;
    private void ShipFire() {
        GameObject smoke = Instantiate(VFXManager.Instance._shipFire);
        smoke.transform.parent = transform;
        smoke.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void ShipExplosion() {
        GameObject smoke = Instantiate(VFXManager.Instance._shipExplosion);
        //smoke.transform.parent = transform;
        smoke.transform.position = transform.position;
        _explosionSource.Play(0);
    }


    private void CheckLife() {

        if (!_smoking && _life <= 60) {
            _smoking = true;
            ShipSmoke();
        }

        if (!_onFire && _life <= 30) {
            _onFire = true;
            ShipFire();
        }

        if (_life <= 0) {
            CleanUpBeforeDestroy();
            _fleet.RemoveShip(gameObject);

            CameraManager.Instance.RemoveCamera(_cameraFollowing.GetComponent<Camera>());
            if (_cameraFollowing.isActiveAndEnabled) {
                _cameraFollowing.GetComponent<ShipCamera>().AutoDestroy();
            } else {
                Destroy(_cameraFollowing.GetComponent<ShipCamera>());
            }
            if (_firstPersonCamera != null) {
                CameraManager.Instance._cameras.Remove(_firstPersonCamera.GetComponent<Camera>());
                Destroy(_firstPersonCamera.gameObject);
            }

            ShipExplosion();

            Destroy(gameObject);
        }
    }

    public abstract void CleanUpBeforeDestroy();

    private void SetThirdPersonCamera() {
        Vector3 cameraPosition = new Vector3(0, 2, -7);
        ShipCamera cam = ShipCamera.Create(cameraPosition, transform);
        _cameraFollowing = cam;
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

    public void SetTeamandHQ(Team team,HQScript hq) {
        _team = team;
        _hq = hq;
    }

    public float _TargetingInterval = 0.1f;
    IEnumerator _targetingRoutine;

    IEnumerator Targeting() {

        while (true) {
            if (_attackingFleet != null) {

                foreach (GameObject enemy in _attackingFleet.GetAllShips()) {

                    if (Vector3.Distance(enemy.transform.position, transform.position) < 50) {

                        //Debug.Log("Enemy Closer than 40");
                        Vector3 targetDir = enemy.transform.position - transform.position;
                        float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

                        if (angleToPlayer >= -_fieldOfView && angleToPlayer <= _fieldOfView) {

                            //Debug.Log("Player in sight!");
                            Fire(enemy.transform);
                            _blasterSource.Play(0);
                            break;
                        }
                    }

                }
            }

            yield return new WaitForSeconds(_TargetingInterval);
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (_team == Team.red && other.tag == "GreenBullet") {
            Debug.Log("HIT");
            _life -= other.GetComponent<Bullet>().GetDamage();
            other.gameObject.SetActive(false);
            GameObject spark = Instantiate(VFXManager.Instance._sparksHit);
            //smoke.transform.parent = transform;
            spark.transform.position = transform.position;
        } else if (_team == Team.green && other.tag == "RedBullet") {
            Debug.Log("HIT");
            _life -= other.GetComponent<Bullet>().GetDamage();
            other.gameObject.SetActive(false);
            GameObject spark = Instantiate(VFXManager.Instance._sparksHit);
            //smoke.transform.parent = transform;
            spark.transform.position = transform.position;

        }
    }

    

    public void ShootBlaster(float multiplier, Transform enemy) {
        Rigidbody rb;
        Debug.Log("Blaster shot");
        foreach (GameObject go in _blasters) {

            GameObject bullet = AmmoPool.Instance.GetBullet();
            bullet.SetActive(true);

            Bullet b = bullet.GetComponent<Bullet>();
            b.SetDamage(_blasterDamage * multiplier);

            if (_team == Team.red) {
                b.GetComponent<TrailRenderer>().startColor = Color.red;
                b.GetComponent<TrailRenderer>().endColor = Color.red;
                b.GetComponent<Renderer>().material.color = Color.red;
                b.tag = "RedBullet";
            } else {
                b.GetComponent<TrailRenderer>().startColor = Color.green;
                b.GetComponent<TrailRenderer>().endColor = Color.green;
                b.GetComponent<Renderer>().material.color = Color.green;
                b.tag = "GreenBullet";
            }

            bullet.transform.position = go.transform.position;
            bullet.transform.localRotation = go.transform.localRotation;
            rb = bullet.GetComponent<Rigidbody>();

            Vector3 dir = enemy.position - transform.position;
            rb.AddForce(dir * _blasterForce * multiplier);

        }
    }

    public void GetBlasters() {
        foreach (Transform child in transform) {
            if (child.name.Contains("Gun")) {
                _blasters.Add(child.gameObject);
            }
        }
    }

    public void AddCameraToManager() {
        Debug.Log("Added camera to spawner" + gameObject);
        CameraManager.Instance.AddCamera(_cameraFollowing.GetComponent<Camera>());
    }

    public abstract void Fire(Transform enemy);
}

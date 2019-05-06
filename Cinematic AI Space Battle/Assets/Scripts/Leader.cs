using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to manage a leader who extends Ship class
public class Leader : Ship {

    public List<Figther> _followers = new List<Figther>();
    public float _avgDistanceBetweenFollowers;
    private bool _engagedInBattle;
    private bool _readyToFight = false;
    public bool _regulateSpeed = true;
    public float _regulateSpeedDist = 15f;

    IEnumerator _searchForFleet;

    private void Start() {
        _searchForFleet = SearchForFleet();
        base.Start();
       
    }
    // Update is called once per frame
    void Update () {
        base.Update();
        if (_attackingFleet != null) {
            UpdateChasingFleetPos();
        }
        CalculateDistanceBetweenFollowers();
        if (_regulateSpeed) {
            RegulateSpeed();
        } else {
            GetComponent<Boid>().maxSpeed = _maxSpeed;
        }

        if (!_readyToFight) {
            CheckDistanceFromHQ();
        }
	}

    private void CheckDistanceFromHQ() {
        if (_hq != null) {
            if (Vector3.Distance(transform.position, _hq.transform.position) > 200) {
                _readyToFight = true;
                _stateMachine.ChangeState(new SearchingForEnemyFleet());
            }
        } else {
            _stateMachine.ChangeState(new SearchingForEnemyFleet());
        }
    }

    private void CalculateDistanceBetweenFollowers() {
        float sum = 0;
        foreach (Figther f in _followers) {
            sum += Vector3.Distance(transform.position, f.transform.position);
        }

        _avgDistanceBetweenFollowers = sum / _followers.Count;
    }

    private void RegulateSpeed() {
        if (_avgDistanceBetweenFollowers >= _regulateSpeedDist && GetComponent<Boid>().maxSpeed > 5f) {
            GetComponent<Boid>().maxSpeed -= 0.02f;
        } else {
            GetComponent<Boid>().maxSpeed = _maxSpeed;
        }
    }

    protected override void SetShipStats() {
        _life = 600;
        _armomr = 200f;
        _fuel = 250;
        _primaryAmmo = 250f;
        _secondaryAmmo = 200f;
        _attackPowerMultiplier = 2f;
        _fieldOfView = BattleSettings.Instance._leaderFieldOfView;
    }

    public void GetRandomTarget(List<Figther> fighters) {
        _targetToFollow = fighters[Random.Range(0, fighters.Count)].transform;
        _stateMachine.ChangeState(new Chasing());
    }

    public void SpawnFleet(int numberInFleet) {

        _fleet = new GameObject().AddComponent<Fleet>();

        string teamName = "";
        if (_team == Team.red) {
            teamName = "TeamRed";
        } else {
            teamName = "TeamGreen";
        }

        string fightersPath = teamName + "/Fighters";

        GameObject[] fightersAvailable = Resources.LoadAll<GameObject>(fightersPath);
        Debug.Log(fightersAvailable.Length);

        switch (numberInFleet) {
            case 3:
                break;
            case 4:
                break;
            case 5:
                SpawnWedge(fightersAvailable);
                break;
        }

        _fleet.AddLeader(this);
        _fleet.AddFighters(_followers);
        BattleFieldManager.Instance.AddFleet(_fleet, _team);
        //_fleet.AddFleetCamerasToManager();
        Debug.Log("Finished spawning fleet");
    }

    private void SpawnWedge(GameObject[] planesAvailable) {
        AddNewFollower(new Vector3(-7, 0, -5), Instantiate(planesAvailable[Random.Range(0, planesAvailable.Length)]));
        AddNewFollower(new Vector3(7, 0, -5), Instantiate(planesAvailable[Random.Range(0, planesAvailable.Length)]));
        AddNewFollower(new Vector3(-14, 0, -10), Instantiate(planesAvailable[Random.Range(0, planesAvailable.Length)]));
        AddNewFollower(new Vector3(0, 7, -10), Instantiate(planesAvailable[Random.Range(0, planesAvailable.Length)]));
        AddNewFollower(new Vector3(14, 0, -10), Instantiate(planesAvailable[Random.Range(0, planesAvailable.Length)]));
    }

    private void AddNewFollower(Vector3 pos, GameObject follower) {
        GameObject newFollower = follower;
        GameObject newPosition = new GameObject();
        newPosition.transform.parent = transform;
        newPosition.transform.position = transform.TransformPoint(pos);
        Figther fighter = newFollower.GetComponent<Figther>();
        fighter.SetFormationTransform(newPosition.transform);
        newFollower.transform.position = transform.TransformPoint(pos);
        newFollower.transform.localRotation = transform.localRotation;
        fighter._fleet = _fleet;
        fighter.SetTeamandHQ(_team,_hq);
        fighter.SetLeader(this);
        _followers.Add(fighter);
    }

    IEnumerator SearchForFleet() {
        while (true) {

            ChooseFleetToAttack();
            //UpdateChasingFleetPos();
            yield return new WaitForSeconds(10f);
        }
    }

    public void StartFleetSearch() {
        StartCoroutine(_searchForFleet);

    }

    public void EndSearchForFleet() {
        StopCoroutine(_searchForFleet);
    }

    public void ChooseFleetToAttack() {
        _attackingFleet = BattleFieldManager.Instance.ChooseNewFleet(this, false);

        /*
        if (_attackingFleet == null) {
            _attackingFleet = BattleFieldManager.Instance.ChooseNewFleet(this, true);
        }
        */

        if (_attackingFleet == null) {
            _stateMachine.ChangeState(new SeekingHQ());
        }

        if (_attackingFleet != null) {
            UpdateChasingFleetPos();
            _stateMachine.ChangeState(new ChasingFleet());
            SetFollowerAttackingFleetOrder();
        }
    }

    private void SetFollowerAttackingFleetOrder() {
        foreach (Figther f in _followers) {
            f._attackingFleet = _attackingFleet;
        }
    }

    Transform pos;
    public void UpdateChasingFleetPos() {
        _targetToFollow = _attackingFleet.GetAveragePositionAsTransform();
    }

    public override void Fire(Transform enemy) {
        ShootBlaster(_attackPowerMultiplier, enemy);
    }

    public override void CleanUpBeforeDestroy() {
        Debug.Log("Cleaned up" + this + " Life at" + _life);

        Destroy(_fleet);
        //_leader._followers.Remove(this);
    }


    public void OrderFollowersToDogFight() {

        foreach (Figther f in _followers) {

        }

    }
}

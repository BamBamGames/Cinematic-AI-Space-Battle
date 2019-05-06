using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetPair : MonoBehaviour{

    public  Fleet _pair1;
    public  Fleet _pair2;
    protected bool _engaged = false;
    private float _minDistanceBeforeEngagment = 50f;
    public StateMachine _stateMachine;
    public Camera _battleCamera;
    public float _timeSinceCreation = 0;

    private void Start() {
        StartCoroutine(Timer());
        _stateMachine = gameObject.AddComponent<StateMachine>();
        GameObject go = new GameObject();
        _battleCamera = go.AddComponent<Camera>();
        go.AddComponent<AudioListener>();
        CameraManager.Instance.AddBattleCamera(_battleCamera);
    }

    private bool _addedBattledCamera = false;

    public void Update() {
        UpdatePairStatus();
        if (!_addedBattledCamera && _timeSinceCreation > 5) {
            CameraManager.Instance.AddBattleCamera(_battleCamera);
            _addedBattledCamera = true;
        }
    }

    private void LateUpdate() {
        UpdateFightPostion();
    }

    IEnumerator Timer() {
        while (true) {
            yield return new WaitForSecondsRealtime(1f);
            _timeSinceCreation += 1;
        }
    }

    Vector3 _middleOfFight;

    private void UpdateFightPostion() {
        _middleOfFight = new Vector3((_pair1.GetAveragePosition().x + _pair2.GetAveragePosition().x) / 2,
            (_pair1.GetAveragePosition().y + _pair2.GetAveragePosition().y) / 2,
        (_pair1.GetAveragePosition().z + _pair2.GetAveragePosition().z) / 2);
        transform.localPosition = _middleOfFight;
        
        _battleCamera.transform.position = _middleOfFight + transform.right * 40f;
        _battleCamera.gameObject.transform.LookAt(transform);
    }

    public void UpdatePairStatus() {
        if (!_engaged) {
            if (Vector3.Distance(_pair1.GetAveragePosition(), _pair2.GetAveragePosition()) < _minDistanceBeforeEngagment) {
                Debug.Log("Fleets engaged:" + _pair1 + " and " + _pair2);
                _engaged = true;
                _stateMachine.ChangeState(new ScrambleFight());
                StartCoroutine(ChangeFightBehaviours());
                //InitiateEngagementBetweenFleets(pair);
            }
        } else if (_engaged) {
            Fleet first = _pair1;
            Fleet second = _pair2;



            if (first == null) {
                return;
            }

            if (second == null) {
                return;
            }




            if (!first._fleeing && first._allShips.Count <= 3) {
                first._fleeing = true;
                _escaping = first;
                _chasing = second;
                _stateMachine.ChangeState(new EscapeFight());
                Debug.Log("Fleet fleeing:" + first);
            }

           
            if (!second._fleeing && second._allShips.Count <= 3) {
                second._fleeing = true;
                _escaping = second;
                _chasing = first;
                _stateMachine.ChangeState(new EscapeFight());
                Debug.Log("Fleet fleeing:" + second);
            }
        }

        

    }

    public static FleetPair Create(Fleet first, Fleet second) {
        FleetPair fleetPair = new GameObject().AddComponent<FleetPair>();
        fleetPair._pair1 = first;
        fleetPair._pair2 = second;

        return fleetPair;
    }

    public Fleet GetFirstFleet() {
        return _pair1;
    }

    public Fleet GetSecondFleet() {
        return _pair2;
    }

    public bool IsEngaged() {
        return _engaged;
    }

    public void Engaged() {
        _engaged = true;
    }

    IEnumerator ChangeFightBehaviours() {

        while (true) {

            yield return new WaitForSeconds(20f);

            ChangePairBehaviour();
        }
    }

    public Fleet _escaping;
    public Fleet _chasing;

    private void ChangePairBehaviour() {
        if (!(_stateMachine.currentState is EscapeFight)) {
            EscapeFight();
        }
    }

    private void EscapeFight() {
        if (_pair1._totalHealth < _pair2._totalHealth) {
            _escaping = _pair1;
            _chasing = _pair2;
        } else {
            _escaping = _pair2;
            _chasing = _pair1;
        }

        _stateMachine.ChangeState(new EscapeFight());
    }

    private void DogFights() {

    }

    private void ScrambleFight() {

    }

    private void Seperation() {

    }
}

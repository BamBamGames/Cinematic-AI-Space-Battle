using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetPair : MonoBehaviour{

    protected Fleet _pair1;
    protected Fleet _pair2;
    protected bool _engaged = false;

    public StateMachine _stateMachine;

    private void Start() {
        _stateMachine = gameObject.AddComponent<StateMachine>();
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

    private void ChangePairBehaviour() {

        Fleet fleetToEscape;

        if (_pair1._totalHealth < _pair2._totalHealth) {
            fleetToEscape = _pair1;
        } else {
            fleetToEscape = _pair2;
        }

    }
}

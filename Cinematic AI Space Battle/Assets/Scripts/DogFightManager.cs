using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFightManager : MonoBehaviour {

    public GameObject _participant1, _participant2;
    public GameObject _evading;
    public GameObject _chasing;
    public bool _fighting = false;
    //public List<GameObject> _evading;
    //public List<GameObject> _chasing;
    public float _avgDistance = float.MaxValue;
    private float _distanceBeforeStartFight = 20f;
    private float _fightingDistance = 30f;

    public static DogFightManager Create(GameObject part1, GameObject part2) {
        DogFightManager dogFight = new GameObject().AddComponent<DogFightManager>();

        dogFight._participant1 = part1;
        dogFight._participant2 = part2;
        return dogFight;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckDistance();
        //Will engage the fight
        if (!_fighting && _avgDistance < _distanceBeforeStartFight) {
            RandomRoles(_participant1, _participant2);
            _fighting = true;
        }

        if (_fighting) {
            RegulateDistance();
        }
	}

    private void RegulateDistance() {
        if (_avgDistance < _fightingDistance) {
            _chasing.GetComponent<Boid>().maxSpeed += 0.01f;
        } else {
            _chasing.GetComponent<Boid>().maxSpeed = _chasing.GetComponent<Ship>()._maxSpeed;
        }
    }

    private void CheckDistance() {
        _avgDistance = Vector3.Distance(_evading.transform.position, _chasing.transform.position);
        //Vector3 fleet1 = GetMeanVector(_evading);
        //Vector3 fleet2 = GetMeanVector(_chasing);
    }

    private void ChangeRoles() {
        
    }

    private void RandomRoles(GameObject ship1, GameObject ship2) {

        if (Random.Range(0, 2) > 0) {
            _evading = ship1;
            _chasing = ship2;
        } else {
            _evading = ship2;
            _chasing = ship1;
        }

        _evading.GetComponent<StateMachine>().ChangeState(new Evading());

        _chasing.GetComponent<Ship>()._targetToFollow = _evading.transform;
        _chasing.GetComponent<StateMachine>().ChangeState(new Chasing());
    }

    public void StartDogFight(GameObject ship1, GameObject ship2) {
        _participant1 = ship1;
        _participant2 = ship2;
    }
}

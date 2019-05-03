using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : Ship {

    private List<Figther> _followers = new List<Figther>();
    public float _avgDistanceBetweenFollowers;
    public Fleet _fleet;
	// Update is called once per frame
	void Update () {
        CalculateDistanceBetweenFollowers();
        RegulateSpeed();
	}

    private void CalculateDistanceBetweenFollowers() {
        float sum = 0;
        foreach (Figther f in _followers) {
            sum += Vector3.Distance(transform.position, f.transform.position);
        }

        _avgDistanceBetweenFollowers = sum / _followers.Count;
    }

    private void RegulateSpeed() {
        if (_avgDistanceBetweenFollowers >= 22f && GetComponent<Boid>().maxSpeed > 5f) {
            GetComponent<Boid>().maxSpeed -= 0.02f;
        } else {
            GetComponent<Boid>().maxSpeed = 10f;
        }
    }

    protected override void SetShipStats() {
        _life = 300;
        _armomr = 200f;
        _fuel = 250;
        _primaryAmmo = 250f;
        _secondaryAmmo = 200f;
        _attackPowerMultiplier = 2f;
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
        _followers.Add(fighter);
    }
}

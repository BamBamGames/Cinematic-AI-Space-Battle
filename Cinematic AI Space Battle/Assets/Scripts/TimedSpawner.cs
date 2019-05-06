using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : MonoBehaviour {

    public float timeBeforeSpawn;
    public GameObject _toSpawn;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnObject() {

        while (true) {
            yield return new WaitForSeconds(timeBeforeSpawn);
            GameObject newItem = Instantiate(_toSpawn);
            newItem.transform.position = transform.position;
            newItem.transform.localRotation = transform.localRotation;
            StopAllCoroutines();
        }
    }
}

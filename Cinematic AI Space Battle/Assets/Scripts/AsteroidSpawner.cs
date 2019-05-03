using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public Vector3 _centerOfSpawn;
    public Vector3 _boxShape;
    private List<GameObject> _asteroidTypes = new List<GameObject>();
    public bool _loadRandomOnSpawn = false;
	// Use this for initialization
	void Start () {
        LoadInAsteroids();

        if (_loadRandomOnSpawn) {
            RandomSpawnInBox();
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A)) {
            RandomSpawnInBox();
        }
	}

    private void LoadInAsteroids() {
        GameObject[] asteroids = Resources.LoadAll<GameObject>("Asteroids");
        for (int i = 0; i < asteroids.Length; i++) {
            _asteroidTypes.Add(asteroids[i]);
        }
    }

    public int _asteroidChance = 5;
    public int _minAsteroidSize = 50;
    public int _maxAsteroidSize = 400;
    public int _amountToSpawn = 100;

    private void RandomSpawnInBox() {
        int possibleTypes = _asteroidTypes.Count;
        for (int i = 0; i < _amountToSpawn; i++) {
            Vector3 spawnPos = _centerOfSpawn + new Vector3(
          (Random.value - 0.5f) * _boxShape.x,
          (Random.value - 0.5f) * _boxShape.y,
          (Random.value - 0.5f) * _boxShape.z
                );

            int size = (int)Random.Range(_minAsteroidSize, _maxAsteroidSize);

            GameObject asteroid = Instantiate(_asteroidTypes[Random.Range(0, possibleTypes)]);
            asteroid.transform.position = spawnPos;
            asteroid.transform.localScale = new Vector3(size, size, size);
            asteroid.transform.parent = transform;
        }



    }

    /*
    private void RandomSpawnShit() {
        int possibleTypes = _asteroidTypes.Count;

        //Load across X
        for (int i = (int)_startSpawn.x; i < (int)_boxShape.x; i++) {

            for (int j = (int)_startSpawn.y; j < (int)_boxShape.y; j++) {
                int rand = (int)Random.Range(0, _asteroidChance);
                int size = (int)Random.Range(_minAsteroidSize, _maxAsteroidSize);

                if (rand > 0) {
                    GameObject asteroid = Instantiate(_asteroidTypes[Random.Range(0, possibleTypes)]);
                    asteroid.transform.position = new Vector3(i, 0, 0);
                    asteroid.transform.localScale = new Vector3(size, size, size);
                    asteroid.transform.parent = transform;
                }
            }
        }

        //Load across Y
        for (int i = (int)_startSpawn.y; i < (int)_boxShape.y; i++) {
            int rand = (int)Random.Range(0, _asteroidChance);
            int size = (int)Random.Range(_minAsteroidSize, _maxAsteroidSize);

            if (rand > 0) {
                GameObject asteroid = Instantiate(_asteroidTypes[Random.Range(0, possibleTypes)]);
                asteroid.transform.position = new Vector3(0, i, 0);
                asteroid.transform.localScale = new Vector3(size, size, size);
                asteroid.transform.parent = transform;
            }
        }

        //Load across Z
        for (int i = (int)_startSpawn.z; i < (int)_boxShape.z; i++) {
            int rand = (int)Random.Range(0, _asteroidChance);
            int size = (int)Random.Range(_minAsteroidSize, _maxAsteroidSize);

            if (rand > 0) {
                GameObject asteroid = Instantiate(_asteroidTypes[Random.Range(0, possibleTypes)]);
                asteroid.transform.position = new Vector3(0, 0, i);
                asteroid.transform.localScale = new Vector3(size, size, size);
                asteroid.transform.parent = transform;
            }
        }
    }
    */
}

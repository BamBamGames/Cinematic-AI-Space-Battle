using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour {

    List<GameObject> _blasterBullets = new List<GameObject>();
    List<GameObject> _missilePool = new List<GameObject>();

    public int _blasterPoolSize = 1000;
    public int _missilePoolSize = 200;

    public static AmmoPool Instance;
	// Use this for initialization
	void Start () {
        Instance = this;
        LoadInBlasterPool();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadInBlasterPool() {
        GameObject bullet = Resources.Load<GameObject>("Bullet");
        for (int i = 0; i < _blasterPoolSize; i++) {
            GameObject nb = Instantiate(bullet);
            nb.SetActive(false);
            _blasterBullets.Add(nb);
            nb.transform.parent = transform;
            
        }
    }

    private void LoadInMissilePool() {

    }

    public GameObject GetBullet() {

        for (int i = 0; i < _blasterBullets.Count; i++) {
            if (!_blasterBullets[i].activeInHierarchy) {
                _blasterBullets[i].transform.parent = null;
                return _blasterBullets[i];
            }
        }

        return null;
    }
}

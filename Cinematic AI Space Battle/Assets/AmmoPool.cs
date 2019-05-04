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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadInBlasterPool() {
        GameObject bullet = Resources.Load<GameObject>("Bullet");
        for (int i = 0; i < _blasterPoolSize; i++) {
            GameObject nb = Instantiate(bullet);
            _blasterBullets.Add(Instantiate(bullet));
            nb.transform.parent = transform;
            nb.SetActive(false);

        }
    }

    private void LoadInMissilePool() {

    }
}

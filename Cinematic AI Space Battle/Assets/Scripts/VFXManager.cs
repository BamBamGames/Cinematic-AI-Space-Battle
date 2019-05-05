using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour {

    public static VFXManager Instance;

    public GameObject _shipFire;
    public GameObject _shipSmoke;
    public GameObject _shipExplosion;
    public GameObject _sparks;
    public GameObject _sparksHit;
    public GameObject _hqMalfunction;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

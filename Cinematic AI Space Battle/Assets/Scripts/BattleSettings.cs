using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton used throuhgout the program so u can set up the battle externaly
public class BattleSettings : MonoBehaviour {
	
    public float _fighterSpeed;
    public float _leaderSpeed;
    public float _commanderSpeed;
    public float _bulletSpeed;
    public float _medicSpeed;
    public float _blasterDamage;
    public float _missileDamage;
    public float _medicHealingPower;
    public float _resupplyTime;
    public float _fighterFieldOfView;
    public float _leaderFieldOfView;

    public static BattleSettings Instance;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

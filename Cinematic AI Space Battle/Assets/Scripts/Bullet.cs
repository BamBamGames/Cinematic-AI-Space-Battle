using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Acts as a bullet and carries out several functionalitys
public class Bullet : MonoBehaviour {

    IEnumerator _bulletTimeOutRoutine;
    private float _damage = 0;
    public GameObject _owner;
	// Use this for initialization
	void Start () {
        
	}

    private void OnEnable() {
        _bulletTimeOutRoutine = BulletTimeOut();
        StartCoroutine(_bulletTimeOutRoutine);
    }

    private void OnDisable() {
        StopCoroutine(_bulletTimeOutRoutine);
        _damage = 0;
        //transform.parent = AmmoPool.Instance.transform;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SetDamage(float damage) {
        _damage = damage;
    }

    public float GetDamage() {
        if (_owner != null) {
            _owner.GetComponent<Ship>()._hq.AddPoints(1f);
        }
        return _damage;
    }

    IEnumerator BulletTimeOut() {

        while (true) {
            yield return new WaitForSeconds(5);
            gameObject.SetActive(false);
        }
    }
}

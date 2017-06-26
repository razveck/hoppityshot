using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float maxHP;
	public float curHP;

	// Use this for initialization
	protected virtual void Start () {
		if(maxHP == 0) {
			maxHP = 1;
		}
		if(curHP == 0) {
			curHP = maxHP;
		}
	}

	// Update is called once per frame
	protected virtual void Update () {
		
	}

	public virtual void TakeDamage(int damage) {
		curHP -= damage;

		if(curHP <= 0) {
			Die();
		}
	}

	protected virtual void Die() {
		Destroy(gameObject);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITarget {

	public float timer;
	public float maxTime;

	public GameObject hitParticlePrefab;

	public GameObject player;
	public Transform shootPoint;

	public Waypoint waypoint;

	public AudioSource aSrc;

	// Use this for initialization
	void Start () {
		aSrc = GetComponent<AudioSource>();
		player = Global.player.gameObject;
		//		transform.Rotate(Vector3.up,-Quaternion.Angle(transform.rotation,player.transform.rotation));
		transform.LookAt(player.transform,transform.up);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= maxTime) {
			Shoot();
			timer = 0;
		}
	}

	void Shoot() {
		RaycastHit hit;
		if(Physics.Raycast(shootPoint.position,player.transform.position - shootPoint.position,out hit)) {
			if(hit.collider.gameObject == player) {
				player.GetComponent<Health>().TakeDamage(1);
				GetComponent<Animator>().SetTrigger("Shoot");
				shootPoint.GetComponent<ParticleSystem>().Play();
				aSrc.Play();
			}
		} 
	}

	public void GetHit(Vector3 point, Vector3 normal) {
		Instantiate(hitParticlePrefab,point,Quaternion.LookRotation(normal,Vector3.up));
		GetComponent<Health>().TakeDamage(1);
	}

	void OnDestroy() {
		waypoint.liveEnemies--;
	}
}
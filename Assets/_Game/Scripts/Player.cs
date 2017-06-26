using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

	NavMeshAgent nav;
	NavMeshPath path;

	public bool atWaypoint;

	void Awake() {
		Global.player = this;
		nav = GetComponent<NavMeshAgent>();
	}
	
	
	// Update is called once per frame
	void Update () {
		if(nav.remainingDistance < 0.01f && atWaypoint==false) {
			Global.waypointManager.ArrivedAtWaypoint();
			atWaypoint = true;
		}
	}
	
	public void GoToWaypoint(Vector3 destination) {
		nav.destination= destination;
		atWaypoint = false;
	}
}

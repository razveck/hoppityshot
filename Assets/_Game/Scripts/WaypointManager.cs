using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour {

	public List<Waypoint> waypoints;
	public int curWP;
	
	void Awake() {
		Global.waypointManager = this;
	}
	public void Start() {
		Global.player.GoToWaypoint(waypoints[curWP].transform.position);
	}

	public void ArrivedAtWaypoint() {
		if(curWP<waypoints.Count)
			waypoints[curWP].StartWaypoint();
	}

	public void WaypointDestroyed() {
		curWP++;
		Global.player.GoToWaypoint(waypoints[curWP].transform.position);
	}
	
}

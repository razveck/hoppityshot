using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	public float timer;
	public float maxTime;
	public bool timeBased;

	public GameObject enemyPrefab;
	public List<Transform> spawns;
	public int liveEnemies;

	// Use this for initialization
	void Start () {
		timer = 0;
		liveEnemies = spawns.Count;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(liveEnemies<=0) {
			EndWaypoint();
		}
	}

	public void StartWaypoint() { //spawn stuff
		for(int i = 0;i < spawns.Count;i++) {
			Enemy enemy=Instantiate(enemyPrefab, spawns[i].position, spawns[i].rotation).GetComponent<Enemy>();
			enemy.waypoint = this;
		}
	}

	public void EndWaypoint() {
		Global.waypointManager.WaypointDestroyed();
		Destroy(gameObject);
	}
}

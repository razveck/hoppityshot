using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirLight : MonoBehaviour {
	Vector3 axis;
	float rotationTimer;
	float rotationTarget;

	public Color[] colors;
	float colorTimer;
	float colorTarget;

	// Use this for initialization
	void Start () {
		axis = transform.up;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.AngleAxis(Mathf.Repeat(Time.time*180, 360),axis);
		
	}

	void ChangeColor() {

	}
}

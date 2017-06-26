using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Texture2D crosshairTex;

	public Material rainbowMat;
	float matTimer;

	void Awake() {
		Global.gameManager = this;
		matTimer = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.isFocused) {
			Cursor.lockState = CursorLockMode.Locked;
		}
		matTimer += Time.deltaTime;
		if(matTimer > 10)
			matTimer = 5;
		rainbowMat.SetFloat("_TextureOffset",matTimer);
	}
}

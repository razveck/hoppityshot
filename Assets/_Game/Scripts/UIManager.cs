using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject canvas;
	public Text ammoText;
	public Text hpText;

	void Awake() {
		Global.uiManager = this;
	}


	public void UpdateAmmo(string text) {
		ammoText.text = "x"+ text;
	}

	public void UpdateHealth(string text) {
		hpText.text = "Health: " + text;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health{


	public override void TakeDamage(int damage) {
		base.TakeDamage(damage);
		Global.uiManager.UpdateHealth(curHP.ToString());
	}

	protected override void Die() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
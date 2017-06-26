using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public float fireRate;
	public float timer;
	public bool canShoot;
	public LayerMask raycastMask;
	public GameObject defaultHitParticlePrefab;

	public Animator anim;
	public AudioSource aSrc;
	public AudioClip reload;	
	public AudioClip shoot;

	int _curAmmo;
	public int curAmmo
	{
		get { return _curAmmo; }
		set
		{
			_curAmmo = value;
			Global.uiManager.UpdateAmmo(value.ToString());
		}
	}
	public int maxAmmo;

	// Use this for initialization
	void Start () {
		aSrc = GetComponent<AudioSource>();
		if(maxAmmo == 0) {
			maxAmmo = 12;
		}
		if(curAmmo == 0) {
			curAmmo = maxAmmo;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if(Input.GetMouseButtonDown(0) && canShoot && timer >= 60f / fireRate) {
			Shoot();
		}

		if(Input.GetKeyDown(KeyCode.R)) {
			Reload();
		}
	}

	void Shoot() {
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit,float.PositiveInfinity,raycastMask)) {
			
			if(hit.collider.GetComponent<ITarget>() != null) {
				hit.collider.GetComponent<ITarget>().GetHit(hit.point, hit.normal);
			} else {
				Instantiate(defaultHitParticlePrefab,hit.point,Quaternion.LookRotation(hit.normal, Vector3.up));
			}
		}
		anim.SetTrigger("Shoot");
		aSrc.Play();
		timer = 0;
		curAmmo--;
		if(curAmmo <= 0) {
			Reload();
		}
	}

	void Reload() {
		canShoot = false;
		aSrc.clip = reload;
		
		StartCoroutine(GetComponent<MouseLook>().Reload(0));
		StartCoroutine(ReloadFinished(GetComponent<MouseLook>().reloadTime*2));
	}

	IEnumerator ReloadFinished(float timeToWait) {
		yield return new WaitForSeconds(timeToWait);
		
		aSrc.clip = shoot;
		curAmmo = maxAmmo;
		canShoot = true;
	}
}

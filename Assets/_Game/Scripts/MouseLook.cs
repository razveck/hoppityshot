using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class MouseLook:MonoBehaviour {

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;

	float xRot;
	float yRot;

    private Quaternion playerRotation;
    private Quaternion cameraRotation;

	public Transform worldCam;
	public int worldFOVNormal;
	public int worldFOVReload;
	public Transform firstPersonCam;
	public int firstPersonFOVNormal;
	public int firstPersonFOVReload;
	public float reloadTime;

	Vector3 fixedPosition;

	Rigidbody rigid;

	AudioSource aSrc;
	public float lowPitch;
	public float highPitch;

    void Start() {
		aSrc = GetComponent<AudioSource>();
		
		playerRotation = transform.localRotation;

		fixedPosition = firstPersonCam.localPosition;
        cameraRotation = firstPersonCam.localRotation;

		rigid = GetComponent<Rigidbody>();
    }

	void FixedUpdate() {
		
	}

    void Update() {        
		GetInput();
	}

	void LateUpdate() {
		LookRotation();
		firstPersonCam.localPosition = fixedPosition;
		worldCam.localPosition = firstPersonCam.localPosition;
	}
	
	void GetInput() {
		yRot = Input.GetAxis("Mouse X") * XSensitivity;
		xRot = Input.GetAxis("Mouse Y") * YSensitivity;
	}

    void LookRotation() {	        

        playerRotation *= Quaternion.Euler(0f,yRot,0f);
        cameraRotation *= Quaternion.Euler(-xRot,0f,0f);

        if(clampVerticalRotation)
            cameraRotation = ClampRotationAroundXAxis(cameraRotation);

        if(smooth) {
			rigid.MoveRotation(Quaternion.Slerp(transform.localRotation,playerRotation,smoothTime * Time.deltaTime));
			firstPersonCam.localRotation = Quaternion.Slerp(firstPersonCam.localRotation,cameraRotation,smoothTime * Time.deltaTime);
        } else {
			//rigid.MoveRotation(playerRotation);
			transform.localRotation = playerRotation;
			firstPersonCam.localRotation = cameraRotation;
        }
		worldCam.localRotation = firstPersonCam.localRotation;
	}


    Quaternion ClampRotationAroundXAxis(Quaternion q) {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX,MinimumX,MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

	public IEnumerator Reload(float time) {
		time += Time.deltaTime;
		firstPersonCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(firstPersonFOVNormal,firstPersonFOVReload,time/ reloadTime);
		worldCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(worldFOVNormal,worldFOVReload,time/ reloadTime);
		aSrc.pitch=Mathf.Lerp(highPitch,lowPitch,time / reloadTime);
		aSrc.Play();
		yield return null;
		if(time <= reloadTime) {
			yield return StartCoroutine(Reload(time));
		}
		firstPersonCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(firstPersonFOVNormal,firstPersonFOVReload,time/ reloadTime);
		worldCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(worldFOVNormal,worldFOVReload,time/ reloadTime);
		aSrc.pitch = Mathf.Lerp(highPitch,lowPitch,time / reloadTime);
		aSrc.Play();
		yield return null;
	}
}


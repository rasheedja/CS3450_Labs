using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Transform firingPosition;
	public GameObject ballPrefab;
	public float firePower;

	public AudioSource audio;
	public AudioClip fireSound;
	public AudioClip noAmmoSound;

	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			if (GameManager.Instance.HasAmmo) Fire ();
			else audio.PlayOneShot (noAmmoSound);
		} 
	}

	void Fire() {
		GameObject ball = Instantiate (ballPrefab, firingPosition.position, firingPosition.rotation);
		Rigidbody rb = ball.GetComponent<Rigidbody> ();
		rb.AddForce (firingPosition.transform.forward * firePower, ForceMode.Impulse);
		audio.PlayOneShot (fireSound);
		GameManager.Instance.DecreaseAmmo ();
	}

}

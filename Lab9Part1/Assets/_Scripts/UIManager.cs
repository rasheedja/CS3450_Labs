using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class is literally just a dumb view, doesn't calculate anything, just puts data on the screen... and it's singleton so we have easy access
 */
public class UIManager : MonoBehaviour {

	public static UIManager Instance { get; private set; }

	public Text scoreText;
	public Text timerText;
	public Text ammoText;


	void Awake() {
		if (Instance == null) Instance = this;
		else Destroy (gameObject);
	}

	public void UpdateScore(int score) {
		scoreText.text = score.ToString ();
	}

	public void UpdateAmmo(int ammo) {
		ammoText.text = ammo.ToString ();
	}

	public void UpdateTimer(float time) {
		timerText.text = time.ToString ();
	}


}

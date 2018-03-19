using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public bool HasAmmo { get { return ammo > 0; } }
			
	public Transform player;
	public Transform hoop;

	int score = 0;
	int ammo = 10;
	float timer = 30f;

	public AudioSource audio;
	public AudioClip increaseScore;
	public AudioClip increaseAmmo;
	public AudioClip timerTick;
	public AudioClip timerGameOver;


	void Awake () {
		if (Instance == null) Instance = this;
		else Destroy (gameObject);
	}

	void Start () {
		UIManager.Instance.UpdateAmmo (ammo);
		UIManager.Instance.UpdateScore (score);
		UIManager.Instance.UpdateTimer (timer);
		InvokeRepeating ("TimerTick", 1, 1);
	}

	public void IncreaseScore() {
		score += Mathf.CeilToInt (Vector3.Distance (player.position, hoop.position));
		UIManager.Instance.UpdateScore (score);
		audio.PlayOneShot (increaseScore);
	}

	public void DecreaseAmmo() {
		ammo--;
		UIManager.Instance.UpdateAmmo (ammo);
		// no need for audio here as covered by firing sound
	}

	public void IncreaseAmmo() {
		ammo += 10;
		ammo = Mathf.Min (ammo, 20);
		UIManager.Instance.UpdateAmmo (ammo);
		audio.PlayOneShot (increaseAmmo);
	}

	void TimerTick() {
		timer -= 1f;
		if (timer > 0) {
			audio.PlayOneShot (timerTick);
			UIManager.Instance.UpdateTimer (timer);
		} else {
			CancelInvoke ("TimerTick");
			StartCoroutine (GameOverCR ());
		}
	}

	IEnumerator GameOverCR() {
		audio.PlayOneShot (timerGameOver);
		UIManager.Instance.UpdateTimer (0);
		yield return new WaitForSeconds (timerGameOver.length + 2f);
        int highscore = PlayerPrefs.GetInt(Globals.HIGH_SCORE_KEY);
        if (score > highscore)
        {
            PlayerPrefs.SetInt(Globals.HIGH_SCORE_KEY, score);
        }
		SceneManager.LoadSceneAsync (Globals.MENU_SCENE);
	}





}

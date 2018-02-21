using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    public AudioSource gameOverSound;
    private int secondsLeft = 30;

	// Use this for initialization
	void Start () {
        UpdateTimer();
        InvokeRepeating("DecrementTimer", 0, 1f);
        InvokeRepeating("UpdateTimer", 0, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (secondsLeft == 0)
        {
            gameOverSound.Play();
            CancelInvoke();
            InvokeRepeating("DecrementTimer", 0, 1f);
        }

        if (secondsLeft == -3)
        {
            CancelInvoke();
            SceneManager.LoadSceneAsync(Globals.MENU_SCENE);
        }
    }

    public void DecrementTimer()
    {
        secondsLeft -= 1;
    }

    public void UpdateTimer()
    {
        this.GetComponent<Text>().text = "Timer: " + secondsLeft;
    }
}

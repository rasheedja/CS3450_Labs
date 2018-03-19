using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTextController : MonoBehaviour {

    public Text highScoreText;

	// Use this for initialization
	void Start () {
        UpdateUI(PlayerPrefs.GetInt(Globals.HIGH_SCORE_KEY));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateUI(int score)
    {
        if (score > 0)
        {
            highScoreText.text = "Highscore: " + score + "!";
        }
        else
        {
            highScoreText.text = "No Score!";
        }
    }

    public void ButtonHandlerReset()
    {
        PlayerPrefs.DeleteKey(Globals.HIGH_SCORE_KEY);
        UpdateUI(0);
    }
}

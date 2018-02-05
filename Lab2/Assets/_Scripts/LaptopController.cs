using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaptopController : MonoBehaviour {

    public Text timeText;
    private Text textComponenet;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float timeSinceLevelLoad = Time.timeSinceLevelLoad;

        // Change text to red if it's been longer than 10 seconds
        if (timeSinceLevelLoad >= 10)
        {
            timeText.color = new Color(255, 0, 0);
        }
        int minutes = Mathf.FloorToInt(timeSinceLevelLoad / 60);
        int seconds = Mathf.FloorToInt(timeSinceLevelLoad % 60);

        timeText.text = string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));
	}
}

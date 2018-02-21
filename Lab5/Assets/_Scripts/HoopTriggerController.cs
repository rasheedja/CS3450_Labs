using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoopTriggerController : MonoBehaviour {

    public AudioSource scoreSound;
    public GameObject scoreText;

    private int scoreCount;
	// Use this for initialization
	void Start () {
        scoreCount = 0;
        updateScoreCount();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball")
        {
            // Increment the score depending on the distance between the player and the hoop
            scoreCount = scoreCount + Mathf.CeilToInt(Vector3.Distance(Camera.main.transform.position, this.transform.position));
            scoreSound.Play();
            updateScoreCount();
        }
    }

    public void updateScoreCount()
    {
        scoreText.GetComponent<Text>().text = "Score: " + scoreCount;
    }
}

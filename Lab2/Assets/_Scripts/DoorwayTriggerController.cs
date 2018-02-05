using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayTriggerController : MonoBehaviour {

    public AudioSource doorwaySound;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            doorwaySound.Play();
            Debug.Log("Playing sound");
        }
    }
}

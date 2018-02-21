using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour {

    public GameObject ballPrefab;
    public GameObject ammoText;
    public AudioSource fireSound;
    public AudioSource outOfAmmoSound;
    public static int ammoCount;

	// Use this for initialization
	void Start () {
        ammoCount = 10;
        updateAmmoText();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (ammoCount > 0)
            {
                ShootBall();
                updateAmmoText();
            } else
            {
                outOfAmmoSound.Play();
            }
        }
	}   

    public void ShootBall()
    {
        GameObject ball = (GameObject) Instantiate(ballPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
        ball.tag = "Ball";
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * 20, ForceMode.Impulse);
        fireSound.Play();
        Object.Destroy(ball, 10.0f);
        ammoCount -= 1;
    }

    public void updateAmmoText()
    {
        ammoText.GetComponent<Text>().text = "Ammo: " + ammoCount;
    }
}

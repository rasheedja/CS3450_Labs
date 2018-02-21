using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoController : MonoBehaviour {
    public GameObject ammoText;
    public GameObject balanceBall;
    public AudioClip reloadSound;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            PlayerInteractionController.ammoCount += 10;
            if (PlayerInteractionController.ammoCount > 20)
            {
                PlayerInteractionController.ammoCount = 20;
            }
            ammoText.GetComponent<Text>().text = "Ammo: " + PlayerInteractionController.ammoCount;
            AudioSource.PlayClipAtPoint(reloadSound, balanceBall.transform.position);

            balanceBall.SetActive(false);
            Invoke("EnableBalanceBall", 5.0f);
        }
    }

    public void EnableBalanceBall()
    {
        balanceBall.SetActive(true);
    }
}

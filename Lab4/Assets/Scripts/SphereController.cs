using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : InteractiveObjectBase {
    public GameObject explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnInteraction()
    {
        Instantiate(explosion, this.transform.position, this.transform.rotation);
    }
}

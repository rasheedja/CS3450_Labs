using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour {

    public GameObject cubePrefab;
    public Transform explosionForceLocation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.C))
        {
            CreateCube();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Physics.gravity = Physics.gravity * -1;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Boom();
        }
	}

    public void CreateCube()
    {
        GameObject cube = (GameObject) Instantiate(cubePrefab, transform.position, transform.rotation);
        cube.transform.parent = this.transform;
        //Rigidbody rb = cube.GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward * 20, ForceMode.Impulse);
        //rb.drag = Random.Range(0, 10);
    }

    public void Boom()
    {
        foreach (Transform cube in this.transform.transform)
        {
            cube.GetComponent<Rigidbody>().AddExplosionForce(20, explosionForceLocation.position, 10, 1, ForceMode.Impulse);
        }
    }
}

using UnityEngine;
using System.Collections;

public class RagdollController : MonoBehaviour {

    void Awake()
    {
        foreach (Rigidbody component in this.GetComponentsInChildren<Rigidbody>())
        {
            component.isKinematic = true;
        }
    }

    void Update () 
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			EnableRagdoll();
		}
	}

	public void EnableRagdoll()
	{
        this.GetComponent<Animator>().enabled = false;
        foreach (Rigidbody component in this.GetComponentsInChildren<Rigidbody>())
        {
            component.isKinematic = false;
        }
    }
}

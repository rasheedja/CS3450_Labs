using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	// the target that the camera will follow
	public Transform target;

	// the distance the camera will stay from the target
	public int distanceFromTarget = 5;

	// boolean to toggle following on and off
	public bool follow = true;

	// the displacement of the camera on the Y axis
	public float displacementY = 1.5f;

	// easing variable
	public float easing = 0.1f;

	// variable to track the new position of the camera
	private Vector3 newPos;

	/**
	    Update the cameras position to follow the target
	*/
	void Update () 
	{
		// if allowed to follow the target
		if (follow) 
		{
			// move within "distanceFromTarget" metres of the target
			// but retain the same camera rotation at all times
			newPos = target.position - (Vector3.forward * distanceFromTarget);

			// apply the vertical displacement
			newPos.y = displacementY;

			// ease the camera to the new position: (target - current position) * easing
			transform.position += (newPos - transform.position) * easing;

			// match the rotation of the target so we look directly at it
			transform.LookAt(target);
		}
		else // otherwise just look at the target
		{
			// use LookAt(...) to point towards the target
			transform.LookAt(target);
		}
	}
}
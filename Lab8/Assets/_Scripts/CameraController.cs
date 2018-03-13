using UnityEngine;
using System.Collections;
 
public class CameraController : MonoBehaviour 
{
    // the target that the camera will follow
    public Transform target;
      
    // the distance the camera will stay from the target
    public int distanceFromTarget = 5;
    
	// vertical displacement
	public float displacementY = 1.5f;
     
    // easing variable
    public float easing = 0.1f;
     
    // variable to track the new position of the camera
    private Vector3 newPos;
	
	
    void Update () 
    {
        newPos = target.position - (transform.forward * distanceFromTarget);
		newPos.y += displacementY;
         
        transform.position += (newPos - transform.position) * easing;
        transform.LookAt(target);
    }
}
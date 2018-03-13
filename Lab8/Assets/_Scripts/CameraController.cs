using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour 
{
    // the target that the camera will follow
    public Transform target;
      
    // the distance the camera will stay from the target
    public int distanceFromTarget = 5;
    
	// vertical displacement
	public float displacementY = 0f;
     
    // easing variable
    public float easing = 0.1f;
     
    // variable to track the new position of the camera
    private Vector3 newPos;

    void Update()
    {
        if (target != null)
        {
            newPos = target.position - (transform.forward * distanceFromTarget);
            newPos.y += displacementY - 1;

            transform.position += (newPos - transform.position) * easing;
            transform.LookAt(target);
        }
    }
}
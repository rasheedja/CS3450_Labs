using UnityEngine;
using System.Collections;

public class CharacterMotor : MonoBehaviour 
{
	public static CharacterMotor Instance  { get; private set; }
	
	public bool IsMoving { get; private set; }
	public bool IsJumping { get; private set; }
	public bool IsGrounded { get; private set; }

	public float speed = 10.0f;
	public float jumpSpeed = 20.0f;
	public float gravity = 20.0f;
	public float rotationSpeed = 100.0f;

	public float currentSpeed = 0.0f;
	public float maxSpeed = 10.0f;
	public float acceleration = 10.0f;
	public float decceleration = 20.0f;

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController characterController;

	// TODO - add animator controller variables here
	public Animator animatorController;
	private int walkingSpeedHashId;
	private int jumpTriggerHashId;




	void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);

		IsMoving = IsJumping = false;
		characterController = GetComponent<CharacterController>();
		IsGrounded = characterController.isGrounded;

		// TODO - init anim controller stuff here...
		walkingSpeedHashId = Animator.StringToHash("walkingSpeed");
		jumpTriggerHashId = Animator.StringToHash("jump");
	}

	// Handle player movement
	void Update() 
	{
		// are we grounded?
		IsGrounded = characterController.isGrounded;

	    // if we are on the ground then allow movement
	    if (IsGrounded) 
	    {
	        // whether we're moving and jumping
			float input = Input.GetAxis("Vertical");
	        IsMoving = (input != 0);
			IsJumping = Input.GetButton("Jump");

			// multiply forward axis by input to dictate direction (forwards / backwards since axis is 1 to -1)
			moveDirection = transform.forward; //* Input.GetAxis("Vertical");
	        
			if (IsMoving) 
			{
				currentSpeed += ((acceleration * input) * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
			}
			else if (currentSpeed > 0) 
			{
				currentSpeed -= (decceleration * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
			}
			else if (currentSpeed < 0) 
			{
				currentSpeed += (decceleration * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, 0);
			}


	        // multiply by speed
			moveDirection *= currentSpeed;

			// TODO - set the walking speed (float) within the anim controller here
			animatorController.SetFloat(walkingSpeedHashId, currentSpeed);

	        
			if (IsJumping) {
				moveDirection.y = jumpSpeed;

				// TODO - set the jump trigger within the anim controller
				animatorController.SetTrigger(jumpTriggerHashId);
			}
	    }
		else // apply gravity 
		{
			moveDirection.y -= (gravity * Time.deltaTime);
		}
	
		// retrieve rotation and multiply by speed (use Time.deltaTime to do movement per second rather than per frame)
		float rotation = (Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime;

		// perform rotation of the player
	    transform.Rotate(0, rotation, 0);

	    // Move the controller (use Time.deltaTime to do movement per second rather than per frame)
		characterController.Move(moveDirection * Time.deltaTime);
	}
	
}

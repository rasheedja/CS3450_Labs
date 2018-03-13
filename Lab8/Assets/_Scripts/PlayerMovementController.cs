using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour 
{
	public bool movementEnabled = true;
	public bool grounded = false;
	
	// players third person camera
	public GameObject camPrefab;
	
	// speed variables
	public float speed = 0.0F;
	public float speedIncrement = 0.05F;
	public float speedMax = 4.0F;
	
	// rotation variables
	public float groundRotation = 3.0F;
	public float airRotation = 5.0F;
	
	// jumping variables
	public float jumpPower = 0.0F;
	public float jumpIncrement = 0.1F;
	public float maxJumpPower = 3.0F;
	private bool chargingJump = false;
	
	// controller and direction
	private CharacterController controller;
	private Vector3 moveDirection = Vector3.zero;
	private float gravity = 10;
	
	// particles
	public ParticleEmitter snowBackEmitter;
	public ParticleEmitter snowFrontEmitter;
	public float maxEmission = 200;
	
	void Awake()
	{
		controller = GetComponent<CharacterController>();
		
		// retrieve and stop particles
		snowFrontEmitter.minEmission = snowFrontEmitter.maxEmission = 0;
		snowBackEmitter.minEmission = snowBackEmitter.maxEmission = 0;
	}
	
	void Update()
	{
		// update grounded
		grounded = controller.isGrounded;
		
		// retrieve current user input
		float clientForwardInput = Input.GetAxis("Vertical"); // ws
		float clientCarvingInput = Input.GetAxis("Horizontal"); // ad
		bool clientJumpDown = Input.GetKeyDown(KeyCode.Space);
		bool clientJumpCharging = Input.GetKey(KeyCode.Space);
		bool clientJumpReleased = Input.GetKeyUp(KeyCode.Space);
		float jump = 0.0F;
		
		// if movement is disabled then set axis inputs to zero
		if (!movementEnabled) 
		{
			clientForwardInput = clientCarvingInput = 0;
			clientJumpDown = clientJumpCharging = clientJumpReleased = false;
		}
		
		// increase or decrease speed 
		if (clientForwardInput > 0) speed += speedIncrement;
		else speed -= speedIncrement;
		
		// limit speed
		speed = Mathf.Clamp(speed, 0.0F, speedMax);
		
		// when space key is pressed
		if (clientJumpDown) chargingJump = true;
		
		// whilst space key is still down
		if (clientJumpCharging) 
		{
			// when charging jump
			if (chargingJump) 
			{
				// increment jump power
				jumpPower += jumpIncrement;
				
				// if jump power is greater than the max jump power, then stop incrementing it
				if (jumpPower >= maxJumpPower) 
				{
					jumpPower = maxJumpPower;
					chargingJump = false;
				}
			}
			
			// when charging jump prevent player turning
			clientCarvingInput = 0;
		}
		
		// once space key is released, jump unless the jump was fully charged and already done
		if (clientJumpReleased)
		{
			jump = jumpPower;
			chargingJump = false;
			jumpPower = 0;
		}
		
		// if the player is on the ground
		if (grounded) 
		{
			// calculate directional vector for movement
			moveDirection = new Vector3(0, jump, speed);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speedMax;
			
			// calculate rotation when on ground
			transform.Rotate(0, clientCarvingInput * (groundRotation * Time.deltaTime), 0, Space.World);
		}
		else
		{
			// calculate rotation when in air
			transform.Rotate(0, clientCarvingInput * (airRotation * Time.deltaTime), 0, Space.World);
		}
		
		// apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		// make the player move
		controller.Move(moveDirection * Time.deltaTime);
		
		// spray particles depending on our speed and whether we're grounded or not
		snowFrontEmitter.emit = snowBackEmitter.emit = grounded;
		snowFrontEmitter.maxEmission = (speed / speedMax) * maxEmission;
		snowFrontEmitter.minEmission = (speed / speedMax) * (maxEmission * 0.5F);
		snowBackEmitter.maxEmission = (speed / speedMax) * maxEmission;
		snowBackEmitter.minEmission = (speed / speedMax) * (maxEmission * 0.5F);
	}
}

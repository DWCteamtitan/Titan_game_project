﻿using UnityEngine;
using System.Collections;
using TitanMaria.StateMachine;

public class TitanPlayerState : StateBehaviour
{
	//TODO: Math for time.deltaTime
	private const float NORMALSPEED = 15.0f;
	private const float NORMALJUMPSPEED = 30.0f;
	private const float UWSPEED = 13.0f;
	private const float UWJUMPSPEED = 5.0f;
	private const float UW2SPEED = 15.0f;
	private const float UW2JUMPSPEED = 8.0f;
	
	//Declare which states we'd like use
	public enum States
	{
		Floating,
		Normal,
		UnderWater,
		UnderWater2
		//,Win,
		//Lose
	}
	
	//prototype scan
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	//public Animator animator;
	//////////////// end protoyupe scan ///////////////////
	
	public float rotationSpeed = 5.0f;
	public float currentRotation = 1.0f;
	
	public static TitanPlayerState Instance;
	public static Transform myTransform;
	public static Rigidbody myRigidbody;
	public float Speed = NORMALSPEED;
	public float JumpForce = NORMALJUMPSPEED;
	public float maxVelocityChange = 10.0f;
	public float waterLiftingForce = 1.55f;
	public float swimmingForce = 1.25f;
	public float gravityForce = 9.81f;
	public bool waterLifting = false;
	public bool grounded = false;
	public bool canJump = true;
	public bool isFloating = false;
	public TitanPlayerState.States pState = TitanPlayerState.States.Floating;
	private void Awake()
	{
		
		
		//Initialize State Machine Engine
		Initialize<States>();
		
		//Change to our first state
		ChangeState(States.Normal);
		
		// prototype defense
		//animator = GetComponent<Animator>();
		// end prototytpe
		
		myRigidbody = GetComponent<Rigidbody>() as Rigidbody;
		Instance = this;
		myTransform = this.transform;
		myRigidbody.freezeRotation = true;
		myRigidbody.mass = 10;
		
	}
	
	// Update is called once per frame
	void Update()
	{
		//if (pState ==null)
		UpdatePlayerState((TitanPlayerState.States) pState);
		GetLocomotionInput();
	}
	void FixedUpdate()
	{
		
		if (Input.GetKey(KeyCode.F))
		{
			
			print("Player pressed f key:" + pState);
			
			
			if (pState == TitanPlayerState.States.Normal)
			{
				Debug.Log("Setting state to Floating");
				TitanPlayerState.Instance.UpdatePlayerState(TitanPlayerState.States.Floating);
			}
			
			else if (pState == TitanPlayerState.States.Floating)
			{
				Debug.Log("Setting state to uw");
				TitanPlayerState.Instance.UpdatePlayerState(TitanPlayerState.States.UnderWater);
			}
			
			else if (pState == TitanPlayerState.States.UnderWater)
			{
				Debug.Log("Setting state to uw2");
				TitanPlayerState.Instance.UpdatePlayerState(TitanPlayerState.States.UnderWater2);
			}
			
			else if (pState == TitanPlayerState.States.UnderWater2)
			{
				Debug.Log("Setting state to floating");
				TitanPlayerState.Instance.UpdatePlayerState(TitanPlayerState.States.Normal);
			}
		}
		if (grounded && pState == TitanPlayerState.States.Normal)
		{
			//calculate how fast we should be moving
			
			Vector3 targetVelocity = new Vector3 (Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical") );
			targetVelocity = myTransform.TransformDirection(targetVelocity);
			targetVelocity *= Speed;
			
			//Apply a force that attempts to reach target velocity
			Vector3 velocity = myRigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			
			//force mode. add velocity
			myRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			
			//jump
			if (canJump && Input.GetKeyDown("space"))
			{
				
				print("Jumping");
				myRigidbody.AddForce((transform.up) * JumpForce, ForceMode.Impulse);
				
			}
			
		}
		
		//floating
		if (isFloating)
		{
			//calculate how fast we should be moving
			
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = myTransform.TransformDirection(targetVelocity);
			targetVelocity *= Speed;
			
			//Apply a force that attempts to reach target velocity
			Vector3 velocity = myRigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			
			//force mode. add velocity
			myRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			
			//add constant upward force
			myRigidbody.AddForce((Vector3.up * gravityForce), ForceMode.Acceleration);
			//Sinking
			if (isFloating && Input.GetKeyDown("c"))
			{
				
				print("Sinking");
				myRigidbody.AddForce((-transform.up) * JumpForce, ForceMode.Impulse);
				
			}
			
		}
		//end floating
		
		else if (pState != TitanPlayerState.States.Normal || pState != TitanPlayerState.States.Floating)
		{
			//calculate how fast we should be moving
			
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = myTransform.TransformDirection(targetVelocity);
			targetVelocity *= Speed;
			
			//Apply a force that attempts to reach target velocity
			Vector3 velocity = myRigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			
			//force mode. add velocity
			myRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			
			//Descend
			if (Input.GetButton("Descend"))
			{
				if (myRigidbody.velocity.y < 5.0f)
				{
					print("descending");
					//force mode.Impulse add an instant impulse using its mass
					myRigidbody.AddForce((-transform.up) * swimmingForce, ForceMode.Impulse);
				}
			}
			//Swim
			
			if (Input.GetButton("Jump")) 
			{
				if (myRigidbody.velocity.y < 5.0f)
				{
					print("ascending");
					//force mode.Impulse add an instant impulse using its mass
					myRigidbody.AddForce((transform.up) * swimmingForce, ForceMode.Impulse);
				}
			}
			myRigidbody.AddForce(Vector3.up * waterLiftingForce, ForceMode.Impulse);
		}
		
		grounded = false;
	}
	
	public void OnCollisionStay() 
	{
		grounded = true;
	}
	void GetLocomotionInput()
	{
		// Scanning Prototype
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
		// end scanning prototype
		
		
		//Will refactor post prototyping
		float inputR = Mathf.Clamp(Input.GetAxis("Mouse X"), -1.0f, 1.0f);
		
		if (Input.GetKeyDown(KeyCode.A))
		{
			currentRotation += 5;
		}
		
		if (Input.GetKeyDown(KeyCode.D))
		{
			currentRotation -= 5;
		}
		
		currentRotation = Helper.ClampedAngle(currentRotation + (inputR * rotationSpeed));
		Quaternion rotationAngle = Quaternion.Euler(0.0f, currentRotation, 0.0f);
		myTransform.rotation = rotationAngle;
	}
	
	public void UpdatePlayerState(TitanPlayerState.States state)
	{
		switch (state) 
		{ 
		case TitanPlayerState.States.Normal:
		{
			Speed = NORMALSPEED;
			JumpForce = NORMALJUMPSPEED;
			pState = TitanPlayerState.States.Normal;
			waterLifting = false;
			isFloating = false;
			break;
		}
		case TitanPlayerState.States.Floating:
		{
			Speed = NORMALSPEED;
			JumpForce = NORMALJUMPSPEED;
			pState = TitanPlayerState.States.Floating;
			canJump = false;
			isFloating = true;
			break;
		}
		case TitanPlayerState.States.UnderWater:
		{
			Speed = UWSPEED;
			JumpForce = UWJUMPSPEED;
			pState = TitanPlayerState.States.UnderWater;
			waterLifting = true;
			isFloating = false;
			break;
		}
		case TitanPlayerState.States.UnderWater2:
		{
			Speed = UW2SPEED;
			JumpForce = UW2JUMPSPEED;
			pState = TitanPlayerState.States.UnderWater2;
			waterLifting = true;
			isFloating = false;
			break;
		}
		default:
			break;
		}
	}
}


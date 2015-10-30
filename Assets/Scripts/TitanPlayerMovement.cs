using UnityEngine;
using System.Collections;

public class TitanPlayerMovement : MonoBehaviour {

    public static TitanPlayerMovement Instance; 
    public static Transform myTransform;
    public float rotationSpeed = 5.0f;
    public float currentRotation = 1.0f;
    public float MoveSpeed = 10f;
    public bool _isSwimming;
    public float Gravity = 21f;
    public float TerminalVelocity = 20f;
    public float VerticalVelocity { get; set; }
    /*
     * public float JumpSpeed = 6f;
     *   
    
     * 
     * public float Slide Threshold = 0.6f
     * public float MaxControllableSlideMagnitude = 0.4f;
     * 
     * private Vector3 slideDirection;
     */

    public Vector3 MoveVector { get; set; }

	// Use this for initialization
	void Awake() 
    {
        Instance = this;
        _isSwimming = false;
        
	}
	
	// Update is called once per frame
	public void UpdateMovement () 
    {
          ProcessMotion();
        //ProcessRotation();
	}


    void ProcessMotion ()
    {
        // Transform MoveVector to World Space
        MoveVector = transform.TransformDirection(MoveVector);

        // Normalize MoveVector if Magnitude > 1
        if (MoveVector.magnitude > 1)
        {
            MoveVector = Vector3.Normalize(MoveVector);
        }

        
        // Multiply MoveVector by MoveSpeed
        MoveVector *= MoveSpeed;

        // Multiply MoveVector by MoveSpeed
       // MoveVector *= Time.deltaTime

        // Reapply vertical velocity to move vector.y
        MoveVector = new Vector3(MoveVector.x, VerticalVelocity, MoveVector.z);

        // Apply gravity
            ApplyGravity();

        
        // Move the Character in World Space
        TitanPlayerController.CharacterController.Move(MoveVector * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (_isSwimming == false)
        {
            if (MoveVector.y > -TerminalVelocity)
                MoveVector = new Vector3(MoveVector.x, MoveVector.y - Gravity * Time.deltaTime, MoveVector.z);

            if (TitanPlayerController.CharacterController.isGrounded && MoveVector.y < -1)
                MoveVector = new Vector3(MoveVector.x, -1, MoveVector.z);
        }

        else if (_isSwimming == true)
            MoveVector = new Vector3(MoveVector.x, 1, MoveVector.z);
            //this creates buoyacy
            //MoveVector = new Vector3(MoveVector.x, MoveVector.y + Gravity * Time.deltaTime, MoveVector.z);
     } 
        
      

    public void IsSwimming(bool swim)
    {
        _isSwimming = swim;

    }

    public void Swim()
    {
        

        //if (TitanPlayerController.CharacterController.isGrounded && MoveVector.y < -1)
            //MoveVector = new Vector3(MoveVector.x, -1, MoveVector.z);
    }

}

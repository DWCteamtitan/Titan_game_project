using UnityEngine;
using System.Collections;

public class TitanPlayerController : MonoBehaviour
{
    public static CharacterController CharacterController;
    public static TitanPlayerController Instance;
    public static Transform myTransform;
    public float rotationSpeed = 5.0f;
    public float currentRotation = 1.0f;

    //public Rigidbody rb;//testing
   // public float torque; //testing theory
    //private float rotationSpeed = 5.0f;
    //private float currentRotation = 1.0f;
    //private float movementSpeed = 5.0f;
    

    // Use this for initialization
    void Awake()
    {
        CharacterController = GetComponent<CharacterController>() as CharacterController;
        Instance = this;
        myTransform = this.transform;

        /////////////////Work in Progress////////////////////
        // TitanCamera.UseExistingOrCreateNewMainCamera();
        //
        
        /*
        rb = GetComponent<Rigidbody>();
        */
        
    }

   
    /***** Depcriated prototyped stuff that is useless but maybe good for reference
         * 
         * I am keeping this stuff for prototyping/reference
         * will be removed when prototyping is over
         * 
         * // get inputs
         * float inputX = Input.GetAxis("Horizontal");
         * float inputY = Input.GetAxis("Vertical");
         * float inputR = Mathf.Clamp(Input.GetAxis("Mouse X"), -1.0f, 1.0f);
         *
         * // press 'Q' to turn 180 degrees
         * if (Input.GetKeyDown(KeyCode.Q))
         * {
         *   currentRotation += 180;
         * }
         *
         * // get current position and rotation, then do calculations
         * // position
         * Vector3 moveVectorX = myTransform.forward * inputY;
         * Vector3 moveVectorY = myTransform.right * inputX;
         * Vector3 moveVector = (moveVectorX + moveVectorY).normalized * movementSpeed * Time.deltaTime;

         * // rotation
         * currentRotation = ClampAngle(currentRotation + (inputR * rotationSpeed));
         * Quaternion rotationAngle = Quaternion.Euler(0.0f, currentRotation, 0.0f);
         *
         *  // update Character position and rotation
         *  myTransform.position = myTransform.position + moveVector;
         *   myTransform.rotation = rotationAngle;
         */

    // Update is called once per frame 
    void Update()
    {
        if (Camera.main == null)
            return;

        GetLocomotionInput();
        TitanPlayerMovement.Instance.UpdateMovement();
        //TitanPlayerMovement.Instance.UpdateRotation();
        
    }

   /* DON'T need fixed update for prototype but may play around with some more.
    * void FixedUpdate()
    {
        float turn = Input.GetAxis("Vertical");
        rb.AddTorque(transform.up * torque * turn);
    }*/

    void GetLocomotionInput()
    {
        var deadZone = 0.1f;

        TitanPlayerMovement.Instance.VerticalVelocity = TitanPlayerMovement.Instance.MoveVector.y;
        TitanPlayerMovement.Instance.MoveVector = Vector3.zero;
        
       
        if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone)
            TitanPlayerMovement.Instance.MoveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
            TitanPlayerMovement.Instance.MoveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        //Will refactor post prototyping
        float inputR = Mathf.Clamp(Input.GetAxis("Mouse X"), -1.0f, 1.0f);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentRotation += 180;
        }

        currentRotation = Helper.ClampedAngle(currentRotation + (inputR * rotationSpeed));
        Quaternion rotationAngle = Quaternion.Euler(0.0f, currentRotation, 0.0f);
        myTransform.rotation = rotationAngle;
    }

    
    /******** "Swimming Stuff" *****************
     * 
     */
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
            SendMessage("IsSwimming", true);
    }
    public void OnTriggerExit(Collider other)
     {
        if (other.CompareTag("Water"))
            SendMessage("IsSwimming", false);
    }
}

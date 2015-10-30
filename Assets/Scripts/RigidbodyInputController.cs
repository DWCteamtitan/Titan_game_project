using UnityEngine;
using System.Collections;
using TitanMaria.StateMachine;

public class RigidbodyInputController : MonoBehaviour
{
    private const float NORMALSPEED = 5.0f;
    private const float NORMALJUMPSPEED = 30.0f;
    private const float UWSPEED = 3.0f;
    private const float UWJUMPSPEED = 5.0f;
    private const float UW2SPEED = 5.0f;
    private const float UW2JUMPSPEED = 8.0f;

    public static RigidbodyInputController Instance;
    public static Transform myTransform;
    public static Rigidbody myRigidbody;
    public float Speed = NORMALSPEED;
    public float JumpForce = NORMALJUMPSPEED;
    public float maxVelocityChange = 10.0f;
    public float waterLiftingForce = 1.55f;
    public float swimmingForce = 1.25f;
    public bool waterLifting = false;
    public bool grounded = false;
    public bool canJump = true;

    public TitanPlayerState.States pState = TitanPlayerState.States.Normal;


    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>() as Rigidbody;
        Instance = this;
        myTransform = this.transform;
        myRigidbody.freezeRotation = true;
        myRigidbody.mass = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            print("Player pressed f key:" + pState);

        }

        if (pState == TitanPlayerState.States.UnderWater)
        {
            Debug.Log("Setting state to uw");
        }

        else if (pState == TitanPlayerState.States.UnderWater2)
        {
            Debug.Log("Setting state to uw2");
        }

        if (grounded && pState == TitanPlayerState.States.Normal)
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

            //jump
            //if(canJump )
            //myRigidbody.AddForce((transform.up))


        }
        else if (pState != TitanPlayerState.States.Normal)
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

            //jump
            if (canJump && Input.GetKeyDown("space"))
            {
               
                print("Jumping");
                myRigidbody.AddForce((transform.up) * JumpForce, ForceMode.Impulse);
                
            }
        }

    }
}

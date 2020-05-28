using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipController : MonoBehaviour
{

     IControllerInput playerInput;
    public GameObject pilot;
    public float forwardThrustPower = 100f;
    public float yawSpeed = 10f;
    public float pitchSpeed = 10f;
    public float rollSpeed = 10f;
    public float maxVelocity = 500f;
    
    Rigidbody myRigidbody;
    float originalDrag;

    Weapon[] myWeapons;

    AudioSource laserBeam;
    // Start is called before the first frame update

    private void Awake()
    {
       

        if (!pilot) pilot = gameObject;

        if (pilot)
        {
            playerInput = pilot.GetComponent<IControllerInput>();

            playerInput.ForwardEvent += ForwardThrust;
            playerInput.YawEvent += YawMovement;
            playerInput.PitchEvent += PitchMovement;
            playerInput.RollEvent += RollMovement;
            playerInput.VerticalStrafeEvent += VerticalStrafeMovement;
            playerInput.SideStrafeEvent += SideStrafeMovement;
            playerInput.SlideEvent += EnableSlide;
            playerInput.FireEvent += FireWeapon;
            playerInput.TurnEvent += TurnToTarget;
        }
        else
        {
            Debug.LogError("No pilot on", gameObject);
        }

    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        originalDrag = myRigidbody.drag;

        myWeapons = transform.GetComponentsInChildren<Weapon>();

        laserBeam = GetComponent<AudioSource>();
    }

    private void FireWeapon()
    {
        if (myWeapons.Length > 0)
        {
            for(int i = 0; i < myWeapons.Length; i ++)
            {
                myWeapons[i].Fire(myRigidbody.velocity);
               
            }

        }
    } 
    private void TurnToTarget(float x, float y, float z)
    {
        Vector3 desiredHeading = new Vector3(x, y, z);

        Quaternion rotationGoal = Quaternion.LookRotation(desiredHeading);

        float step = yawSpeed * Time.deltaTime; //degrees per second that target is moving

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationGoal, step);
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnableSlide(float slide)
    {
        if (slide > 0)
        {
            myRigidbody.drag = 0;
        }
        else
        {
            myRigidbody.drag = originalDrag;
        }
    }
    public void ForwardThrust(float thrust)
    {
        myRigidbody.AddForce(gameObject.transform.forward * thrust * forwardThrustPower * Time.deltaTime);

        if(myRigidbody.velocity.magnitude > maxVelocity)
        {
            myRigidbody.velocity = myRigidbody.velocity.normalized * maxVelocity;
        }
    }
    public void SideStrafeMovement(float thrust)
    {
        myRigidbody.AddForce(gameObject.transform.right * thrust * forwardThrustPower * Time.deltaTime);
    }
    public void VerticalStrafeMovement(float thrust)
    {
        myRigidbody.AddForce(gameObject.transform.up * thrust * forwardThrustPower * Time.deltaTime);
    }
    public void YawMovement(float Yaw)
    {
        myRigidbody.AddTorque(gameObject.transform.up * Yaw * yawSpeed * Time.deltaTime);
    }
    public void PitchMovement(float pitch)
    {
        myRigidbody.AddTorque(gameObject.transform.right * pitch * pitchSpeed * Time.deltaTime);
    }
    public void RollMovement(float roll)
    {
        myRigidbody.AddTorque(gameObject.transform.forward * roll * rollSpeed * Time.deltaTime);
    }

}

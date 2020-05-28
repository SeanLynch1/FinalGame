// Directives
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour, IControllerInput 
{

    public event InputEventFloat SlideEvent;
    public event InputEventFloat ForwardEvent;
    public event InputEventFloat YawEvent;
    public event InputEventFloat PitchEvent;
    public event InputEventFloat RollEvent;
    public event InputEventFloat SideStrafeEvent;
    public event InputEventFloat VerticalStrafeEvent;
    public event InputEvent FireEvent;
    public event InputEventVector3 TurnEvent;

    public float deadZoneRadius = .10f;
    public float invertModifier = -1;

    AudioSource laserBeam;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Screen Width : " + Screen.width);
        laserBeam = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetKeyboardInput();
        GetMouseInput();
    }

    void GetKeyboardInput()
    {
        if (ForwardEvent != null)
        {
            if (Input.GetAxis("Vertical") != 0) ForwardEvent(Input.GetAxis("Vertical"));
        }

        if (VerticalStrafeEvent != null)
        {
            if (Input.GetAxis("VerticalStrafe") != 0) VerticalStrafeEvent(Input.GetAxis("VerticalStrafe") * invertModifier);

        }
        if (SideStrafeEvent != null)
        {
            if (Input.GetAxis("Horizontal") != 0) SideStrafeEvent(Input.GetAxis("Horizontal"));
        }

        if (RollEvent != null)
        {
            if (Input.GetAxis("Roll") != 0) RollEvent(Input.GetAxis("Roll"));
        }
        if (SlideEvent != null)
        {
            if (Input.GetButton("Slide")) SlideEvent(1);
            if (Input.GetButtonUp("Slide")) SlideEvent(0);
        }

    }

    void GetMouseInput()
    {

        if(FireEvent != null)
        {
            if (Input.GetMouseButton(0))
            {
                FireEvent();
               
            }

        }

        Vector3 mousePosition = Input.mousePosition;
        
        //strength of yaw
        float yaw = (mousePosition.x - (Screen.width * .5f)) / (Screen.width * .5f);
        if (Mathf.Abs(yaw) < deadZoneRadius)
        {
            yaw = 0;
        }
       
        if (YawEvent != null)
        {

            if (yaw != 0)
            {
                YawEvent(yaw);
            }
        }
        float pitch = (mousePosition.y - (Screen.height * .5f)) / (Screen.height * .5f) * invertModifier;
        if (Mathf.Abs(pitch) < deadZoneRadius)
        {
            pitch = 0;
        }
        
        if (PitchEvent != null)
        {

            if (pitch != 0)
            {
                PitchEvent(pitch);
            }
        }

    }

    }


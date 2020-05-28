using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVehicle : MonoBehaviour
{
    public GameObject player;
    public GameObject vehicle;
    private bool inVehicle = false;

    GameObject guiObject;
    GameObject guiHealthObject;
    GameObject shipContolsCanvas;
    GameObject playerManagerObject;

    AudioSource shipEngine;
    shipController shipControllerScript;

    bool GravityState = false;

    
    Rigidbody vr;
    Rigidbody RB;
    void Start()
    {
        RB = vehicle.GetComponent<Rigidbody>();

        player.SetActive(true);
        player.transform.parent = null;
        shipControllerScript = GetComponent<shipController>();
        shipControllerScript.enabled = false;

        guiObject = GameObject.Find("PressG");
        guiObject.SetActive(false);

        guiHealthObject = GameObject.Find("HealthBarBackground");
        guiHealthObject.SetActive(false);
        shipEngine = GetComponent<AudioSource>();
        

        shipContolsCanvas = GameObject.Find("ShipControlsCanvas");
        shipContolsCanvas.SetActive(false);

        playerManagerObject = GameObject.Find("PlayerManager");
        playerManagerObject.SetActive(false);

        vr = vehicle.GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        if (inVehicle == true && Input.GetKey(KeyCode.H))
        {
            Debug.Log("Exiting vehicle");
            player.SetActive(true);
            player.transform.parent = null;
            
            vehicle.SetActive(true);
            shipControllerScript.enabled = false;
            playerManagerObject.SetActive(false);

            inVehicle = false;

            vehicle.tag = "Untagged";

            shipContolsCanvas.SetActive(false);
            guiHealthObject.SetActive(false);

            shipEngine.Stop();

            GravityState = true;
            if (GravityState == true)
            {
                Debug.Log("Gravity is on");
                vr.useGravity = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && inVehicle == false)
        {
            guiObject.SetActive(true);
        }
        if(other.gameObject.tag == "Player" && inVehicle == false && Input.GetKey(KeyCode.G))
        {
            Debug.Log("Entering Ship");
            guiObject.SetActive(false);
            player.SetActive(false);
            player.transform.parent = vehicle.transform;
            shipControllerScript.enabled = true;
            vehicle.tag = "PlayerFaction";

            playerManagerObject.SetActive(true);

            shipContolsCanvas.SetActive(true);
            guiHealthObject.SetActive(true);
            inVehicle = true;

            shipEngine.Play();

            RB.isKinematic = false;

            GravityState = false;
            if (GravityState == false)
            {
                Debug.Log("Gravity is on");
                vr.useGravity = false;
            }
        
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            guiObject.SetActive(false); 
        }
    }
   
}

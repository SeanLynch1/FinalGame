using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadShip : MonoBehaviour
{
    private bool onPanel;

    public GameObject ship;
    public GameObject loadParticleEffect;
    GameObject guiLoad;

    Rigidbody RB;

    public AudioSource[] loadShip;
    // Start is called before the first frame update
    void Start()
    {
        RB = ship.GetComponent<Rigidbody>();
        guiLoad = GameObject.Find("PressL");
        guiLoad.SetActive(false);
        loadShip = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
     

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            guiLoad.SetActive(true);
            onPanel = true;
        }
    
        if (other.gameObject.tag == "Player" && onPanel && Input.GetKey(KeyCode.L))
        {
            loadShip[1].Play();
            Instantiate(loadParticleEffect, transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity);

            StartCoroutine(firstCoroutine());
            guiLoad.SetActive(false);
        }
        if(other.gameObject.name == "Ship")
        {
            guiLoad.SetActive(false);
        }
      
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerFaction")
        {
            guiLoad.SetActive(false);
            onPanel = false;
        }
    }
    IEnumerator firstCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(4.8f);
        loadShip[0].Play();
       
        ship.transform.position = transform.position + new Vector3(0, 0, 0);
        ship.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        RB.isKinematic = true;
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}

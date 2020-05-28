using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGravityBody : MonoBehaviour
{
    public PlanetScript attractorPlanet;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {

        playerTransform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(attractorPlanet)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            attractorPlanet.Attract(playerTransform);
        }
    }
}

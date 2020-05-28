using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraScript : MonoBehaviour
{
    public float rotationSpeed = 2f;
    public Transform target;
    public Transform player;
    float mouseX;
    float mouseY;

    public Transform obstructionObject;
    

    // Start is called before the first frame update
    void Start()
    {
        obstructionObject = target;
    }

    // Update is called once per frame
    void Update()
    {
        cameraControl();
        //ViewObstructed();
    }

    void cameraControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        if (Input.GetMouseButton(1))
        {
            target.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(0, mouseX, 0);
            target.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
    }

    void ViewObstructed()
    {
        RaycastHit hit; //Structure used to get information back from a raycast.

        if (Physics.Raycast(transform.position,target.position - transform.position, out hit, 4.5f)) //Casts a ray, from point origin, in direction direction, of length maxDistance, against all colliders in the Scene.
        {
            Debug.Log(hit +  obstructionObject.name);
            if (hit.collider.tag != "Player")
            {
                obstructionObject = hit.transform;
                obstructionObject.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
               
            }
        }
        else
        {
           
            obstructionObject.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
           
        }
    }
}

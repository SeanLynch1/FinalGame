using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 0.1f;
    VectorMovement vm = new VectorMovement();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += vm.Movement(speed);

    }

    public class VectorMovement
    {

        public Vector3 Movement(float dist)
        {
            Vector3 vec = new Vector3();

            if (Input.GetKey(KeyCode.W))
            {
                vec.z = vec.z - dist;
            }
            if (Input.GetKey(KeyCode.S))
            {
                vec.z = vec.z + dist;
            }
            if (Input.GetKey(KeyCode.A))
            {
                vec.x += dist;
            }
            if (Input.GetKey(KeyCode.D))
            {
                vec.x -= dist;
            }

            return vec;
        }
    }
}

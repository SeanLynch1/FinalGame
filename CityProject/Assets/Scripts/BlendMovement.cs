using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendMovement : MonoBehaviour
{
   

    public GameObject player;

    private Animator _animator;

    CapsuleCollider collider;
    public float distToGround;

    public AudioSource[] blendSounds;
    // Start is called before the first frame update
    void Start()
    {
      
        blendSounds = GetComponents<AudioSource>();
        blendSounds[0].enabled = true;
        blendSounds[1].enabled = true;
       
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        collider = GetComponent<CapsuleCollider>();
        distToGround = collider.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator == null) return;

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        if(Physics.Raycast(transform.position, Vector3.down, distToGround))
        {
            
            _animator.enabled = true;
            _animator.SetBool("Jump", false);
            blendSounds[0].enabled = true;
            blendSounds[1].enabled = true;

        }
        else if (Physics.Raycast(transform.position, Vector3.down, distToGround) == false)
        {
            
            _animator.SetBool("Jump", true);
            
            blendSounds[0].enabled = false;
            blendSounds[1].enabled = false;
        }
        Move(x, y);
        ActivateBlendStates();

    }
    
    public void Move(float x, float y)
    {
        _animator.SetFloat("VelX", x);
        _animator.SetFloat("VelY", y);

        
    }

    public void ActivateBlendStates()
    {
        //ACTIVATE BLEND STATE
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _animator.SetInteger("Condition", 1);
        }
        else
        {
            _animator.SetInteger("Condition", 0);
        }
        //SPRINT
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Sprint");
            _animator.SetInteger("Condition", 2);
        }


    }


    //All we're doing is taking the axis from our controller and sending it to our animator

}

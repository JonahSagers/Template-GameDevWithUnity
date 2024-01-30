using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLineMove : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask ground;

    void Start()
    {

    }

    void Update()
    {
        //move
        rb.velocity += Input.GetAxisRaw("Horizontal") * transform.right * Time.deltaTime * 25;
        rb.velocity += Input.GetAxisRaw("Vertical") * transform.forward * Time.deltaTime * 25;
        //jump
        if(Input.GetKeyDown(KeyCode.Space) && Physics.CheckSphere(transform.position + Vector3.down, 0.4f, ground)){
            rb.velocity += transform.up * 10;
        }
    }
}

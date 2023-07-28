using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask ground;
    public GameObject cam;

    void Start()
    {
        //init
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //move
        rb.velocity += Input.GetAxisRaw("Horizontal") * transform.right * Time.deltaTime * 50;
        rb.velocity += Input.GetAxisRaw("Vertical") * transform.forward * Time.deltaTime * 50;
        //jump
        if(Input.GetKeyDown(KeyCode.Space) && Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.4f, ground)){
            rb.velocity += transform.up * 10;
        }
        //dash
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            rb.velocity +=Input.GetAxisRaw("Horizontal") * transform.right * 60 + Input.GetAxisRaw("Vertical") * transform.forward * 60;
        }
        //artifical drag, so that movement feels snappier
        rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0.95f,1,0.95f));
        //camera
        transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * 70);
        cam.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x - (Input.GetAxis("Mouse Y") * Time.deltaTime * 125), transform.eulerAngles.y, transform.eulerAngles.z);
    }
}

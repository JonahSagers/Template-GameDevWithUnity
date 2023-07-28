using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask ground;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity += Input.GetAxisRaw("Horizontal") * transform.right * Time.deltaTime * 50;
        rb.velocity += Input.GetAxisRaw("Vertical") * transform.forward * Time.deltaTime * 50;
        if(Input.GetKeyDown(KeyCode.Space) && Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.4f, ground)){
            rb.velocity += transform.up * 10;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            rb.velocity +=Input.GetAxisRaw("Horizontal") * transform.right * 60 + Input.GetAxisRaw("Vertical") * transform.forward * 60;
        }
        rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0.95f,1,0.95f));
        transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X"));
        cam.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x - Input.GetAxis("Mouse Y"), transform.eulerAngles.y, transform.eulerAngles.z);
    }
}

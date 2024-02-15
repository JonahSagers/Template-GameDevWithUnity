using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask ground;
    public GameObject cam;

    public bool dashAvailable;
    public bool isGrounded;

    public float sensitivity;

    public ParticleSystem burstParticles;
    public ParticleSystem walkParticles;

    void Start()
    {
        //set game values
        dashAvailable = true;
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //This can be one line, but this makes more logical sense for a lesson
        if(Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.4f, ground)){
            isGrounded = true;
        } else {
            isGrounded = false;
        }
        //move
        rb.velocity += Input.GetAxisRaw("Horizontal") * transform.right * Time.deltaTime * 50;
        rb.velocity += Input.GetAxisRaw("Vertical") * transform.forward * Time.deltaTime * 50;
        //jump
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.velocity += transform.up * 20;
        }
        //dash
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashAvailable){
            StartCoroutine(Dash(2,25));
        }
        //camera
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + (Input.GetAxis("Mouse X") * Time.deltaTime * 70 * sensitivity), 0);
        cam.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x - (Input.GetAxis("Mouse Y") * Time.deltaTime * 150 * sensitivity), transform.eulerAngles.y, 0);

        var emission = walkParticles.emission;
        if(isGrounded && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) ){
            emission.rateOverDistance = 1;
        } else {
            emission.rateOverDistance = 0;
        }
    }

    void FixedUpdate()
    {
        //artifical drag, so that movement feels snappier
        rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0.95f,1,0.95f));
        rb.velocity += Vector3.down / 2;
    }

    IEnumerator Dash(float cooldown, float distance)
    {
        burstParticles.Play();
        rb.velocity += Input.GetAxisRaw("Horizontal") * transform.right * distance + Input.GetAxisRaw("Vertical") * transform.forward * distance;
        dashAvailable = false;
        yield return new WaitForSeconds(cooldown);
        dashAvailable = true;
        yield return null;
    }
}

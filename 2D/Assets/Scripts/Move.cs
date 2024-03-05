using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask ground;
    public ParticleSystem particles;
    public bool dashCD;
    public Vector3 mousePosition;
    public GameObject afterimage;
    // Start is called before the first frame update
    void Start()
    {
        // rb.mass = 10;
        // rb.drag = 10;
        // rb.angularDrag = 10;
        // rb.gravityScale = 10;
        // rb.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition,20,ground);
        // Debug.DrawLine(transform.position, mousePosition);
        // if(hit){
        //     Debug.Log(hit.collider);
        // }
        particles.gameObject.transform.position = transform.position;
        rb.velocity += Input.GetAxisRaw("Horizontal") * Vector2.right * Time.deltaTime * 20;
        if(Input.GetKeyDown(KeyCode.Space) && Physics2D.OverlapCircle(transform.position + Vector3.down, 0.5f, ground)){
            rb.velocity = new Vector2(rb.velocity.x, 15);
            particles.Play();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashCD == false){
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 20, 2);
            StartCoroutine(DashCooldown(1));
        }
        if(Input.GetKey(KeyCode.LeftAlt)){
            rb.gravityScale = 10;
        } else {
            rb.gravityScale = 2;
        }
    }

    public IEnumerator DashCooldown(float cooldown){
        dashCD = true;
        int i = 0;
        while(i < 8){
            Instantiate(afterimage, transform.position, transform.rotation, null);
            yield return new WaitForSeconds(0.05f);
            i += 1;
        }
        yield return new WaitForSeconds(cooldown - 0.5f);
        dashCD = false;
    }

    void OnCollisionEnter2D(Collision2D collider){
        Debug.Log(collider.gameObject.name);
    }
}

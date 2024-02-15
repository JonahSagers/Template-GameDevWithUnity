using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask ground;
    public ParticleSystem particles;
    public bool dashCD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        particles.gameObject.transform.position = transform.position;
        rb.velocity += Input.GetAxisRaw("Horizontal") * Vector2.right * Time.deltaTime * 20;
        if(Input.GetKeyDown(KeyCode.Space) && Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.4f, ground)){
            rb.velocity = new Vector2(rb.velocity.x, 15);
            particles.Play();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashCD == false){
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 20, rb.velocity.y);
            StartCoroutine(DashCooldown());
        }
    }

    public IEnumerator DashCooldown(){
        dashCD = true;
        yield return new WaitForSeconds(1);
        dashCD = false;
        
    }
}

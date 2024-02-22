using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    public float velocity;
    public SpriteRenderer render;
    public float opacity;
    // Start is called before the first frame update
    public IEnumerator Start()
    {
        opacity = 1;
        while(opacity > 0){
            // render.material.color.a -= 1;
            opacity -= 0.01f;
            render.color = new Color(1f, 1f, 1f, opacity);
            transform.eulerAngles += Vector3.forward * velocity;
            yield return 0;
        }
        Destroy(gameObject);
    }
}

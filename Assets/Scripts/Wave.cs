using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float waitBeforeDestroy;
    public Vector2 mov;
    public float speed;
    Animator anim;
    void Start()
    {
        anim.GetComponent<Animator>();
        anim.SetTrigger("active");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(mov.x, mov.y, 0) * speed * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class player : MonoBehaviour
{
    public float speed = 2f;
    Animator anim;
    Rigidbody2D rb2d;
    Vector2 mov;
    CircleCollider2D attackCollider;
    //public GameObject initialMap;

    void Awake()
    {
        //Assert.IsNotNull(initialMap);
    }


    void Start()
    {
        Camera.main.GetComponent<MainCamera>().updateLimit(-16, 3, 6, -12);
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
    }


    void Update()
    {
        mov = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            );

        if(mov != Vector2.zero)
        {
            anim.SetFloat("movx", mov.x);
            anim.SetFloat("movy", mov.y);
            anim.SetBool("walking", true);
        }else
        {
            anim.SetBool("walking", false);
        }

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool validate = stateInfo.IsName("player_attack");
        if (Input.GetKeyDown(KeyCode.Mouse0) && !validate)
        {
            anim.SetTrigger("slash-trigger");      
        }
        if(mov != Vector2.zero)
        {
            attackCollider.offset = new Vector2(mov.x / 3, mov.y / 3);
        }
        if (validate)
        {
            float playbackTime = stateInfo.normalizedTime;
            if (playbackTime > 0.4 && playbackTime < 0.7)
            {
                attackCollider.enabled = true;
            }
            else
            {
                attackCollider.enabled = false;
            }
        }
    }
    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
    }
}


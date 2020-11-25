using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lina : MonoBehaviour
{
    GameObject player;
    Animator anim;
    bool enter;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && enter)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            anim.SetBool("dir", true);
            anim.SetFloat("movx", dir.x);
            anim.SetFloat("movy", dir.y);
        }
        else
        {
           // anim.SetBool("dir", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enter = false;
        }
    }

}

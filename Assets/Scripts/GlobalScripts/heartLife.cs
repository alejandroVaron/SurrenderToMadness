using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartLife : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            anim.Play("destroy");
            yield return new WaitForSeconds(0.41f);
            Destroy(gameObject);
        }
    }
}

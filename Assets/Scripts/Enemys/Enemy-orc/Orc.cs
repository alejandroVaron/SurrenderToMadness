using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Orc : MonoBehaviour
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public float moveSpeed;
    Animator anim;
    public float value;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        checkDistance();
    }

    void checkDistance()
    {
        
        value = 0;
        value = Vector2.Distance(target.position, transform.position);
        //Debug.Log("Este es el cambio " + value + "El target.position es: "+ target.position+ "El transform.position"+ transform.position);
        if (value <= chaseRadius)
        {
            if(value > attackRadius) 
            {
                Vector3 dir = (target.position - transform.position).normalized;
                anim.SetBool("perseguir", true);
                anim.SetFloat("movx", dir.x);
                anim.SetFloat("movy", dir.y);
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }
            if(value <= attackRadius){
                anim.SetBool("atacar", true);
            } else {
                anim.SetBool("atacar", false);
            }
        }
        else
        {
            anim.SetBool("perseguir", false);
        }
    }
}

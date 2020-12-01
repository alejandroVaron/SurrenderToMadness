using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Orc : MonoBehaviour
{
    public float visionRadius;
    public float attackRadius;
    public float speed;
    GameObject player;
    Vector3 initialPosition;
    Animator anim;
    Rigidbody2D rb2d;
    public GameObject slashPrefab;
    bool reset = true;
    bool inProcess = false;
    public int maxHp = 3;
    public int hp;
    bool stopMov = true;
    bool death = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = initialPosition;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, visionRadius, 1 << LayerMask.NameToLayer("Default"));

        //debug
        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        //si encuentra al player
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                target = player.transform.position;
                anim.SetBool("perseguir", true);
            }
        }
        else
        {
            anim.SetBool("perseguir", false);
        }
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (player.transform.position - transform.position).normalized;
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                anim.SetBool("perseguir", true);
                anim.SetFloat("movx", dir.x);
                anim.SetFloat("movy", dir.y);
            }
        }
        //Rango de ataque
        if (target != initialPosition && distance < attackRadius)
        {
            anim.SetBool("perseguir", false);
            if (reset)
            {
                anim.SetTrigger("atacar");
                StartCoroutine("resetShoot");
                /*
                float angle = Mathf.Atan2(
                    anim.GetFloat("movy"),
                    anim.GetFloat("movx")
                    ) * Mathf.Rad2Deg;
                GameObject slashObj = Instantiate(
                    slashPrefab, transform.position,
                    Quaternion.AngleAxis(angle, Vector3.forward)
                    );
                orcslash shoot = slashObj.GetComponent<orcslash>();
                shoot.mov.x = anim.GetFloat("movx");
                shoot.mov.y = anim.GetFloat("movy");
                */
            }
        }
        else if(!death)
        {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);
            //Se mueve
        }
        if (target == initialPosition && distance < 0.02f)
        {
            transform.position = initialPosition;
            anim.SetBool("walk", false);
            //idle 
        }
        Debug.DrawLine(transform.position, target, Color.green);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    IEnumerator resetShoot()
    {
        if (!inProcess)
        {
            Debug.Log("Golpea");
            inProcess = true;
            reset = false;
            yield return new WaitForSeconds(0.51f);
            reset = true;
            inProcess = false;
        }
    }
    public IEnumerator Attacked()
    {
        if (--hp <= 0)
        {
            death = true;
            anim.Play("orc-death");
            stopMov = false;
            yield return new WaitForSeconds(1.15f);
            Destroy(gameObject);
        }
    }
    private void OnGUI()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(
            new Rect(
                pos.x - 20,
                Screen.height - pos.y + 60,
                40,
                24
                ), hp + "/" + maxHp
            );
    }
}

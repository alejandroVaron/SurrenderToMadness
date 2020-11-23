using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
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
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        hp = maxHp;
    }

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
                Debug.Log("entree");
                target = player.transform.position;
                anim.SetBool("walk", true);
            }
            else
            {
                anim.SetBool("walk", false);
            }
        }
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                anim.SetFloat("movx", dir.x);
                anim.SetFloat("movy", dir.y);
            }
        }
        //Rango de ataque
        if (target != initialPosition && distance < attackRadius)
        {
            if (reset)
            {
                StartCoroutine("resetShoot");
                float angle = Mathf.Atan2(
                    anim.GetFloat("movy"),
                    anim.GetFloat("movx")
                    ) * Mathf.Rad2Deg;
                GameObject slashObj = Instantiate(
                    slashPrefab, transform.position,
                    Quaternion.AngleAxis(angle, Vector3.forward)
                    );
                Shoot shoot = slashObj.GetComponent<Shoot>();
                shoot.mov.x = anim.GetFloat("movx");
                shoot.mov.y = anim.GetFloat("movy");
            }
        }
        else
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
            inProcess = true;
            reset = false;
            yield return new WaitForSeconds(1);
            reset = true;
            inProcess = false;
        }
    }
    public IEnumerator Attacked()
    {
        if (--hp <= 0)
        {
            anim.Play("batDeath");
            stopMov = false;
            yield return new WaitForSeconds(0.45F);
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

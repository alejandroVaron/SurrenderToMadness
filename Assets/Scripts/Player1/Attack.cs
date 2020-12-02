using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    public float thrust;
    float shakeAmount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<AudioManager>().Play("PlayerSwordSlashHit");
        if(collision.tag == "Enemy")
        {
            shake(0.1f, 0.1f);
            collision.SendMessage("Attacked");
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                StartCoroutine(KnockCoroutine(enemy));
            }
        }
    }
    private IEnumerator KnockCoroutine(Rigidbody2D enemy)
    {
        Vector2 forceDirection = enemy.transform.position - transform.position;
        Vector2 force = forceDirection.normalized * thrust;

        enemy.velocity = force;
        yield return new WaitForSeconds(.3f);

        enemy.velocity = new Vector2();
    }
    public void shake(float ant, float length)
    {
        shakeAmount = ant;
        InvokeRepeating("beginShake", 0, 0.01f);
        Invoke("stopShake", length);
    }
    void beginShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = Camera.main.transform.position;
            float shakAmtx = Random.value * shakeAmount * 2 - shakeAmount;
            float shakAmty = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += shakAmtx;
            camPos.y += shakAmty;
            Camera.main.transform.position = camPos;
        }
    }
    void stopShake()
    {
        CancelInvoke("beginShake");
        Camera.main.transform.localPosition = Camera.main.GetComponent<MainCamera>().getPositionActual();
    }
}

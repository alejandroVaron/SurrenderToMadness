using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float waitBeforeDestroy;
    public Vector2 mov;
    public float speed;
    void Start()
    {
    }

    void Update()
    {
        transform.position += new Vector3(mov.x, mov.y, 0) * speed * Time.deltaTime;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Object" || collision.tag == "wall")
        {
            yield return new WaitForSeconds(waitBeforeDestroy);
            Destroy(gameObject);
        }
        else if (collision.tag == "Player" && collision.tag != "Attack")
        {
            Destroy(gameObject);
        }
        else
        {
            yield return new WaitForSeconds(6);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdScript : MonoBehaviour
{
    public Vector3 mov;
    public float speed;
    public string direction;
    // Start is called before the first frame update
    void Start()
    {
        if(direction == "l")
        {
            GetComponent<Animator>().Play("fly-left");
        }else if(direction == "r")
        {
            GetComponent<Animator>().Play("fly-right");
        }
        destroyObj();

    }
    IEnumerator destroyObj()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(mov.x, mov.y, 0) * speed * Time.deltaTime;
    }
}

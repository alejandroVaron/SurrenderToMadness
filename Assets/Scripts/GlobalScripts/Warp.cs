using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Warp : MonoBehaviour
{
    public float x, y, dx, dy;
    public GameObject target;
    public GameObject minimap;
    // Start is called before the first frame update

    void Awake()
    {
        Assert.IsNotNull(target);

        //GetComponent<SpriteRenderer>().enabled = false;
        //transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("El target.transform.position es: " + target.transform.position);
            other.transform.position = target.transform.position;
          
            Camera.main.GetComponent<MainCamera>().updateLimit(x, y, dx, dy);
            minimap.GetComponent<MainCamera>().updateLimit(x, y, dx, dy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showBirdMedium : MonoBehaviour
{
    public GameObject birds;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            birds.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

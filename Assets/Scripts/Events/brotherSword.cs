using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brotherSword : MonoBehaviour
{
    bool enter;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enter)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                panel.gameObject.SetActive(true);
                StartCoroutine("hideObject");
            }
        }
    }
    IEnumerator hideObject()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
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

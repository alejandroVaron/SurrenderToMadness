using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area : MonoBehaviour
{
    Animator anim;
    public GameObject shadow;
    public GameObject label;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public IEnumerator showText(string text)
    {
        anim.Play("area_show");
        transform.GetChild(0).GetComponent<Text>().text = text;
        transform.GetChild(1).GetComponent<Text>().text = text;
        yield return new WaitForSeconds(1f);
        anim.Play("area_fadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

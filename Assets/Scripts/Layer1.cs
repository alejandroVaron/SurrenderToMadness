using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer1 : MonoBehaviour
{
    public GameObject target;

    public GameObject targetMap;
    // Start is called before the first frame update
    void Start()
    {
        //Camera.main.GetComponent<MainCamera>().updateLimit(targetMap);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("eeweqe");
        //Camera.main.GetComponent<MainCamera>().updateLimit(targetMap);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

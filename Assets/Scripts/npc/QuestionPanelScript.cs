using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPanelScript : MonoBehaviour
{
    bool response = false;
    public int valueResponse = 0;
    public GameObject lina;
    public string responses;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void acceptResponse()
    {
        Debug.Log("La respuesta es si");
        response = true;
        lina.GetComponent<linaScript>().response = 1;
    }
    public void declineResponse()
    {
        Debug.Log("La respuesta es no");
        response = true;
        valueResponse = 2;
        lina.GetComponent<linaScript>().response = 2;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

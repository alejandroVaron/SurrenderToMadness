using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public Image fillBar;
    public float health;

    public void Start()
    {
        fillBar.fillAmount = health / 100;
    }



    public void loseHealth(int value)
    {
        if(health <= 0)
        {
            return;
        }
        health -= value;
        Debug.Log(health/100);
        fillBar.fillAmount = health / 100;
        if (health <= 0)
        {
            Debug.Log("Moriste");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 initialScale;
    public float scalex;
    public float scaley;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(scalex+0.30f, scaley+ 0.30f, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        initialScale = new Vector3(scalex, scaley, 1);
        transform.localScale = initialScale;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

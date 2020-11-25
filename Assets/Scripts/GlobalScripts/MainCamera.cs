using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float smoothTime = 3f;
    Transform target;
    //Vector2 velocity;
    //public Animator anim;

    private float tlx, tly, brx, bry;
    private Vector3 velocity;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, tlx, brx) , 
            Mathf.Clamp(target.position.y, bry, tly), 
            transform.position.y-100
            );
    }

    public void updateLimit(float x, float y, float z, float  u)
    {
        float cameraSize = Camera.main.orthographicSize;
        tlx = x + cameraSize;
        tly = y - cameraSize;
        brx = z - cameraSize;
        bry = u + cameraSize;

    }
}

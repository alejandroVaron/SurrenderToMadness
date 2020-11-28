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
    Vector2 velocitys;

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
        float posx = Mathf.Round( Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocitys.x, 0.3f) * 100) / 100;
        float posy = Mathf.Round(Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocitys.y, 0.3f) * 100) / 100;

        transform.position = new Vector3(
            Mathf.Clamp(posx, tlx, brx) , 
            Mathf.Clamp(posy, bry, tly), 
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
        fastMove();

    }
    public void fastMove()
    {
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            target.position.z
            );
    }
}

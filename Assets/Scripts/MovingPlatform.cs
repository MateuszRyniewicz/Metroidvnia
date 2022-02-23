using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 0.1f;
    private Vector2 startPoint;
    private Vector2 endPoint;

    public Transform startPointOnScene;
    public Transform endPointOnScene;

    private void Start()
    {
        startPoint = startPointOnScene.position;
        endPoint = endPointOnScene.position;

        Destroy(startPointOnScene.gameObject);
        Destroy(endPointOnScene.gameObject);
    }
    void Update()
    {
        Vector2 currentPos =  Vector2.Lerp(startPoint, endPoint, Mathf.PingPong(Time.time * speed, 1));
        transform.position = currentPos; 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform;
            other.attachedRigidbody.Sleep();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}

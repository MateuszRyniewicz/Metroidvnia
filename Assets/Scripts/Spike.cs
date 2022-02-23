using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    public float moveSpeed=2f;
    public Transform[] points;
    public int curretPoint;



    void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].SetParent(null);
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[curretPoint].position, moveSpeed*Time.deltaTime);
        if (Vector3.Distance(transform.position, points[curretPoint].position) < 0.2f)
        {
            curretPoint++;
            if (curretPoint >= points.Length)
            {
                curretPoint = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    
    private SpriteRenderer sr;
    public Sprite leftSwitch;
    public GameObject doorToOpen;
    public float delayToDestroy = 2f;

    public bool switching;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        doorToOpen = GameObject.Find("Door");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !switching)
        {
            switching = true;
            sr.sprite = leftSwitch;

            doorToOpen.GetComponent<Animator>().SetTrigger("open");
            Destroy(doorToOpen, delayToDestroy);
        }
    }
}

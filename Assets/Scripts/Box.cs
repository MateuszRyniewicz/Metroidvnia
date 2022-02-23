using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Animator anim;

    [Header("Drop Items")]
    public bool shouldDropItem;
    public GameObject[] itemToDrop;
    public float itemToDropPercent;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            if (PlayerController.instance.dashCounter > 0)
            {
                anim.SetTrigger("isOpening");
                DropItem();
                
            }
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }


    public void DropItem()
    {
        if (shouldDropItem)
        {
            float dropChance = Random.Range(0f, 100f);            

            if (dropChance < itemToDropPercent)
            {
                int randomItem = Random.Range(0, itemToDrop.Length);

                Instantiate(itemToDrop[randomItem], transform.position, transform.rotation);
            }
        }
    }
}




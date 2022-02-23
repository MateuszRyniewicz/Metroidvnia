using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathPickup : MonoBehaviour
{
    public int healAdd = 1;
    public GameObject healEffect;

    public float waitToCollected = 1f;

    private void Update()
    {
        if (waitToCollected > 0)
        {
            waitToCollected -= Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToCollected <= 0)
        {
            PlayerHealthController.instance.HealPlayer(healAdd);
            Instantiate(healEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            AudioManager.instance.PlaySFXAdjusted(5);
        }
    }
}

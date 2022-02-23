using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    [SerializeField] private float waitToCollect = 1f;
    [SerializeField] private GameObject collectEffect;


    private void Update()
    {
        if (waitToCollect > 0)
        {
            waitToCollect -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToCollect <= 0)
        {
            Instantiate(collectEffect, transform.position, transform.rotation);
            GameManager.instance.GetCoins(coinValue);
            Destroy(gameObject);
            AudioManager.instance.PlaySFXAdjusted(5);

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{

    public GameObject deathEffect;
    public int damageToEnemy=1;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag== "Enemy")
        {
            other.GetComponent<EnemyPatrol>().Damage(damageToEnemy);
            PlayerController.instance.Bounce();
            AudioManager.instance.PlaySFXAdjusted(11);
        }
    }
}

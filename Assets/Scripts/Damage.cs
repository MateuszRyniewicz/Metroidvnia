using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damageToPlayer = 1;
    public int damageToEnemy = 1;
    public int damageToBoss = 1;

    public GameObject hitEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player");
            if (PlayerController.instance.gameObject != null)
            {
                PlayerHealthController player = other.GetComponent<PlayerHealthController>();
              
                player.Damage(damageToPlayer);
                AudioManager.instance.PlaySFXAdjusted(11);
                PlayerController.instance.Bounce();
                Instantiate(hitEffect, transform.position, transform.rotation);

            }
        }

        if (other.tag == "Enemy")
        {
            EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();
            if (enemy != null)
            {
                enemy.Damage(damageToEnemy);
                AudioManager.instance.PlaySFXAdjusted(11);
                Instantiate(hitEffect, transform.position, transform.rotation);
            }
        }

        if (other.tag == "Boss")
        {
            BossHealthController.instance.Damage(damageToBoss);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float moveSpeed;
    public int damageToPlayer=2;
    public GameObject bulletEffect;


    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector3 dir = transform.position - PlayerHealthController.instance.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        AudioManager.instance.PlaySFXAdjusted(2);
    }

 
    void Update()
    {
        rb.velocity = -transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.Damage(damageToPlayer);
        }

        if (bulletEffect != null)
        {
            Instantiate(bulletEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        AudioManager.instance.PlaySFXAdjusted(3);
    }
}

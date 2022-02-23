using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anim;

    public GameObject deathEffect;
    public Transform[] patrolPoints;
    private int currentPoint;

    public float moveSpeed;
    public float maxSpeed;
    public float waitAtNextPoint;
    private float waitCounter;


    public float jumpForce;


    public float distanceToaAttackPlayer;
    public float chaseSpeed;

    public bool isChasing;
    public bool isPatrol;
    

    public int currentHealth;
    public int maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
        moveSpeed = maxSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        waitCounter = waitAtNextPoint;

        foreach (Transform patrolPoint in patrolPoints)
        {
            patrolPoint.SetParent(null);
        }
    }
    void Update()
    {
        if (PlayerController.instance.gameObject.activeInHierarchy && isPatrol)
        {



            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToaAttackPlayer)
            {
                if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > 0.2f)
                {
                    if (transform.position.x < patrolPoints[currentPoint].position.x)
                    {
                        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                        transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    }

                    if (transform.position.y < patrolPoints[currentPoint].position.y - 0.5f && rb.velocity.y < 0.1f)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    }

                }
                else
                {

                    rb.velocity = new Vector2(0, rb.velocity.y);
                   
                    waitCounter -= Time.deltaTime;
                    if (waitCounter <= 0)
                    {
                        waitCounter = waitAtNextPoint;
                        currentPoint++;

                        if (currentPoint >= patrolPoints.Length)
                        {
                            currentPoint = 0;
                        }
                    }
                }

                anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
                anim.SetBool("isAttack", false);
            }
            else
            {
                if (!isChasing)
                    return;

                transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, chaseSpeed * Time.deltaTime);
                if (transform.position.x < PlayerController.instance.transform.position.x)
                {
                    transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
                }
                else
                {
                    transform.localScale = Vector2.one;
                }

                anim.SetBool("isAttack", true);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }


    public void Damage(int amount)
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject);
                Instantiate(deathEffect, transform.position, transform.rotation);
                AudioManager.instance.PlaySFX(4);
            }
        }
    }
}

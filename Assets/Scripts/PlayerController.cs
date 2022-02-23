using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [HideInInspector]
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Speed")]
    public float speed= 8f;
    public float maxSpeed = 8f;

    private Transform groundPoint;
    private bool isGrounded;
    public float timeAttack = 2f;
    private float attackCounter;
    private bool canAttack;
    
    [Header("Jump")]
    public float bounceForce;
    public bool canDubleJump;
    public float forceJump = 20f;
  

    [Header("Dash System")]
    public float dashValue;
    public float dashTime;
    public SpriteRenderer sr;
    public SpriteRenderer afterSR;
    public float aterSrLifeTime;
    public float timerBetweenNextSR;
    private float afterSrCounter;
    public Color afterSrColor;
    public float waitAfterDash;
    private float dashLoaderCouter;
    [HideInInspector]
    public float dashCounter;

    [Header("Back Force")]
    public float bounceBackLenght;
    public float bounceBackForce;
    private float bounceCounter;



    bool facingRight;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

      //  DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        rb =   GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr =   GetComponent<SpriteRenderer>();

        groundPoint = transform.Find("GroundChackerPoint");
        speed = maxSpeed;
    }

    
    void Update()
    {
        if (!PauseController.instance.isPaused)
        {
            if (bounceCounter <= 0)
            {

                if (dashLoaderCouter > 0)
                {
                    dashLoaderCouter -= Time.deltaTime;
                }
                else
                {

                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        dashCounter = dashTime;
                        ShowAfterSR();

                        AudioManager.instance.PlaySFXAdjusted(6);

                    }

                }

                if (dashCounter > 0)
                {
                    dashCounter -= Time.deltaTime;

                    rb.velocity = new Vector2(dashValue * transform.localScale.x, rb.velocity.y);

                    afterSrCounter -= Time.deltaTime;


                    if (afterSrCounter <= 0)
                    {
                        ShowAfterSR();
                    }


                    dashLoaderCouter = waitAfterDash;
                }
                else
                {

                    rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);

                    FlipSprite();

                }

                isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, 1 << LayerMask.NameToLayer("Ground"));

                if (Input.GetButtonDown("Jump") && (isGrounded || canDubleJump))
                {
                    if (isGrounded)
                    {
                        canDubleJump = true;
                        AudioManager.instance.PlaySFXAdjusted(10);
                    }
                    else
                    {
                        canDubleJump = false;

                        anim.SetTrigger("dubleJump");
                        AudioManager.instance.PlaySFXAdjusted(8);
                    }


                    rb.velocity = new Vector2(rb.velocity.x, forceJump);
                }




                if (Input.GetKeyDown(KeyCode.LeftAlt) && canAttack)
                {
                    canAttack = false;

                    anim.SetTrigger("attack");
                }



                attackCounter -= Time.deltaTime;
                if (attackCounter <= 0)
                {
                    canAttack = true;
                    attackCounter = timeAttack;
                }

            }
            else
            {
                bounceCounter -= Time.deltaTime;
                if (transform.localScale.x == 1)
                {
                    rb.velocity = new Vector2(-bounceBackForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(bounceBackForce, rb.velocity.y);
                }
            }
        }

        anim.SetBool("isOnGround", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }


    public void FlipSprite()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;

        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
    }

    public void ShowAfterSR()
    {
        SpriteRenderer image = Instantiate(afterSR, transform.position, transform.rotation);
        image.sprite = sr.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterSrColor;


        Destroy(image.gameObject, aterSrLifeTime);

        afterSrCounter = timerBetweenNextSR;
    }

    public void BounceBack()
    {
        bounceCounter = bounceBackLenght;
        rb.velocity = new Vector2(0, bounceBackForce);
    }

    public void Bounce()
    {
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
    }
}

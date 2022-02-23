using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private CameraController cam;
    public Transform camPos;
    public float camSpeed;

    public int threshold1;
    public int threshold2;

    public float activeTime;
    public float inactiveTime;
    public float fadeoutTime;

    private float activeCounter;
    private float fadeCounter;
    private float inactiveCounter;

    public Transform[] spawnPoints;
    private Transform targetPoint;
    public float moveSpeed;

    public Animator anim;
    public Transform theBoss;

    public float timeBetweenShot1;
    public float timeBetweenShot2;
    private float shotCounter;
    public GameObject bulletBoss;
    public Transform shotPoint;
    public bool battleEnded;
    public GameObject door;

    void Start()
    {
        AudioManager.instance.BossMusic();

        cam = FindObjectOfType<CameraController>();
        cam.enabled = false;

        anim = GetComponentInChildren<Animator>();
        activeCounter = activeTime;

        theBoss = transform.Find("The Boss").transform;

        shotCounter = timeBetweenShot1;
    }

    void Update()
    {
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPos.position, camSpeed * Time.deltaTime);
        if (!battleEnded)
        {


            if (BossHealthController.instance.currenthHealth > threshold1)
            {
                if (activeCounter > 0)
                {
                    activeCounter -= Time.deltaTime;
                    if (activeCounter <= 0)
                    {
                        fadeCounter = fadeoutTime;
                        anim.SetTrigger("vanish");
                    }


                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShot1;
                        Instantiate(bulletBoss, shotPoint.position, Quaternion.identity);
                    }
                }
                else if (fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        theBoss.gameObject.SetActive(false);
                        inactiveCounter = inactiveTime;
                    }
                }
                else if (inactiveCounter > 0)
                {
                    inactiveCounter -= Time.deltaTime;
                    if (inactiveCounter <= 0)
                    {
                        theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                        theBoss.gameObject.SetActive(true);

                        activeCounter = activeTime;
                        shotCounter = timeBetweenShot1;
                    }
                }
            }
            else
            {
                if (targetPoint == null)
                {
                    targetPoint = theBoss;
                    fadeCounter = fadeoutTime;
                    anim.SetTrigger("vanish");

                }
                else
                {
                    if (Vector3.Distance(theBoss.position, targetPoint.position) > 0.2f)
                    {
                        theBoss.position = Vector3.MoveTowards(theBoss.position, targetPoint.position, moveSpeed * Time.deltaTime);


                        if (Vector3.Distance(theBoss.position, targetPoint.position) <= 0.2f)
                        {
                            fadeCounter = fadeoutTime;
                            anim.SetTrigger("vanish");
                        }

                        shotCounter -= Time.deltaTime;
                        if (shotCounter <= 0)
                        {
                            if (PlayerHealthController.instance.currentHealth == 2)
                            {
                                shotCounter = timeBetweenShot1;

                            }
                            else
                            {
                                shotCounter = timeBetweenShot2;
                            }

                            Instantiate(bulletBoss, shotPoint.position, Quaternion.identity);
                        }
                    }
                    else if (fadeCounter > 0)
                    {
                        fadeCounter -= Time.deltaTime;
                        if (fadeCounter <= 0)
                        {
                            theBoss.gameObject.SetActive(false);
                            inactiveCounter = inactiveTime;
                        }
                    }
                    else if (inactiveCounter > 0)
                    {
                        inactiveCounter -= Time.deltaTime;
                        if (inactiveCounter <= 0)
                        {
                            theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                            while (targetPoint.position == theBoss.position)
                            {
                                targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                            }

                            theBoss.gameObject.SetActive(true);

                            if (PlayerHealthController.instance.currentHealth == 1)
                            {
                                shotCounter = timeBetweenShot2;
                            }
                            else
                            {
                                shotCounter = timeBetweenShot1;
                            }


                        }
                    }
                }
            }
        }
        else
        {
            fadeCounter -= Time.deltaTime;
            if (fadeCounter < 0)
            {
                if (door != null)
                {
                    door.GetComponent<Animator>().SetTrigger("open");
                }

                cam.enabled = true;
                gameObject.SetActive(false);

                AudioManager.instance.LevelMusic();
            }
        }
    }

    public void EndBossBattle()
    {
        battleEnded = true;
        fadeCounter = fadeoutTime;
        anim.SetTrigger("vanish");
        theBoss.GetComponent<BoxCollider2D>().enabled = false;

        BossBullet[] bullets = FindObjectsOfType<BossBullet>();
        if (bullets.Length > 0)
        {
            foreach ( var buletsOnScene in bullets)
            {
                Destroy(buletsOnScene.gameObject);
            }
        }
    }
}

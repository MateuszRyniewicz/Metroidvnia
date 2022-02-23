using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController instance;

    public BossController theBoss;


    public Slider bossHealthSlider;
    public int currenthHealth;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Start()
    {
        theBoss = GameObject.FindObjectOfType<BossController>();

        bossHealthSlider.maxValue = currenthHealth;
        bossHealthSlider.value = currenthHealth;
    }
    public void Damage(int amount)
    {
        currenthHealth -= amount;
        if (currenthHealth <= 0)
        {
            currenthHealth = 0;
            theBoss.EndBossBattle();

            AudioManager.instance.PlaySFX(0);
        }
        else
        {
            AudioManager.instance.PlaySFX(1);
        }

        bossHealthSlider.value = currenthHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;

    [Header("Health UI")]
    public Image[] heartDisplayUI;
    public Sprite heartEmpty;
    public Sprite heartFull;

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
    }

    private void Start()
    {
        maxHealth = CharacterTracker.instance.maxHealth;
        currentHealth = CharacterTracker.instance.currentHealth;
    }

    public void UpdateHealthPlayer()
    {
        switch (currentHealth)
        {
            case 3:
                heartDisplayUI[2].sprite = heartFull;
                heartDisplayUI[1].sprite = heartFull;
                heartDisplayUI[0].sprite = heartFull;
                break;  
                case 2:
                heartDisplayUI[2].sprite = heartFull;
                heartDisplayUI[1].sprite = heartFull;
                heartDisplayUI[0].sprite = heartEmpty;
                break;
            case 1:
                heartDisplayUI[2].sprite = heartFull;
                heartDisplayUI[1].sprite = heartEmpty;
                heartDisplayUI[0].sprite = heartEmpty;
                break;
            case 0:
                heartDisplayUI[2].sprite = heartEmpty;
                heartDisplayUI[1].sprite = heartEmpty;
                heartDisplayUI[0].sprite = heartEmpty;
                break;
        }
    }

    public void Damage(int amount)
    {
        currentHealth -= amount;
        AudioManager.instance.PlaySFXAdjusted(9);
        PlayerController.instance.BounceBack();

        UpdateHealthPlayer();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PlayerController.instance.anim.SetBool("isDead", true);
            
            GameManager.instance.RespownPlayer();
            AudioManager.instance.PlaySFX(7);
        }
    }

    public void HealPlayer(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthPlayer();
    }
}

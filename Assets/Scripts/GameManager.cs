using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Coins")]
    public int currentCoin;
    public Text coinText;

    [Header("FadeScreen")]
    public Image fadeScreen;
    public float fadeSpeed;
    public bool fadeToBlack;
    public bool fadeOUTBlack;


    public float waitToRespown;

    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

       // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentCoin = CharacterTracker.instance.currentCoins;
        coinText.text = currentCoin.ToString();

        fadeOUTBlack = true;
        fadeToBlack = false;
    }


    public void Update()
    {
        if (fadeOUTBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                                         Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                fadeOUTBlack = false;
            }
        }

        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                                         Mathf.MoveTowards(fadeScreen.color.a, 1, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }


    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOUTBlack = false;
    }

    public void StartFaceOutToBlack()
    {
        fadeToBlack = false;
        fadeOUTBlack = true;
    }

    public void GetCoins(int amount)
    {
        currentCoin += amount;
        coinText.text = currentCoin.ToString();
    }

    public void SpendCoins(int amount)
    {
        currentCoin -= amount;

        if (currentCoin <= 0)
        {
            currentCoin = 0;
        }

        coinText.text = currentCoin.ToString();
    }

    public void RespownPlayer()
    {
        StartCoroutine(RespownCo());
    }

    

    private IEnumerator RespownCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
        PlayerController.instance.speed = 0;

        StartFadeToBlack();
        yield return new WaitForSeconds(waitToRespown);
        PlayerController.instance.speed = PlayerController.instance.maxSpeed;
        PlayerController.instance.transform.position = SaveController.instance.spawnPoint;
        PlayerController.instance.gameObject.SetActive(true);
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        PlayerHealthController.instance.UpdateHealthPlayer();
        StartFaceOutToBlack();
    }


    
}

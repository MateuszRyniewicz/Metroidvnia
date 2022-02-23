using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public string LevelName;
    public float delayToLoadNewLevel = 1.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadBossLevelCO());
        }
    }


    IEnumerator LoadBossLevelCO()
    {
            
        PlayerController.instance.speed = 0;
        GameManager.instance.StartFadeToBlack();
        yield return new WaitForSeconds(delayToLoadNewLevel);
        SceneManager.LoadScene(LevelName);
        PlayerController.instance.speed = PlayerController.instance.maxSpeed;
        PlayerController.instance.transform.position = SaveController.instance.spawnPoint;
        CharacterTracker.instance.currentCoins = GameManager.instance.currentCoin;
        CharacterTracker.instance.currentHealth = PlayerHealthController.instance.currentHealth;
        CharacterTracker.instance.maxHealth = PlayerHealthController.instance.maxHealth;
    }
}

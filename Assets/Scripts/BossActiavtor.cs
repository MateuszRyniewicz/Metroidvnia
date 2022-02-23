using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActiavtor : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossBattle;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            StartCoroutine(delayActiveBossCO());
        }
    }

    IEnumerator delayActiveBossCO()
    {
        bossBattle.SetActive(true);
        yield return new WaitForSeconds(2f);
        boss.SetActive(true);
        gameObject.SetActive(false);
    }
}

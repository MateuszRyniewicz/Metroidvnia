using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SaveController.instance.DeactivateSavePoints();
            anim.SetBool("isAchieved",true);

            SaveController.instance.NewSpawnPoint(transform.position);
        }
    }

    public void ResetSavePoint()
    {
        anim.SetBool("isAchieved",false);
    }
}

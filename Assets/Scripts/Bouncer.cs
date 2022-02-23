using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float bouncerForce = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            PlayerController.instance.rb.velocity = new Vector2(PlayerController.instance.rb.velocity.x, bouncerForce);
        }
        
    }
}

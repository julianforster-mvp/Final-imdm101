using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float multiplier = 1.5f;
    public float duration = 3.0f;


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    
    void Pickup(Collider player)
    {
        
        PlayerControl playerControl = player.GetComponent<PlayerControl>();

        
        if (playerControl != null)
        {
        StartCoroutine(TemporarySpeedBoost(playerControl));
        Debug.Log("Power-Up Picked Up!"); 
        }
        else
        {
        Debug.LogError("Player does not have a PlayerControl component!");
        }
    }

     IEnumerator TemporarySpeedBoost(PlayerControl playerControl)
    {
        playerControl.speed *= multiplier;
        yield return new WaitForSeconds(duration);
        playerControl.speed /= multiplier;
        Destroy(gameObject);

        Debug.Log("Power-Up effect ended.");
    }

}

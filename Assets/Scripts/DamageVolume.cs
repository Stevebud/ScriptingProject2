using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    public int damageAmount = 50;
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth collidedHealth = other.gameObject.GetComponent<PlayerHealth>();
        //searches if it is the player
        if (collidedHealth != null)
        {
            //if it is, do damage
            collidedHealth.DamagePlayer(damageAmount);
        }

    }

}

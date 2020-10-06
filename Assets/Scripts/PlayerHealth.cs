using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Level01Controller level01Controller;
    [SerializeField] GameObject _deathText;

    public int health = 100;

    private void Awake()
    {
        _deathText.SetActive(false);
    }
    public void DamagePlayer(int damageAmount)
    {
        //subtract from health
        health -= damageAmount;
        //update visual
        level01Controller.UpdateHealthSlider();
        //check if player is dead
        if(health == 0)
        {
            Time.timeScale = 0f;
            _deathText.SetActive(true);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Level01Controller level01Controller;
    [SerializeField] GameObject _deathText;
    [SerializeField] AudioClip playerHit;
    [SerializeField] AudioClip playerDeath;

    public int health = 100;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = playerHit;
    }

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
            audioSource.clip = playerDeath;//Audio feedback
            Time.timeScale = 0f;
            _deathText.SetActive(true);
        }
        audioSource.Play();//audio feedback
    }
    
}

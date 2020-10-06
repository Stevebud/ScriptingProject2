using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ParticleSystem weaponParticles;

    AudioSource weaponAudio;
    void Start()
    {
        weaponAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        weaponAudio.Play();
        weaponParticles.Play();
    }
}

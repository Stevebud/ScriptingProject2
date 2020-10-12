using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ParticleSystem weaponParticles;
    public Camera camController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] Light hitLight;
    [SerializeField] int weaponDamage = 3;
    [SerializeField] float weaponKnockBack = 4f;
    [SerializeField] LayerMask hitLayer;

    RaycastHit objectHit; //Stores info about raycast hit

    AudioSource weaponAudio;
    void Start()
    {
        weaponAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1f)//if game isn't paused
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
    }

    // fire the weapon using raycast
    void Fire()
    {
        weaponAudio.Play();
        weaponParticles.Play();

        //caluculate direction to shoot the ray
        Vector3 rayDirection = camController.transform.forward;
        //cast a debug ray
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.grey, 1f);
        //do the raycast
        if(Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance, hitLayer))//if it hits a collider in the hitLayer layer
        {
            Debug.Log("You hit the " + objectHit.transform.name);

            if (objectHit.transform.tag == "Enemy")
            {
                //check if enemy is hit
                EnemyCube enemy = objectHit.transform.gameObject.GetComponent<EnemyCube>();
                if(enemy != null)
                {
                    //damage the enemy
                    enemy.damageTaken(weaponDamage);

                    //knock the enemy back
                    Rigidbody enemyRB = objectHit.transform.gameObject.GetComponent<Rigidbody>();
                    if(enemyRB != null)
                    {
                        enemyRB.AddForce(rayDirection * weaponKnockBack);
                        //objectHit.transform.gameObject.transform.position
                    }

                    Debug.Log("Enemy Health: " + enemy.cubeHealth);
                }
                hitLight.transform.position = objectHit.point + (objectHit.normal*0.01f);
            }
            
        } else
        {
            Debug.Log("Miss");
        }
    }
}

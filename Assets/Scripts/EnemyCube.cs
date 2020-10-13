using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    public int cubeHealth = 10;
    public float colorChangeFactor = 10f;
    public float weaponCooldown = 3f;
    public float projectileSpeed = 20f;
    public float weaponRange = 20f;
    public float weaponRecoil = 30f;
    public int weaponDamage = 20;
    public float aimCorrectionSpeed = 0.9f;
    [SerializeField] Rigidbody projectile;
    [SerializeField] Transform target;
    [SerializeField] Transform rayOrigin;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] AudioClip enemyCubeHitClip;
    [SerializeField] AudioClip enemyCubeShootClip;

    Renderer rend;
    AudioSource audioSource;
    Rigidbody cubeRB;
    float cooldownLeft;

    //was going to save color in case it changed the material asset
    //Color startColor;
    void Start()
    {
        rend = GetComponent<Renderer>();
        cubeRB = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        //startColor = rend.material.color;
        cooldownLeft = weaponCooldown;
        DamageVolume damageVol = projectile.gameObject.GetComponent<DamageVolume>();
        if(damageVol != null)
        {
            damageVol.damageAmount = weaponDamage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //update weapon cooldown
        cooldownLeft -= Time.deltaTime;

        //make the cube look toward the player if the player is in range
        float distFromTarget = Vector3.Distance(target.transform.position, gameObject.transform.position);
        if (distFromTarget < weaponRange)
        {
            Vector3 goodPoint = gameObject.transform.position + (gameObject.transform.forward * distFromTarget);
            Vector3 pointToLookAt = Vector3.Lerp(target.transform.position, goodPoint, aimCorrectionSpeed);
            transform.LookAt(pointToLookAt);
        }

        if (cooldownLeft <= 0)//when ready to fire
        {
            if (Physics.Raycast(rayOrigin.position, transform.forward, weaponRange, playerLayer))//when player is in sights and in range
            {
                //fire the weapon
                Shoot();
                audioSource.clip = enemyCubeShootClip;
                audioSource.Play();
                //reset cooldown
                cooldownLeft = weaponCooldown;
            }
        }
    }

    public void damageTaken(int damageAmount)
    {
        //Audio Feedback
        audioSource.clip = enemyCubeHitClip;
        audioSource.Play();

        cubeHealth -= damageAmount;
        rend.material.color += new Color(colorChangeFactor, -colorChangeFactor, -colorChangeFactor, 1);
        if(cubeHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void Shoot()
    {
        //Instantiates projectile at gameObject
        Rigidbody newProject;
        newProject = Instantiate(projectile, transform.position, transform.rotation);

        //gives the projectile velocity
        newProject.velocity = transform.TransformDirection(Vector3.forward * projectileSpeed);

        //destroy the projectile after a few seconds in case it doesn't hit anything
        Destroy(newProject.gameObject, 2f);

        //recoil on EnemyCube
        cubeRB.AddForce(transform.forward * -weaponRecoil);
    }
}

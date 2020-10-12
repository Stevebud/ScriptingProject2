using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    public int cubeHealth = 10;
    public float colorChangeFactor = 10f;

    Renderer rend;
    AudioSource audioSource;

    //was going to save color in case it changed the material asset
    //Color startColor;
    void Start()
    {
        rend = GetComponent<Renderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        //startColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damageTaken(int damageAmount)
    {
        //Audio Feedback
        audioSource.Play();

        cubeHealth -= damageAmount;
        rend.material.color += new Color(colorChangeFactor, -colorChangeFactor, -colorChangeFactor, 1);
        if(cubeHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }


}

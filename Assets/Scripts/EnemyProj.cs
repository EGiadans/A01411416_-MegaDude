﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProj : MonoBehaviour {
    public float damage = 100f; //Damage made by the projectile

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //	We call this method to destroy the projectile
    public void Hit()
    {
        Destroy(gameObject);
    }

    //	Getter for the damage property.
    public float getDamage()
    {
        return damage;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Object for eliminating the player 
        Shredder eliminar = collider.gameObject.GetComponent<Shredder>();
        


        //If projectile collides with a Shredder trigger
        if (eliminar)
        {
            //Projectile is destroyed
            Hit();
        }
    }
    
    void OnCollisionEnter2D(Collision2D collider)
    {
        Projectile proj = collider.gameObject.GetComponent<Projectile>();
        MovingEnemy movEne = collider.gameObject.GetComponent<MovingEnemy>();

        //If the projectile collides with another projectile, destroy it
        if (proj)
        {
            Hit();

        }
        /*else if (movEne)
        {
            Hit();
            movEne.health = 150f;
        }
        */
    }
    
    
}

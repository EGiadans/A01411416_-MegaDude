using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Object for the player shooting
    public float damage = 100f; //Damage Made by the projectile

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
        //Object for coins in the game


        //If projectile collides with a Shredder trigger
        if (eliminar)
        {
            //The projectile is gone
            Hit();
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        //If the projectile collides with another projectile
        Projectile proj = collider.gameObject.GetComponent<Projectile>();
        //MovingEnemy movEne = collider.gameObject.GetComponent<MovingEnemy>();

        if (proj)
        {
            //Destroy this 
            Hit();

        }
    }
}
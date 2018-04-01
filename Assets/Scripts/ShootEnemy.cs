using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour {
    //Speed when moving
    public float speed = 2;
    //Direction of Movement
    Vector2 dir = Vector2.right;

    public float damage;

    public int scoreValue = 150;
    private ScoreKeeper scoreKeeper;

    public AudioClip enemyHit;

    public float health = 150f;
    public GameObject projectile;
    public float projectileSpeed;
    public float shotsPerSeconds = 0.5f;
    private Rigidbody2D rgb;

    private Vector3 offset = new Vector3(1, 0.25f, 0);
    public AudioClip fireSound;


    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    void FixedUpdate()
    {
        //Speed to move
        GetComponent<Rigidbody2D>().velocity = dir * speed;
        //	We use the deltaTime and the shotsPerSecond properties to compute a shooting probability.
        float probability = shotsPerSeconds * Time.deltaTime;

        //	If a random generated value is less than the computed shooting probability, then the enemy
        //	ship shoots a laser beam.
        if (Random.value < probability && dir.x < 0)
        {
            Fire();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //If the object collides with our pivot, it changes direction
        transform.localScale = new Vector2(-1 * transform.localScale.x,
                                                transform.localScale.y);

        // And mirror it
        dir = new Vector2(-1 * dir.x, dir.y);
        Flip();
    }

    //Method for destroying(unused)
    void OnCollisionEnter2D(Collision2D collider)
    {
        Projectile proj = collider.gameObject.GetComponent<Projectile>();
        EnemyProj enemyProj = collider.gameObject.GetComponent<EnemyProj>();
        //Mario player = collider.gameObject.GetComponent<Mario>();
        if (enemyProj)
        {
            enemyProj.Hit();
        }
        else if (proj)
        {
            health -= proj.getDamage();
            AudioSource.PlayClipAtPoint(enemyHit, transform.position);
            proj.Hit();      // The missile is destroyed upon collision with our ship.

            if (health <= 0)
            {
                //	We call the Die() method;
                Die();

            }

        }
    }


    //Changing direction of sprite
    void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        projectileSpeed = projectileSpeed * -1;
        offset = offset * -1;
    }

    void Die()
    {
        Destroy(gameObject);
        //	We add the scoreValue to the score.
        scoreKeeper.Score(scoreValue);

    }

    public float getDamage()
    {
        return damage;
    }
    
    void Fire()
    {
        //	We instantiate the laser bean and give it a positive velocity in the y axis.  We offset the
        //	beam's position 1 unit above our ship, because we do not want an instant collision between
        //	them.
        //Vector3 offset = new Vector3(1, 0, 0);
        //GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
        GameObject beam = Instantiate(projectile, transform.position + offset, transform.rotation) as GameObject;
        rgb = beam.GetComponent<Rigidbody2D>();
        rgb.velocity = new Vector3(projectileSpeed, 0, 0);


        //	We play the fireSound Clip
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour {
    //Speed when moving
    public float speed = 2;
    //Direction of Movement
    Vector2 dir = Vector2.right;
    //Damage made by enemy
    public float damage;
    //Score when killed
    public int scoreValue = 150;
    //Link to scorekeeper object
    private ScoreKeeper scoreKeeper;
    //Audio when damaged
    public AudioClip enemyHit;
    //Amount of health for this enemy
    public float health = 150f;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    //Impressing speed when moving
    void FixedUpdate()
    {
        //Speed to move
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //If the object collides with our pivot, it changes direction
        transform.localScale = new Vector2(-1 * transform.localScale.x,
                                                transform.localScale.y);

        // And mirror it
        dir = new Vector2(-1 * dir.x, dir.y);
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
        /*else if (enemyProj)
        {
            enemyProj.Hit();
        }*/
    }

    
    //Changing direction of sprite
    void Flip()
    {
        Vector3 escala = transform.localScale;

    }

    void Die()
    {
        Destroy(gameObject);
        //	We add the scoreValue to the score.
        scoreKeeper.Score(scoreValue);
        
    }
    //Retrieve damage attribute
    public float getDamage()
    {
        return damage;
    }

    
}

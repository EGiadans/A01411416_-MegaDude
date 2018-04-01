using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mario : MonoBehaviour {
    public float maxVel = 5f; //Max Speed when walking
    public float yJumpForce = 300f; //JumForce given when jumping

    private bool isjumpling = false; 
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 jumpforce;
    private bool movingRight = true;

    //public GameObject enemigo;
    public float health = 300f; //Health of our player
    public static float scoreMario = 0f; //Score of the player

    public AudioClip coin; //Music when colliding when coins
    public AudioClip jump; //Music when jumping
    public AudioClip fireSound;
    public AudioClip hit; //SOund when receiving damage
    public AudioClip heart;
    

    public GameObject projectile;   //	The object we use to instantiate a laser beam
    public float projectileSpeed;   //	The speed of our laser beam
    public float firingRate = 0.2f; //	How fast we can instantiate our laser beam

    private Rigidbody2D rgb;        //	A linkt to our beam rigid body


    private int coinCount;//Coincount so when we reach it, the game ends
  
    private Vector3 offset = new Vector3(0.25f, 0, 0);

    private HealthKeeper healthKeeper;
    private ScoreKeeper scoreKeeper;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        jumpforce = new Vector2(0, 0);
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Update horizontal speed
        float v = Input.GetAxis("Horizontal");
        Vector2 vel = new Vector2(0, rb.velocity.y);

        v *= maxVel;

        vel.x = v;

        rb.velocity = vel;

        

        //Change to walking animation when needed
        if (v != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        //If player press jump
        if (Input.GetAxis("Jump") > 0.01f)
        {
            if (!isjumpling)
            {
                if (rb.velocity.y == 0)
                {
                    //if player jumped, we play animation for jumping
                    anim.SetBool("isJumping",true); //Change animation when jumping
                    
                    isjumpling = true;
                    //This 0 value is for the player to only go upside down
                    jumpforce.x = 0f;
                    //This will be a variable, the force in the jump will take it
                    jumpforce.y = yJumpForce;
                    //The rigidBody of the player object will take this force for moving
                    rb.AddForce(jumpforce);
                    //sound played when jumping
                    AudioSource.PlayClipAtPoint(jump, transform.position);
                }
            }
        }
        else
        {
            isjumpling = false;
        }
        if (movingRight && v < 0)
        {
            movingRight = false;
            Flip();
        }
        else if (!movingRight && v > 0)
        {
            movingRight = true;
            Flip();
        }
        if (Input.GetButtonDown("Vertical"))
        {
            anim.SetBool("isDucking", true);
            
        }
        if (Input.GetButtonUp("Vertical"))
        {
            anim.SetBool("isDucking", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            //InvokeRepeating("Fire", 0.000001f, firingRate);
            anim.SetBool("isShooting", true);
            Fire();
            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("isShooting", false);
        }

        //	If the user stops pressing the space bar, we cancel the invoke to the Fire method.



    }
    //method for flipping the sprite when going to opposite direction
    private void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        projectileSpeed = projectileSpeed * -1;
        offset = offset * -1;
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Object for eliminating the player 
        FlShred eliminar = collider.gameObject.GetComponent<FlShred>();
        //Object for coins in the game
        Coin moneda = collider.gameObject.GetComponent<Coin>();
        Heart corazon = collider.gameObject.GetComponent<Heart>();

        DoorColl door = collider.gameObject.GetComponent<DoorColl>();

        //If player collides with a Shredder trigger
        if (eliminar)
        {
            //The scene now is Lose
            LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            levelManager.LoadLevel("GameOver");
        }
        //If the player collides with a coin Trigger
        else if (moneda)
        {
            //We increase the score by the value of the coin.points
            
            //Plays the coin sound
            AudioSource.PlayClipAtPoint(coin, transform.position);
            scoreKeeper.Score(moneda.getPoints());
            coinCount++;
            //Destroys object so the player cannot see it
            Destroy(collider.gameObject);
           
            Debug.Log(moneda.getPoints());
            //When the coinCount reaches the number of coins in the scene, next level
            /*
            if (coinCount == 3)
            {
                LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
                //levelManager.LoadLevel("Win");
                Application.LoadLevel(Application.loadedLevel + 1);
                coinCount = 0;
                if (levelManager.Equals("Game2"))
                {
                    levelManager.LoadLevel("Win");
                }
            }*/
        } else if (door)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (corazon)
        {
            health = 300f;
            AudioSource.PlayClipAtPoint(heart, transform.position);
            Destroy(collider.gameObject);
        }
    }


    
    void OnCollisionEnter2D(Collision2D collider)
    {
        //We find any enemies in the scene
        //Cactus enemy = collider.gameObject.GetComponent<Cactus>();
        MovingEnemy movEnemy = collider.gameObject.GetComponent<MovingEnemy>();
        EnemyProj enemyproj = collider.gameObject.GetComponent<EnemyProj>();
        //If the player collides with an enemy, the enemy reduces player's health by its own damage
        //If player's health is 0, the player dies
        /*if (enemy)
        {
            health -= enemy.getDamage();
            if (health <= 0)
            {
                Die();
            }
        }*/
        if (movEnemy)
        {
            AudioSource.PlayClipAtPoint(hit, transform.position);
            //healthKeeper.Damages(movEnemy.getDamage());
            health -= movEnemy.getDamage();
            
            if (health <= 0)
            {
                Die();
            }
        }
        else if (enemyproj)
        {
            health -= enemyproj.getDamage();
            AudioSource.PlayClipAtPoint(hit, transform.position);
            enemyproj.Hit();      // The missile is destroyed upon collision with our ship.

            if (health <= 0)
            {
                //	We call the Die() method;
                Die();

            }

        }
        else
        {
            //Debug.Log("Floor");
            //When player collides with floor, the jump animation stops
            //This is because I dont know how to stop that animation
            anim.SetBool("isJumping", false);
        }
    }
    /*
    void OnCollisionEnter2D(Collision2D collider)
    {
        
            //Debug.Log("Floor");
            //When player collides with floor, the jump animation stops
            //This is because I dont know how to stop that animation
            anim.SetBool("isJumping", false);
    }
    */
        //Function for killing the player
        void Die()
    {
        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.LoadLevel("GameOver");
        Destroy(gameObject);
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

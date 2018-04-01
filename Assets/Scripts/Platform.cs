using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        //Get object for the player
        Mario mario = collider.gameObject.GetComponent<Mario>();

        //If our platform collides with the player, it will fall and disapear
        if (mario)
        {
            Debug.Log("Mario");
            this.GetComponent<Rigidbody2D>().gravityScale = 1f;
            Destroy(this.gameObject);
        }

    }
}

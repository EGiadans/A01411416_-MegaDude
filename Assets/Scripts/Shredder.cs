using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {
    private Vector3 offset;
    //public GameObject player;
    // Use this for initialization
    void Start()
    {
        //offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
        void OnTriggerEnter2D(Collider2D collider)
        {
            //	If any object collides with the shredder, then that object is destroyed
            Destroy(collider.gameObject);
        }
        
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }
    */
}

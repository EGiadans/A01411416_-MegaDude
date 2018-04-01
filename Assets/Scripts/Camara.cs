using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camara : MonoBehaviour {
    public GameObject player;
    public Vector3 offset;
    //public Vector3 farLeft;  // End of screen Left
    //public Vector3 farRight;  //End of Screen Right
    //public float speed = 0.1F;


    // Use this for initialization


    // Update is called once per frame
    
    void Update () {
        transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y +offset.y, offset.z);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offset.y, offset.z);

    }


    /*
void Update()
{
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
    {
        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        transform.Translate(-touchDeltaPosition.x * Time.deltaTime * speed, 0, 0);

        if (transform.position.x > farRight.x)
            transform.position = new Vector3(farRight.x, transform.position.y, transform.position.z);

        if (transform.position.x < farLeft.x)
            transform.position = new Vector3(farLeft.x, transform.position.y, transform.position.z);
    }
}
*/
}

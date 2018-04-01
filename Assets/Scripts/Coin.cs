using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public int points; //Points given when taking the coin
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int getPoints() //Method to retrieve points for other object
    {
        return points;
    }

    public void Die()
    {
        //	We play the deathSound clip and then we destroy the object.
        
    }
}

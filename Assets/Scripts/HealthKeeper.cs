using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthKeeper : MonoBehaviour {
    public static float health;
    private Text healthText;
    public Mario player;

    // Use this for initialization
    void Start () {
        healthText = GetComponent<Text>();
        Reset();
        //health = player.health;
        //healthText.text = "Health:"+health.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        health = player.health;
        healthText.text = "Health:" + health.ToString();
    }

    public void Damages(float dam)
    {
        health -= dam;
        healthText.text = "Health:" + health.ToString();
        //Debug.Log(score);
    }

    public static void Reset()
    {
        health = 0;
        // scoreText.text = score.ToString ();
    }

   
}

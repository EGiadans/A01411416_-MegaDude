using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Text scoreText = GetComponent<Text>(); //Obtain the Text label from Unity
        scoreText.text = ScoreKeeper.score.ToString();//Show score as a text
        //ScoreKeeper.Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

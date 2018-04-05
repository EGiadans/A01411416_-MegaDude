using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public void LoadLevel(string name)
    {
        Debug.Log("New Level load: " + name);
        //	Application.LoadLevel (name);    -- This method was deprecated a long time ago
        SceneManager.LoadScene(name);
    }
    //When pressing Quit, end application
    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }
    //Reset score when reaching start scene
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            ScoreKeeper.Reset();
        }
    }

    }

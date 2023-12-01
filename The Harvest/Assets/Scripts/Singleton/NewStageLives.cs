using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewStageLives : MonoBehaviour
{
    void Start()
    {   
        // Scene 13 is Boss Scene
        if(SceneManager.GetActiveScene().buildIndex != 13 ) {
            if(Singleton.instance.GetCurrentLives() < 3) {
                Singleton.instance.ResetCurrentLives();
            }
        }
        else {
            // If it is the Boss Scene
            Singleton.instance.ResetBossCurrentLives();
        }
    }
}

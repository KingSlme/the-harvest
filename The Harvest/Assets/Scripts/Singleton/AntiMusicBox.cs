using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiMusicBox : MonoBehaviour
{
    void Start()
    {
        Singleton.instance.StopMusic();
    }
}

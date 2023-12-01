using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    void Start()
    {
        Singleton.instance.FindNextMusic();
        Singleton.instance.PlayMusic();
    }
}

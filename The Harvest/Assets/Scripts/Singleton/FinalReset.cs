using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalReset : MonoBehaviour
{

    void Start()
    {
        Singleton.instance.ResetCurrentLives();
        Singleton.instance.currentShopCurrency = 0;
        Singleton.instance.hasAutoSeeder = false;
        Singleton.instance.hasFertilizer = false;
        Singleton.instance.hasFrostbite = false;
        Singleton.instance.hasWarHorn = false;
    }

}

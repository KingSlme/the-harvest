using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedTest2 : MonoBehaviour
{
    PlantInfo2 plantInfo;

    IEnumerator DelayListAppend() {
        yield return new WaitForSecondsRealtime(0.6f);
        // Optional Null Test (Might Interfere with Ice)
        /*if(GameObject.Find ("Seed(Clone)") != null) {
            plantInfo.seedList.Add(this.gameObject);
        }*/
        plantInfo.seedList.Add(this.gameObject);
    }

    void Start()
    {
        plantInfo = transform.root.GetComponent<PlantInfo2>();
        StartCoroutine(DelayListAppend());
    }
}

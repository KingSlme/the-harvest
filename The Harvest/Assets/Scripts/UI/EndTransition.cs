using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTransition : MonoBehaviour
{
    [SerializeField] GameObject endScene;

    IEnumerator Transition() {
        yield return new WaitForSecondsRealtime(8f);
        endScene.SetActive(true);
        gameObject.SetActive(false);
    }

    void Start()
    {
       StartCoroutine(Transition()); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textbox : MonoBehaviour
{
    public bool hasStarted = false;

    bool canClose = false;

    IEnumerator DelayClose() {
        yield return new WaitForSeconds(1f);
        canClose = true;
    }

    void Start() {
        StartCoroutine(DelayClose());
    }

    void Update() {
        if(Input.anyKey && canClose) {
            gameObject.SetActive(false);
            hasStarted = true;
        }
    }
}

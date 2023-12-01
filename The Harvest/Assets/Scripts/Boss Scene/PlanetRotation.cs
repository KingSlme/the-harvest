using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] GameObject textBox;
    Textbox textBoxScript;

    void Start() {
        textBoxScript = textBox.GetComponent<Textbox>();
    }

    void Update()
    {
        if(textBoxScript.hasStarted) {
            transform.Rotate(new Vector3(0, 0, -1) * (Time.deltaTime) * 20);
        }
    }
}

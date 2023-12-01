using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionPopup : MonoBehaviour
{
    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Setup() {
        textColor = textMesh.color;
    }

    void Start()
    {
        textColor = textMesh.color;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        moveVector = new Vector3(.7f, 1) * 3f;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder + 5;
    }

    
    void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if(disappearTimer > DISAPPEAR_TIMER_MAX * .5f) {
            // First half of popup lifetime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else {
            // Second half of popup lifetime
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if(disappearTimer <0 ) {
            // Start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a <= 0) {
                Destroy(gameObject);
            }
        }

    }
}

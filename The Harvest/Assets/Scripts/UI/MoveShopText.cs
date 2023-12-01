using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShopText : MonoBehaviour
{
    private float startingY;
    private bool movingUp = true;

    void Start()
    {
        startingY = transform.position.y;
    }

    void MoveUp() {
        transform.Translate(new Vector3(0, 0.1f * Time.deltaTime, 0));
    }

    void MoveDown() {
        transform.Translate(new Vector3(0, -0.1f * Time.deltaTime, 0));
    }

    void Update()
    {
        // Y less than upper limit
        if(transform.position.y < startingY + 0.05 && movingUp) {
            MoveUp();
        }
        // Y reaches upper limit
        else {
            movingUp = false;
        }

        if(transform.position.y > startingY && !movingUp) {
            MoveDown();
        }
        else {
            movingUp = true;
        }
    }
}

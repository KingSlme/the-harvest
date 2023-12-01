using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAim : MonoBehaviour
{
    void Update() {
        Vector3 targetOrientationV3 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 targetOrientationV2 = new Vector2(targetOrientationV3.x,targetOrientationV3.y);

        transform.right = targetOrientationV2;
    }
}

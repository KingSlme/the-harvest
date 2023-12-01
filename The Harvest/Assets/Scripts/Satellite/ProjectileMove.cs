using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    float xSpeed = 20f;
    float lifeTime = 10f;

    void Update() {
        transform.position += transform.right * xSpeed * Time.deltaTime;
        // Makes sure projectile gets destroyed after period of time
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0) {
            Destroy(gameObject);
        }
    }
}

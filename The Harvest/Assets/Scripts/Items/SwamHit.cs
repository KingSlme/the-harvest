using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwamHit : MonoBehaviour
{
    float lifeTime = 15f;

    void OnTriggerEnter2D(Collider2D other) {
        // For Seed Impact
        if(other.tag == "Bomb") {
            other.GetComponent<BirdClick>().KillBomberBird();
        }
        else if(other.tag == "Asteroid") {
            other.GetComponent<Asteroid>().KillAst();
        }
    }

    void Update() {
        transform.position += new Vector3(16, 0, 0)*Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0) {
            Destroy(gameObject);
        }
    }
}

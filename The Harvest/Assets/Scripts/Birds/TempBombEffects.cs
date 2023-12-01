using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBombEffects : MonoBehaviour
{
    [SerializeField] ParticleSystem bombParticleSystem; 
    float bombLifetime = 2f;

    void Start()
    {
        bombParticleSystem.Play();
    }

    void Update()
    {
        bombLifetime -= Time.deltaTime;
        if(bombLifetime <= 0) {
            Destroy(gameObject);
        }
    }
}

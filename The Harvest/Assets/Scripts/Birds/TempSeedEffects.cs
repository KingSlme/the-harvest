using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSeedEffects : MonoBehaviour
{
    [SerializeField] ParticleSystem seedParticleSystem; 
    float seedLifetime = 2f;

    void Start()
    {
        seedParticleSystem.Play();
    }

    void Update()
    {
        seedLifetime -= Time.deltaTime;
        if(seedLifetime <= 0) {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEffects : MonoBehaviour
{
    // SFX
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] breakSounds;

    [SerializeField] ParticleSystem asteroidParticleSystem; 

    float asteroidLifetime = 2f;

    void Start()
    {
        int randomSfx = Random.Range(0,2);
        audioSource.clip = breakSounds[randomSfx];
        audioSource.Play();
        asteroidParticleSystem.Play();
    }

    void Update()
    {
        asteroidLifetime -= Time.deltaTime;
        if(asteroidLifetime <= 0) {
            Destroy(gameObject);
        }
    }
}

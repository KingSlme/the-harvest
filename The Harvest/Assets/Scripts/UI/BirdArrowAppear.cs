using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdArrowAppear : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] GameObject birdArrow;

    IEnumerator DelayAppear() {
        yield return new WaitForSecondsRealtime(10f);
        birdArrow.SetActive(true);
        audioSource.PlayOneShot(audioClip);
    }

    void Start()
    {
        StartCoroutine(DelayAppear());
    }
}

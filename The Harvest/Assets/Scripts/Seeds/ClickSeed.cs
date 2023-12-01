using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSeed : MonoBehaviour
{
    [SerializeField] ParticleSystem seedParticles;
    Animator animator;
    bool isClicked = false;
    bool isDead = false;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClipsCollect; 

    GameManager gameManager;

    [SerializeField] GameObject collectionPopup;

    [SerializeField] int seedWorth;

    AudioClip GetRandomAudioClipCollect() {
        return audioClipsCollect[Random.Range(0,2)];
    }

    public bool CheckIfClicked() {
        return isClicked;
    }

    public void SetClickStatus() {
        isClicked = false;
    }

    public void SetIsDead() {
        isDead = true;
        DisableGlow();
    }

    public void DisableGlow() {
        animator.SetBool("isGlowing", false);
    }

    void EnableGlow() {
        animator.SetBool("isGlowing", true);
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnMouseDown() {
        if(!isDead && !isClicked && !PauseMenu.GameIsPaused) {
            seedParticles.Play();
            // Audio Replaced
            /*audioSource.clip = GetRandomAudioClipCollect();
            audioSource.Play();*/
            Singleton.instance.PlayCollectSound();
            DisableGlow();
            isClicked = true;
            Instantiate(collectionPopup, transform.position, Quaternion.identity);
            gameManager.numSeeds += seedWorth;
        }
    }

    public void SimulateClick() {
        if(!isDead && !isClicked && !PauseMenu.GameIsPaused) {
            seedParticles.Play();
            // Audio Replaced
            /*audioSource.clip = GetRandomAudioClipCollect();
            audioSource.Play();*/
            Singleton.instance.PlayCollectSound();
            DisableGlow();
            isClicked = true;
            Instantiate(collectionPopup, transform.position, Quaternion.identity);
            gameManager.numSeeds += seedWorth;
        }
    }
    
    void Update()
    {
        if(!isClicked && !isDead) {
            EnableGlow();
        }
    }
}

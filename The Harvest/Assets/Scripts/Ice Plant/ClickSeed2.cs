using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSeed2 : MonoBehaviour
{
    [SerializeField] ParticleSystem seedParticles;
    [SerializeField] ParticleSystem iceParticles;
    
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClipsIce;
    [SerializeField] AudioClip[] audioClipsCollect;

    Animator animator;
    bool isClicked = false;
    bool isDead = false;

    GameManager gameManager;

    [SerializeField] GameObject collectionPopup;

    int seedWorth = 1;

    AudioClip GetRandomAudioClipIce() {
        return audioClipsIce[Random.Range(0,3)];
    }

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
        animator.SetBool("hasDied", true);
        // Ice Plant 
        animator.SetBool("isFrozen", false);
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
        if(!isDead && !isClicked && ! animator.GetBool("isFrozen") && !PauseMenu.GameIsPaused) {
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

    void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.F) && animator.GetBool("isFrozen") && !isDead && !PauseMenu.GameIsPaused) {
            iceParticles.Play();
            audioSource.clip = GetRandomAudioClipIce();
            audioSource.Play();
            animator.SetBool("isFrozen", false);
            animator.SetBool("isGlowing", true);
        }
    }

    public void SimulateClick() {
        if(animator.GetBool("isFrozen") && !isDead && !PauseMenu.GameIsPaused) {
            iceParticles.Play();
            audioSource.clip = GetRandomAudioClipIce();
            audioSource.Play();
            animator.SetBool("isFrozen", false);
            animator.SetBool("isGlowing", true);
        }

        if(!isDead && !isClicked && ! animator.GetBool("isFrozen") && !PauseMenu.GameIsPaused) {
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
        // Changed for Ice
        /*if(!isClicked && !isDead) {
            EnableGlow();
        }*/

        // Needs to  happen if plant resets
        // NOT Dead NOT Clicked
        // Need to detect after unfrozen

        //
        if(!isDead && !isClicked && !animator.GetBool("isFrozen") && !animator.GetBool("isGlowing")) {
            
            animator.SetBool("isFrozen", true);
        }
    }
}

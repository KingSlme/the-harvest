using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantDeath : MonoBehaviour
{
    [SerializeField] Slider sliderObject;
    Slider sliderComponent;
    Animator animator;

    PlantInfo plantInfo;

    GameManager gameManager;
    public bool hasAlreadDied = false;

    // For Cactus
    [SerializeField] bool isCactus;

    // SFX
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathSound;

    void Start()
    {
        sliderComponent = sliderObject.GetComponent<Slider>();
        animator = GetComponent<Animator>();
        plantInfo = GetComponentInParent<PlantInfo>();
        gameManager = FindObjectOfType<GameManager>();
    }

    bool CheckIfDead() {
        if(sliderComponent.value == sliderComponent.maxValue) {
            return true;
        }
        else {
            return false;
        }
    }

    public void KillFlower() {
        audioSource.clip = deathSound;
        audioSource.Play();

        animator.SetBool("isDead", true);
        FirstDeactivation();
        StartCoroutine(DelayDeactivation());
        StartCoroutine(BackupDelayDeactivation());
        StartCoroutine(BackupDelayDeactivation2());
        StartCoroutine(BackupDelayDeactivation3());
        StartCoroutine(BackupDelayDeactivation4());
        StartCoroutine(BackupDelayDeactivation5());
        // Old Lives
        // gameManager.numLives--;
        Singleton.instance.DecrementCurrentLives();
        // For Cactus
        if(!isCactus) {
            gameManager.numOfDeadPlants++;
        }
        
        sliderComponent.value = sliderComponent.maxValue;
        hasAlreadDied = true;
    }

    void FirstDeactivation() {
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed>().SetIsDead();
        }
    }

    IEnumerator DelayDeactivation() {
        yield return new WaitForSecondsRealtime(1f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed>().SetIsDead();
        }
    }

    IEnumerator BackupDelayDeactivation4() {
        yield return new WaitForSecondsRealtime(0.5f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed>().SetIsDead();
        }
    }

    IEnumerator BackupDelayDeactivation5() {
        yield return new WaitForSecondsRealtime(2.5f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed>().SetIsDead();
        }
    }

    IEnumerator BackupDelayDeactivation() {
        yield return new WaitForSecondsRealtime(1.5f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed>().SetIsDead();
        }
    }

    IEnumerator BackupDelayDeactivation2() {
        yield return new WaitForSecondsRealtime(2f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed>().SetIsDead();
        }
    }

    IEnumerator BackupDelayDeactivation3() {
        yield return new WaitForSecondsRealtime(3f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed>().SetIsDead();
        }
    }

    void Update()
    {
        if(CheckIfDead() && !hasAlreadDied) {
            KillFlower();
        }
    }
}

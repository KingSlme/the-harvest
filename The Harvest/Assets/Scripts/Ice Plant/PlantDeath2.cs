using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantDeath2 : MonoBehaviour
{
    [SerializeField] Slider sliderObject;
    Slider sliderComponent;
    Animator animator;

    PlantInfo2 plantInfo;

    GameManager gameManager;
    public bool hasAlreadDied = false;

    // SFX
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathSound;

    void Start()
    {
        sliderComponent = sliderObject.GetComponent<Slider>();
        animator = GetComponent<Animator>();
        plantInfo = GetComponentInParent<PlantInfo2>();
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
        //gameManager.numLives--;
        Singleton.instance.DecrementCurrentLives();
        gameManager.numOfDeadPlants++;
        sliderComponent.value = sliderComponent.maxValue;
        hasAlreadDied = true;
    }

    void FirstDeactivation() {
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed2>().SetIsDead();
        }
    }

    IEnumerator DelayDeactivation() {
        yield return new WaitForSecondsRealtime(1f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed2>().SetIsDead();
        }
    }

    IEnumerator BackupDelayDeactivation4() {
        yield return new WaitForSecondsRealtime(1.5f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed2>().SetIsDead();
        }
    }

    IEnumerator BackupDelayDeactivation5() {
        yield return new WaitForSecondsRealtime(1.5f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed2>().SetIsDead();
        }
    }

    IEnumerator BackupDelayDeactivation() {
        yield return new WaitForSecondsRealtime(1.5f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed2>().SetIsDead();
        }
    }

    IEnumerator BackupDelayDeactivation2() {
        yield return new WaitForSecondsRealtime(2f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed2>().SetIsDead();
        }
    }

    IEnumerator BackupDelayDeactivation3() {
        yield return new WaitForSecondsRealtime(3f);
        foreach(GameObject o in plantInfo.seedList) {
            o.GetComponent<ClickSeed2>().SetIsDead();
        }
    }

    void Update()
    {
        if(CheckIfDead() && !hasAlreadDied) {
            KillFlower();
        }
    }
}

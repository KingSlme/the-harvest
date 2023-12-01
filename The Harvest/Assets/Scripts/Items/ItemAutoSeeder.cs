using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ItemAutoSeeder : MonoBehaviour
{
    bool onCooldown = false;
    [SerializeField] GameObject textPopup;
    [SerializeField] TextMeshPro textBox;
    [SerializeField] Sprite availableItem;
    [SerializeField] Sprite notAvailableItem;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip autoSeeder;

    [SerializeField] GameObject monsterBG;

    void Start() {
        if(!Singleton.instance.hasAutoSeeder) {
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;
        }
        if(Singleton.instance.autoSeederCooldown > 0) {
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;
            onCooldown = true;
        }
        if(Singleton.instance.hasAutoSeeder && Singleton.instance.autoSeederCooldown <= 0) {
            gameObject.GetComponent<SpriteRenderer>().sprite = availableItem;
        }
    }

    int numToGen;
    bool canGen;

    void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !onCooldown && Singleton.instance.hasAutoSeeder) {
            onCooldown = true;
            Singleton.instance.autoSeederCooldown = 50f;
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;

            StartCoroutine(genSeeds());
            audioSource.PlayOneShot(autoSeeder);
            StartCoroutine(DelayBGDestroy());
        }
    }

    IEnumerator DelayBGDestroy() {
        GameObject bg = Instantiate(monsterBG, new Vector3(0,0,0), Quaternion.identity);
        yield return new WaitForSecondsRealtime(1f);
        Destroy(bg);
    }

    IEnumerator genSeeds() {
        GameManager g = FindObjectOfType<GameManager>();
        for(int i = 0; i<20; i++) {
            Instantiate(textPopup, transform.position, Quaternion.identity);
            g.numSeeds++;
            yield return new WaitForSeconds(.5f);
        }
    }

    // Items Parent will be activated my GameManager to stop timers during textbox incidents
    void Update() {
        if(onCooldown) {
            Singleton.instance.autoSeederCooldown -= Time.deltaTime;
            textBox.text = ((int)Singleton.instance.autoSeederCooldown).ToString();
        }
        if(Singleton.instance.autoSeederCooldown <= 0 && onCooldown) {
            onCooldown = false;
            textBox.text = "";
            gameObject.GetComponent<SpriteRenderer>().sprite = availableItem;
        }
    }
}
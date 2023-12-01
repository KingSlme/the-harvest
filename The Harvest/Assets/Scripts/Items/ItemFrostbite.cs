using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemFrostbite : MonoBehaviour
{
    bool onCooldown = false;
    [SerializeField] GameObject frostbiteBG;
    [SerializeField] TextMeshPro textBox;
    [SerializeField] Sprite availableItem;
    [SerializeField] Sprite notAvailableItem;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip frostBite;

    void Start() {
        if(!Singleton.instance.hasFrostbite) {
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;
        }
        if(Singleton.instance.frostbiteCooldown > 0) {
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;
            onCooldown = true;
        }
        if(Singleton.instance.hasFrostbite && Singleton.instance.frostbiteCooldown <= 0) {
            gameObject.GetComponent<SpriteRenderer>().sprite = availableItem;
        }
    }

    // Made it so timeScale set to 1f at start of GameManager to avoid freeze lasting
    IEnumerator ResumeNormalTime() {
        GameObject bg = Instantiate(frostbiteBG, new Vector3(0,0,0), Quaternion.identity);
        // Not real time, so pause works with it
        // .75 x 4 mult = 3 secs
        yield return new WaitForSeconds(.75f);
        Time.timeScale = 1f;
        Destroy(bg);
    }

    void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !onCooldown && Singleton.instance.hasFrostbite) {
            onCooldown = true;
            Singleton.instance.frostbiteCooldown = 20f;
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;
            Time.timeScale = .25f;
            StartCoroutine(ResumeNormalTime());
            audioSource.PlayOneShot(frostBite);
        }
    }

    // Items Parent will be activated my GameManager to stop timers during textbox incidents
    void Update() {
        if(onCooldown) {
            Singleton.instance.frostbiteCooldown -= Time.deltaTime;
            textBox.text = ((int)Singleton.instance.frostbiteCooldown).ToString();
        }
        if(Singleton.instance.frostbiteCooldown <= 0 && onCooldown) {
            onCooldown = false;
            textBox.text = "";
            gameObject.GetComponent<SpriteRenderer>().sprite = availableItem;
        }
    }
}

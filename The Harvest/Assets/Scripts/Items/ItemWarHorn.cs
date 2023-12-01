using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ItemWarHorn : MonoBehaviour
{
    bool onCooldown = false;
    [SerializeField] GameObject birdSwarm;
    [SerializeField] TextMeshPro textBox;
    [SerializeField] Sprite availableItem;
    [SerializeField] Sprite notAvailableItem;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip warHorn_horn;
    [SerializeField] AudioClip warHorn_bird;
    
    void Start() {
        if(!Singleton.instance.hasWarHorn) {
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;
        }
        if(Singleton.instance.warHornCooldown > 0) {
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;
            onCooldown = true;
        }
        if(Singleton.instance.hasWarHorn && Singleton.instance.warHornCooldown <= 0) {
            gameObject.GetComponent<SpriteRenderer>().sprite = availableItem;
        }
    }

    void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !onCooldown && Singleton.instance.hasWarHorn) {
            onCooldown = true;
            Singleton.instance.warHornCooldown = 20f;
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;
            Instantiate(birdSwarm, new Vector3(-10,1.5f,0), Quaternion.identity);
            StartCoroutine(WarHornAudio());
        }
    }

    IEnumerator WarHornAudio()
    {
        audioSource.PlayOneShot(warHorn_horn);
        yield return new WaitForSecondsRealtime(.5f);
        audioSource.PlayOneShot(warHorn_bird);
    }

    // Items Parent will be activated my GameManager to stop timers during textbox incidents
    void Update() {
        if(onCooldown) {
            Singleton.instance.warHornCooldown -= Time.deltaTime;
            textBox.text = ((int)Singleton.instance.warHornCooldown).ToString();
        }
        if(Singleton.instance.warHornCooldown <= 0 && onCooldown) {
            onCooldown = false;
            textBox.text = "";
            gameObject.GetComponent<SpriteRenderer>().sprite = availableItem;
        }
    }
}

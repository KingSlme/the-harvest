using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFertilizer : MonoBehaviour
{
    [SerializeField] Sprite originalButton;
    [SerializeField] Sprite hoverButton;
    [SerializeField] Sprite clickedButton;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip click; 
    
    bool canBeClicked = true;

    [SerializeField] GameObject textObject;

    IEnumerator delayClick() {
        canBeClicked = false;
        audioSource.clip = click;
        audioSource.Play();
        GetComponent<SpriteRenderer>().sprite = clickedButton;
        yield return new WaitForSecondsRealtime(0.1f);
        GetComponent<SpriteRenderer>().sprite = originalButton;
        canBeClicked = true;
    }

    void OnMouseEnter() {
        audioSource.clip = hover;
        audioSource.Play();
        GetComponent<SpriteRenderer>().sprite = hoverButton;
        textObject.SetActive(true);
    }

    void OnMouseExit() {
        GetComponent<SpriteRenderer>().sprite = originalButton;
        textObject.SetActive(false);
    }

    void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canBeClicked && !Singleton.instance.hasFertilizer && Singleton.instance.currentShopCurrency >= 2) {
            Singleton.instance.hasFertilizer = true;
            Singleton.instance.currentShopCurrency-=2;
            StartCoroutine(delayClick());
        }
    }

    void Update()
    {
        if(Singleton.instance.hasFertilizer) {
            GetComponent<SpriteRenderer>().sprite = clickedButton;
        }
    }
}

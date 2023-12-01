using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAutoSeeder : MonoBehaviour
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
        if(Input.GetKeyDown(KeyCode.Mouse0) && canBeClicked && !Singleton.instance.hasAutoSeeder && Singleton.instance.currentShopCurrency >= 5) {
            Singleton.instance.hasAutoSeeder = true;
            Singleton.instance.currentShopCurrency-=5;
            StartCoroutine(delayClick());
        }
    }

    void Update()
    {
        if(Singleton.instance.hasAutoSeeder) {
            GetComponent<SpriteRenderer>().sprite = clickedButton;
        }
    }
}

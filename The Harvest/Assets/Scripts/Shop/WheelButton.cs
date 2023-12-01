using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelButton : MonoBehaviour
{
    [SerializeField] GameObject[] wheelLights;
    [SerializeField] Sprite darkLight;
    [SerializeField] Sprite lightLight;

    [SerializeField] Sprite originalButton;
    [SerializeField] Sprite hoverButton;
    [SerializeField] Sprite clickedButton;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip click; 

    bool canBeClicked = true;

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
    }

    void OnMouseExit() {
        GetComponent<SpriteRenderer>().sprite = originalButton;
    }

    void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canBeClicked && !isSpinning && Singleton.instance.currentShopCurrency >= 3) {
            Singleton.instance.currentShopCurrency -= 3;
            StartCoroutine(delayClick());
            StartCoroutine(SpinWheel());
        }
    }

    bool isSpinning = false;
    int currentLightIndex = 0;

    IEnumerator SpinWheel() {
        isSpinning = true;
        for(int i = 0; i<Random.Range(30, 45); i++) {
            audioSource.clip = hover;
            audioSource.Play();
            wheelLights[currentLightIndex].GetComponent<SpriteRenderer>().sprite = lightLight;
            yield return new WaitForSecondsRealtime(0.1f);
            wheelLights[currentLightIndex].GetComponent<SpriteRenderer>().sprite = darkLight;
            if(currentLightIndex!=14) {
                currentLightIndex++;
            }
            else {
                currentLightIndex = 0;
            }
        }
        wheelLights[currentLightIndex].GetComponent<SpriteRenderer>().sprite = lightLight;
        isSpinning = false;
        // Now use currentLightIndex to give rewards
        if(currentLightIndex <= 2) { // 0 1 2 
            Singleton.instance.hasFrostbite = true;
        }
        else if(currentLightIndex >= 3 && currentLightIndex <= 5) { // 3 4 5 
            Singleton.instance.hasFertilizer = true;
        }
        else if(currentLightIndex >= 6 && currentLightIndex <= 8) { // 6 7 8
            Singleton.instance.currentShopCurrency+=6;
        }
        else if(currentLightIndex >= 9 && currentLightIndex <= 11) { // 9 10 11
            Singleton.instance.hasAutoSeeder = true;
        }
        else { // 12 13 14
            Singleton.instance.hasFertilizer = true;
        }
        // Reset currentLightIndex
        currentLightIndex = 0;
    }
}

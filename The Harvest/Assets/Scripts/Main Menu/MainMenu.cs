using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Sprite originalButtonImage;
    [SerializeField] Sprite pressedButtonImage;
    [SerializeField] Sprite hoverButtonImage;
    float delayTime = 0.1f;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonHoverSound;
    [SerializeField] AudioClip buttonClickSound;

    IEnumerator DelayNextScene() {
        gameObject.GetComponent<Image>().sprite = pressedButtonImage;
        yield return new WaitForSecondsRealtime(delayTime);
        SceneManager.LoadScene(1);
    }

    public void Play() {
        // Reset currency
        Singleton.instance.currentShopCurrency = 0;
        Singleton.instance.ResetCurrentLives();
        audioSource.clip = buttonClickSound;
        audioSource.Play();
        StartCoroutine(DelayNextScene());
    }

    public void Hover() {
        gameObject.GetComponent<Image>().sprite = hoverButtonImage;
        audioSource.clip = buttonHoverSound;
        audioSource.Play();
    }

    public void StopHover() {
        gameObject.GetComponent<Image>().sprite = originalButtonImage;
    }
}

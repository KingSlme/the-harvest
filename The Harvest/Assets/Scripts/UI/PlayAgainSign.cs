using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainSign : MonoBehaviour
{   
    [SerializeField] Sprite originalSign;
    [SerializeField] Sprite hoverSign;
    [SerializeField] Sprite clickedSign;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonHoverSound;
    [SerializeField] AudioClip buttonClickSound;
    
    IEnumerator ClickSign() {
        gameObject.GetComponent<SpriteRenderer>().sprite = clickedSign;
        yield return new WaitForSecondsRealtime(0.1f);
        SceneManager.LoadScene(1);
    }

    void OnMouseDown() {
        audioSource.clip = buttonClickSound;
        audioSource.Play();
        StartCoroutine(ClickSign());
    }

    void OnMouseEnter() {
        audioSource.clip = buttonHoverSound;
        audioSource.Play();
        gameObject.GetComponent<SpriteRenderer>().sprite = hoverSign;
    }

    void OnMouseExit() {
        gameObject.GetComponent<SpriteRenderer>().sprite = originalSign;
    }
}

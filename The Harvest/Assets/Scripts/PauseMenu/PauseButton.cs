using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] Sprite originalButtonImage;
    [SerializeField] Sprite hoverButtonImage;
    [SerializeField] Sprite pressedButtonImage;
    float delayTime = 0.1f;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonHoverSound;
    [SerializeField] AudioClip buttonClickSound;

    public bool canBePressed = true;

    private void OnMouseEnter() {
        if(canBePressed) {
            GetComponent<SpriteRenderer>().sprite = hoverButtonImage;
            audioSource.clip = buttonHoverSound;
            audioSource.Play();
        }
    }

    private void OnMouseOver() {
        if(canBePressed) {
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                canBePressed = false;
                GetComponent<SpriteRenderer>().sprite = pressedButtonImage;
                audioSource.clip = buttonClickSound;
                audioSource.Play();
                GetComponent<PauseMenu>().Pause();
                StartCoroutine(DelayOriginalImage());
            }
        }
    }

    IEnumerator DelayOriginalImage() {
        yield return new WaitForSecondsRealtime(delayTime);
        GetComponent<SpriteRenderer>().sprite = originalButtonImage;
    }

    private void OnMouseExit() {
        GetComponent<SpriteRenderer>().sprite = originalButtonImage;
    }
}

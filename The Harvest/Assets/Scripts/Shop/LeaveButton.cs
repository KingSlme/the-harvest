using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveButton : MonoBehaviour
{
    [SerializeField] Sprite originalButton;
    [SerializeField] Sprite hoverButton;
    [SerializeField] Sprite clickedButton;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip click; 

    bool canBeClicked = true;

    IEnumerator delayLeave() {
        canBeClicked = false;
        audioSource.clip = click;
        audioSource.Play();
        GetComponent<SpriteRenderer>().sprite = clickedButton;
        yield return new WaitForSecondsRealtime(0.1f);
        SceneManager.LoadScene(Singleton.instance.GetCurrentIndexCheckpoint()+3);
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
        if(Input.GetKeyDown(KeyCode.Mouse0) && canBeClicked) {
            StartCoroutine(delayLeave());
        }
    }
}

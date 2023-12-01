using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    Sprite currentTextImage;
    [SerializeField] Sprite[] textImages;
    float transitionDelay = 0.25f;
    bool canTransition = true;
    int currentTextImageIndex = 0;

    IEnumerator ChangeTextImage() {
        canTransition = false;
        if(currentTextImageIndex == 0) {
            currentTextImageIndex =  1;
            gameObject.GetComponent<Image>().sprite = textImages[1];
        }
        else if(currentTextImageIndex == 1) {
            currentTextImageIndex = 2;
            gameObject.GetComponent<Image>().sprite = textImages[2];
        }
        else { // for currentTextImageIndex == 2
            currentTextImageIndex = 0;
            gameObject.GetComponent<Image>().sprite = textImages[0];
        }
        yield return new WaitForSecondsRealtime(transitionDelay);
        canTransition = true;
    }

    void Update()
    {
        if(canTransition) {
            StartCoroutine(ChangeTextImage());
        }
    }
}

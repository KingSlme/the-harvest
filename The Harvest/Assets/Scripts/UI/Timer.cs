using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] Slider sliderObject; 
    [SerializeField] TextMeshProUGUI countdown;
    [SerializeField] GameObject fill;

    Slider sliderComponent;
    Text text;
    Image image;

    Color32 yellow = new Color32(255,255,0,255);
    Color32 orange = new Color32(255,127,0, 255);
    Color32 red = new Color32(255,0,0,255);
    Color32 green = new Color32(0, 255, 21, 255);
    Color32 white = new Color32(255, 255, 255, 255);

    void ChangeColor() {
        if(sliderComponent.value < sliderComponent.maxValue/4.0) {
            image.color = green;
            countdown.color = white;
        }
        else if(sliderComponent.value > sliderComponent.maxValue/4.0 && sliderComponent.value < sliderComponent.maxValue/1.96) {
            image.color = yellow;
            countdown.color = white;
        }
        else if(sliderComponent.value > sliderComponent.maxValue/2.0 && sliderComponent.value < sliderComponent.maxValue/1.32) {
            image.color = orange;
            countdown.color = white;
        }
        else if(sliderComponent.value > sliderComponent.maxValue/1.33) {
            image.color = red;
            countdown.color = red;
        }
    }

    void Start()
    {
        sliderComponent = sliderObject.GetComponent<Slider>();
        image = fill.GetComponent<Image>();
    }


    void Update()
    {
        sliderComponent.value = Mathf.MoveTowards(sliderComponent.value, sliderComponent.maxValue, 10* Time.deltaTime);
        countdown.text = (((int)(sliderComponent.maxValue-((int)sliderComponent.value))/10)).ToString();
        ChangeColor();
    }
}

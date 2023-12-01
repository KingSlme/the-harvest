using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BirdClick : MonoBehaviour
{
    private float lives = 3;
    bool hasntDied = true;
    [SerializeField] bool canBeClicked;
    // [SerializeField] GameObject clickPopup;
    [SerializeField] bool isBomber;
    TextMeshPro textMeshPro;

    // [SerializeField] ParticleSystem bloodParticleSystem; 

    [SerializeField] GameObject newPopup;

    [SerializeField] bool isSeedBird; // false means is flower bird

    // No F Added Because of Ice Plant Mechanic
    KeyCode[] keyCodes = {
    KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
    KeyCode.G, /*KeyCode.H, KeyCode.I,*/ KeyCode.J, KeyCode.K, 
    KeyCode.L, /*KeyCode.M,*/ /*KeyCode.N,*/ KeyCode.O, KeyCode.P, 
    KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, 
    KeyCode.V, /*KeyCode.W,*/ KeyCode.X, KeyCode.Y, /*KeyCode.Z*/
    };

    string[] strings = {
        "A", "B", "C", "D", "E",
        "G", /*"H", "I",*/ "J", "K",
        "L", /*"M",*/ /*"N",*/ "O", "P",
        "Q", "R", "S", "T", "U",
        "V", /*"W",*/ "X", "Y", /*"Z"*/
    };

    KeyCode correctKeyCode;
    string correctString;

    [SerializeField] GameObject tempEffects;
    [SerializeField] GameObject tempEffectsHit;

    TextMeshPro birdHealthbar;

    IEnumerator ClickCoolDown() {
        canBeClicked = false;
        yield return new WaitForSecondsRealtime(.5f);
        canBeClicked = true;
    }

    /*IEnumerator DelayDeath() {
        yield return new WaitForSecondsRealtime(.25f);
        Destroy(gameObject);
    }*/

    public void KillBomberBird() {
        hasntDied = false;
        // bloodParticleSystem.Play();
        Singleton.instance.PlayBirdExplodeSound();
        // StartCoroutine(DelayDeath());
        Instantiate(tempEffects, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canBeClicked && !PauseMenu.GameIsPaused) {
            lives--;
            Singleton.instance.PlayBirdHitSound();
            if(lives > 0) {
                // Instantiate(clickPopup, transform.position, Quaternion.identity);
                if(lives != 0) {
                    Instantiate(tempEffectsHit, transform.position, Quaternion.identity);
                    birdHealthbar.text = lives.ToString()+"/3";
                }
            }
            StartCoroutine(ClickCoolDown());
        }
   }

   void OnTriggerEnter2D(Collider2D other) {
    if(isBomber && other.tag == "Plant" && hasntDied) {
        if(!other.gameObject.GetComponentInChildren<PlantDeath>().hasAlreadDied) {
            other.gameObject.GetComponentInChildren<PlantDeath>().KillFlower();
        }
        KillBomberBird();
    }
    else if(isBomber && other.tag == "Plant2" && hasntDied) {
        if(!other.gameObject.GetComponentInChildren<PlantDeath2>().hasAlreadDied) {
            other.gameObject.GetComponentInChildren<PlantDeath2>().KillFlower();
        }
        KillBomberBird();
    }
    else if(isBomber && other.tag == "Boss" && hasntDied) {
        if(!other.gameObject.GetComponentInChildren<PlantDeathBoss>().hasAlreadDied) {
            other.gameObject.GetComponentInChildren<PlantDeathBoss>().KillFlower();
        }
        KillBomberBird();
    }
   }

    void Awake() {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        birdHealthbar = GetComponentInChildren<TextMeshPro>();
    }

    void Start() {
        if(isBomber) {
            int randomNum = Random.Range(0, 25-4-2);
            correctKeyCode = keyCodes[randomNum];
            correctString = strings[randomNum];
            textMeshPro.text = correctString;
        }
    }

    void Update() {
        if(lives <= 0 && hasntDied) {
            hasntDied = false;
            // bloodParticleSystem.Play();
            Singleton.instance.PlayBirdDeathSound();
            if(isSeedBird) {
                Instantiate(newPopup, transform.position, Quaternion.identity);
                FindObjectOfType<GameManager>().numSeeds += 10;
            }
            else if(!isSeedBird) {
                Instantiate(newPopup, transform.position, Quaternion.identity);
                Singleton.instance.IncremenetCurrentLives();
            }
            // StartCoroutine(DelayDeath());
            Instantiate(tempEffects, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // Bomber Bird
        if(isBomber) {
            if(Input.GetKeyDown(correctKeyCode) && hasntDied && !PauseMenu.GameIsPaused) {
                KillBomberBird();
            }
        }
    }
}

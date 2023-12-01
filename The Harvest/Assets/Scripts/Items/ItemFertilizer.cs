using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemFertilizer : MonoBehaviour
{
    bool onCooldown = false;
    [SerializeField] TextMeshPro textBox;
    [SerializeField] Sprite availableItem;
    [SerializeField] Sprite notAvailableItem;

    GameObject[] allPlants;
    GameObject[] allPlants2;
    GameObject[] allPlantsBoss;

    List<GameObject> totalPlants;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip fertilizer;

    void Start() {
        if(!Singleton.instance.hasFertilizer) {
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;
        }
        if(Singleton.instance.fertilizerCooldown > 0) {
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;
            onCooldown = true;
        }
        if(Singleton.instance.hasFertilizer && Singleton.instance.fertilizerCooldown <= 0) {
            gameObject.GetComponent<SpriteRenderer>().sprite = availableItem;
        }

        allPlants = GameObject.FindGameObjectsWithTag("Plant");
        allPlants2 = GameObject.FindGameObjectsWithTag("Plant2");
        allPlantsBoss = GameObject.FindGameObjectsWithTag("Boss");

        totalPlants = new List<GameObject>();
        foreach(GameObject o in allPlants) {
            if(!o.GetComponentInChildren<PlantDeath>().hasAlreadDied) {
                totalPlants.Add(o);
            }
        }
        foreach(GameObject o in allPlants2) {
            if(!o.GetComponentInChildren<PlantDeath2>().hasAlreadDied) {
                totalPlants.Add(o);
            }
        }
        foreach(GameObject o in allPlantsBoss) {
            if(!o.GetComponentInChildren<PlantDeathBoss>().hasAlreadDied) {
                totalPlants.Add(o);
            }
        }
    }

    IEnumerator DelaySeedCollection() {
        yield return new WaitForSeconds(0.25f);
        int randInt = Random.Range(0, totalPlants.Count);
            if(totalPlants[randInt].tag == "Plant") {
                ClickSeed[] allSeeds = totalPlants[randInt].GetComponentsInChildren<ClickSeed>();
                foreach(ClickSeed o in allSeeds) {
                    o.SimulateClick();
                }
            }
            if(totalPlants[randInt].tag == "Plant2") {
                ClickSeed2[] allSeeds2 = totalPlants[randInt].GetComponentsInChildren<ClickSeed2>();
                foreach(ClickSeed2 o in allSeeds2) {
                    o.SimulateClick();
                }
            }
            if(totalPlants[randInt].tag == "Boss") {
                ClickSeedBoss[] allSeedsBoss = totalPlants[randInt].GetComponentsInChildren<ClickSeedBoss>();
                foreach(ClickSeedBoss o in allSeedsBoss) {
                    o.SimulateClick();
                }
            }
    }

    void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !onCooldown && Singleton.instance.hasFertilizer) {
            onCooldown = true;
            Singleton.instance.fertilizerCooldown = 20f;
            gameObject.GetComponent<SpriteRenderer>().sprite = notAvailableItem;

            // Get Random Alive Plant AND Collect its Seeds
            // Delayed so seeds can gen (small buffer) ur fault if u suck TBH
            StartCoroutine(DelaySeedCollection());
            audioSource.PlayOneShot(fertilizer);
        }
    }

    // Items Parent will be activated my GameManager to stop timers during textbox incidents
    void Update() {
        if(onCooldown) {
            Singleton.instance.fertilizerCooldown -= Time.deltaTime;
            textBox.text = ((int)Singleton.instance.fertilizerCooldown).ToString();
        }
        if(Singleton.instance.fertilizerCooldown <= 0 && onCooldown) {
            onCooldown = false;
            textBox.text = "";
            gameObject.GetComponent<SpriteRenderer>().sprite = availableItem;
        }

        // Getting all alive plants (bad practice ik)
        totalPlants = new List<GameObject>();
        foreach(GameObject o in allPlants) {
            if(!o.GetComponentInChildren<PlantDeath>().hasAlreadDied) {
                totalPlants.Add(o);
            }
        }
        foreach(GameObject o in allPlants2) {
            if(!o.GetComponentInChildren<PlantDeath2>().hasAlreadDied) {
                totalPlants.Add(o);
            }
        }
        foreach(GameObject o in allPlantsBoss) {
            if(!o.GetComponentInChildren<PlantDeathBoss>().hasAlreadDied) {
                totalPlants.Add(o);
            }
        }
    }
}

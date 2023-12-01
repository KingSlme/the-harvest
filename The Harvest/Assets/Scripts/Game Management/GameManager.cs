using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI seedCounterText;
    [SerializeField] TextMeshProUGUI lifeCounterText;

    [SerializeField] GameObject[] objectsToActivate;
    [SerializeField] GameObject[] textBox;
    Textbox textBoxScript;

    public int numSeeds = 0;
    [SerializeField] int numRequiredSeeds;
    public int numLives = 3;
    public int maxNumLives = 3;
    public int numOfDeadPlants = 0;

    int numOfGeneratedSeeds = 0;

    [SerializeField] GameObject spawner;

    [SerializeField] GameObject items;

    public int GetNumOfGeneratedSeeds() {
        return numOfGeneratedSeeds;
    }

    public void ResetNumOfGeneratedSeeds() {
        numOfGeneratedSeeds = 0;
    }

    public void IncrementNumOfGeneratedSeeds() {
        numOfGeneratedSeeds++;
    }

    // Losing
    IEnumerator LosingScene() {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("YouLost");
    }
    // Plant 3
    int GetNumOfPlant3() {
        int count = 0;
        foreach(GameObject o in objectsToActivate) {
            if(o.name.Substring(0,7) == "Plant 3") {
                count++;
            }
        }
        return count;
    }

    void Start() {
        Time.timeScale = 1f;
        if(textBox.Length > 0) {
            textBoxScript = textBox[0].GetComponent<Textbox>();
        }
        // Old Lives
        numLives = objectsToActivate.Length;
    }

    void Update() {
        seedCounterText.text = numSeeds + "/" + numRequiredSeeds;
        // Old Lives
        // lifeCounterText.text = numLives + "/" + objectsToActivate.Length;
        lifeCounterText.text = Singleton.instance.GetCurrentLives() + "/" + maxNumLives;
        if(textBox.Length > 0 && textBoxScript.hasStarted) {
            foreach(GameObject o in objectsToActivate) {
                o.SetActive(true);
            }
            // Bird Spawner
            spawner.SetActive(true);
            // Items
            items.SetActive(true);
        }
        if(numSeeds >= numRequiredSeeds) {
            if(SceneManager.GetActiveScene().name == "Boss") {
                SceneManager.LoadScene("YouWin");
            }
            // 3 6 9 12 are the finals for each biome
            if(SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 9 || SceneManager.GetActiveScene().buildIndex == 12) {
                // Adds currency
                Singleton.instance.currentShopCurrency += Singleton.instance.GetCurrentLives();
                SceneManager.LoadScene("Store");
            }
            else {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        // Losing:
        // Old Lives
        /*if(numLives <= 0 || numOfDeadPlants >= objectsToActivate.Length) {
            StartCoroutine(LosingScene());
        }*/

        // Singleton Reference
        if(Singleton.instance.GetCurrentLives() <= 0 || numOfDeadPlants >= objectsToActivate.Length - GetNumOfPlant3()) {
            StartCoroutine(LosingScene());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantInfo2 : MonoBehaviour
{
    string lastSpawnDirection; 
    [SerializeField] GameObject timer;
    [SerializeField] Slider sliderObject;
    Slider sliderComponent;

    public List<GameObject> seedList;
    float time = 0;

    // Regen
    bool allClickedEventBegan = false;
    GameManager gameManager;
    [SerializeField] ParticleSystem completionParticles;
    
    public List<GameObject> GetSeedList() {
        return seedList;
    }

    public void AddToSeedList(GameObject seed) {
        seedList.Add(seed);
    }

    // For Seed Checking
    bool AllSeedsClicked() {
        // Regen
        
        if(seedList.Count < 1) {
            return false;
        }        

        foreach(GameObject o in seedList) {
            if(o.GetComponent<ClickSeed2>().CheckIfClicked() == false) {
                return false;
            }
        }
        return true;
    }

    IEnumerator DelayAllSeedsClicked() {
        yield return new WaitForSecondsRealtime(1f);
    }

    void Start()
    {
        // Places timer next to flower
        timer.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x+1, transform.position.y+1, 0));
        sliderComponent = sliderObject.GetComponent<Slider>();
        // Regen
        gameManager = FindObjectOfType<GameManager>();
    }
    
    IEnumerator DelayDestroy() {
        yield return new WaitForSecondsRealtime(.3f);
        foreach(GameObject o in seedList) {
        o.GetComponent<ClickSeed2>().SetClickStatus();
        // Regen
        Destroy(o);
        }
        // New Clear
        seedList.Clear();
    }

    // New
    IEnumerator AllClickStopper() {
        // After AllSeedsClicked event, event begins then stops after delay
        allClickedEventBegan = true;
        yield return new WaitForSecondsRealtime(1f);
        allClickedEventBegan = false;
    }

    public int numOfSeeds = 0;

    void Update()
    {   
        if(time < 3) {
            time += Time.deltaTime;
        }
        if(time > 2) {
            if(AllSeedsClicked() && !allClickedEventBegan && !GetComponentInChildren<PlantDeath2>().hasAlreadDied) {
                // Regen
                StartCoroutine(AllClickStopper());
                // allClickedEventBegan = true;
                completionParticles.Play();
                Singleton.instance.PlayCompletionSound();

                sliderComponent.value = 0;
                // Regen
                GenerateSeed[] generateSeeds = GetComponentsInChildren<GenerateSeed>();

                StartCoroutine(DelayDestroy());
                // Regen
                //seedList.Clear();

                //gameManager.ResetNumOfGeneratedSeeds();
                numOfSeeds = 0;
                foreach(GenerateSeed generateSeed in generateSeeds) {
                    generateSeed.NewGeneration();
                }
                //allClickedEventBegan = false;
            }
        }
    }    
}

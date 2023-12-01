using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSeed : MonoBehaviour
{
    [SerializeField] GameObject seed;
    [SerializeField] GameObject[] seedSpawnpoints;

    GameManager gameManager;

    GameObject RandomSeedSpawn() {
        int randomNum = Random.Range(0,4);
        return seedSpawnpoints[randomNum];
    }

    void GetNextSeed() {
        StartCoroutine(SeedDelay());
        // Move to Coroutine
        // Instantiate(seed, RandomSeedSpawn().transform.position, Quaternion.identity, this.transform);
    }

    // Original Chance 
    /*bool GetChanceForSeed() {
        // Ensures some seeds are generated:
        if(gameManager.GetNumOfGeneratedSeeds() < 6) {
            gameManager.IncrementNumOfGeneratedSeeds();
            return true;
        }
        else if(gameManager.GetNumOfGeneratedSeeds() < 8) {
            int randomNum = Random.Range(0,3);
            if(randomNum == 0) {
                gameManager.IncrementNumOfGeneratedSeeds();
                return true;
            }
            else {
                return false;
            }
        }
        else {
            int randomNum = Random.Range(0,5);
            if(randomNum == 0) {
                gameManager.IncrementNumOfGeneratedSeeds();
                return true;
            }
            else {
                return false;
            }            
        }
    }*/

    int firstGate = 6;
    int secondGate = 8;
    int secondChance = 4;
    int thirdChance = 10;
    // New Chance
    bool GetChanceForSeed() {
        // Ensures some seeds are generated:
        if(GetComponentInParent<PlantInfo>() != null) {
            if(GetComponentInParent<PlantInfo>().numOfSeeds < firstGate) {
                GetComponentInParent<PlantInfo>().numOfSeeds++;
                return true;
            }
            else if(GetComponentInParent<PlantInfo>().numOfSeeds < secondGate) {
                int randomNum = Random.Range(0,secondChance);
                if(randomNum == 0) {
                    GetComponentInParent<PlantInfo>().numOfSeeds++;
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                int randomNum = Random.Range(0,thirdChance);
                if(randomNum == 0) {
                    GetComponentInParent<PlantInfo>().numOfSeeds++;
                    return true;
                }
                else {
                    return false;
                }            
            }
        }

        if(GetComponentInParent<PlantInfo2>() != null) {
            if(GetComponentInParent<PlantInfo2>().numOfSeeds < firstGate) {
                GetComponentInParent<PlantInfo2>().numOfSeeds++;
                return true;
            }
            else if(GetComponentInParent<PlantInfo2>().numOfSeeds < secondGate) {
                int randomNum = Random.Range(0,secondChance);
                if(randomNum == 0) {
                    GetComponentInParent<PlantInfo2>().numOfSeeds++;
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                int randomNum = Random.Range(0,thirdChance);
                if(randomNum == 0) {
                    GetComponentInParent<PlantInfo2>().numOfSeeds++;
                    return true;
                }
                else {
                    return false;
                }            
            }
        }

        // Boss get's special treatment
        else {
            if(GetComponentInParent<PlantInfoBoss>().numOfSeeds < firstGate+2) {
                GetComponentInParent<PlantInfoBoss>().numOfSeeds++;
                return true;
            }
            else if(GetComponentInParent<PlantInfoBoss>().numOfSeeds < secondGate+2) {
                int randomNum = Random.Range(0,secondChance);
                if(randomNum == 0) {
                    GetComponentInParent<PlantInfoBoss>().numOfSeeds++;
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                int randomNum = Random.Range(0,thirdChance-5);
                if(randomNum == 0) {
                    GetComponentInParent<PlantInfoBoss>().numOfSeeds++;
                    return true;
                }
                else {
                    return false;
                }            
            }
        }
    }

    public void NewGeneration() {
        if(GetChanceForSeed()) {
            //Debug.Log(gameManager.GetNumOfGeneratedSeeds());
            // Make sure object is destroyed:
            StartCoroutine(NewGenerationDelay());
        }
    }

    IEnumerator NewGenerationDelay() {
        yield return new WaitForSecondsRealtime(0.5f);
        /*SeedTest[] seedTestList = GetComponentsInChildren<SeedTest>();
        foreach(SeedTest s in seedTestList) {
            Destroy(s.gameObject);
        }
        SeedTest2[] seedTest2List = GetComponentsInChildren<SeedTest2>();
        foreach(SeedTest2 s in seedTest2List) {
            Destroy(s.gameObject);
        }*/
        GetNextSeed();
    }

    IEnumerator SeedDelay() {
        yield return new WaitForSecondsRealtime(0.1f);
        Instantiate(seed, RandomSeedSpawn().transform.position, Quaternion.identity, this.transform);
    }

    void Start()
    { 
        gameManager = FindObjectOfType<GameManager>();
        if(GetChanceForSeed()) {
            GetNextSeed();
        }
    }
}

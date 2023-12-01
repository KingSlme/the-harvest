using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] asteroidSpawns;
    [SerializeField] GameObject asteroidObject;
    bool hasAttemptedAsteroid = false;

    GameObject[] plantsInScene;
    GameObject[] plantsInScene2;
    GameObject[] plantsInScene3;
    
    void SpawnAsteroid() {
        int randomSpawn = Random.Range(0,asteroidSpawns.Length);
        Instantiate(asteroidObject, asteroidSpawns[randomSpawn].transform.position, Quaternion.identity);
    }

    IEnumerator AttemptAsteroid() {
        hasAttemptedAsteroid = true;
        int randomNum = Random.Range(0, 5); // 20% chance
        if(randomNum == 0) {
            SpawnAsteroid();
        }
        yield return new WaitForSecondsRealtime(1f);
        hasAttemptedAsteroid = false;
    }

    public List<GameObject> GetPlantsInScene() {
        List<GameObject> listToReturn = new List<GameObject>();

        if(plantsInScene.Length > 0) {
            foreach(GameObject o in plantsInScene) {
                listToReturn.Add(o);
            }
        }
        if(plantsInScene2.Length > 0) {
            foreach(GameObject o in plantsInScene2) {
            listToReturn.Add(o);
            }
        }
        if(plantsInScene3.Length > 0) {
            foreach(GameObject o in plantsInScene3) {
            listToReturn.Add(o);
            }
        }
        return listToReturn;
    }

    void Start()
    {
        // Will Start off not active
        // GameManager will activate after textbox (if there is one)
        plantsInScene = GameObject.FindGameObjectsWithTag("Plant");
        plantsInScene2 = GameObject.FindGameObjectsWithTag("Plant2");
        plantsInScene3 = GameObject.FindGameObjectsWithTag("Boss");
    }

    void Update()
    {
        if(!hasAttemptedAsteroid && !PauseMenu.GameIsPaused) {
            StartCoroutine(AttemptAsteroid());
        }
    }
}

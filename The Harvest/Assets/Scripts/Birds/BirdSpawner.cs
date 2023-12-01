using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] birdLeftSpawns;
    [SerializeField] GameObject[] birdRightSpawns;
    [SerializeField] GameObject[] birdBombSpawnsLeft;
    [SerializeField] GameObject[] birdBombSpawnsRight;

    [SerializeField] GameObject leftSeedBird;
    [SerializeField] GameObject rightSeedBird;
    [SerializeField] GameObject leftFlowerBird;
    [SerializeField] GameObject rightFlowerBird;
    [SerializeField] GameObject leftBombBird;
    [SerializeField] GameObject rightBombBird;

    private bool hasAttemptedSeed = false;
    private bool hasAttemptedFlower = false;
    private bool hasAttemptedBomb = false;

    // For getting plants in scene
    GameObject[] plantsInScene;
    GameObject[] plantsInScene2;
    GameObject[] plantsInScene3;

    string GetRandomSpawnDirection() {
        int randomDirection = Random.Range(0,2);
        if(randomDirection == 0) {
            return "left";
        }
        else {
            return "right";
        }
    }

    GameObject GetRandomSpawnpoint(string direction) {
        if(direction == "left") {
            return birdLeftSpawns[Random.Range(0,birdLeftSpawns.Length)];
        }
        else {
            // right
            return birdRightSpawns[Random.Range(0,birdRightSpawns.Length)];
        }
    }

    GameObject GetRandomSpawnpointBomber(string direction) {
        if(direction == "left") {
            return birdBombSpawnsLeft[Random.Range(0, birdBombSpawnsLeft.Length)];
        }
        else {
            // right
            return birdBombSpawnsRight[Random.Range(0, birdBombSpawnsRight.Length)];
        }
    }

    void SpawnSeedBird() {
        if(GetRandomSpawnDirection() == "left") {
            Instantiate(leftSeedBird, GetRandomSpawnpoint("left").transform.position, Quaternion.identity);
        }
        else if(GetRandomSpawnDirection() == "right") {
            Instantiate(rightSeedBird, GetRandomSpawnpoint("right").transform.position, Quaternion.identity);
        }

    }

    void SpawnFlowerBird() {
        if(GetRandomSpawnDirection() == "left") {
            Instantiate(leftFlowerBird, GetRandomSpawnpoint("left").transform.position, Quaternion.identity);
        }
        else if(GetRandomSpawnDirection() == "right") {
            Instantiate(rightFlowerBird, GetRandomSpawnpoint("right").transform.position, Quaternion.identity);
        }
    }

    void SpawnBombBird() {
        int randomNum = Random.Range(0, 2);
        if(randomNum == 0) {
            // Left Facing Bird needs to spawn Right
            Instantiate(leftBombBird, GetRandomSpawnpointBomber("right").transform.position, Quaternion.identity);
        }
        else if(randomNum == 1) {
            // Right Facing Bird needs to spawn Left
            Instantiate(rightBombBird, GetRandomSpawnpointBomber("left").transform.position, Quaternion.identity);
        }
    }

    IEnumerator AttemptSeedBird() {
        hasAttemptedSeed = true;
        int randomNum = Random.Range(0, 10); // 10% chance
        if(randomNum == 0) {
            SpawnSeedBird();
        }
        yield return new WaitForSecondsRealtime(1f);
        hasAttemptedSeed = false;
    }

    IEnumerator AttemptFlowerBird() {
        hasAttemptedFlower = true;
        int randomNum = Random.Range(0, 100); // 1% chance
        if(randomNum == 69) {
            SpawnFlowerBird();
        }
        yield return new WaitForSecondsRealtime(1f);
        hasAttemptedFlower = false;
    }

    IEnumerator AttemptBombBird() {
        hasAttemptedBomb = true;
        int randomNum = Random.Range(0, 10); // 10% chance
        if(randomNum == 0) {
            SpawnBombBird();
        }
        yield return new WaitForSecondsRealtime(1f);
        hasAttemptedBomb = false;
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
        if(!hasAttemptedSeed && !PauseMenu.GameIsPaused) {
            StartCoroutine(AttemptSeedBird());
        }
        if(!hasAttemptedFlower && !PauseMenu.GameIsPaused) {
            StartCoroutine(AttemptFlowerBird());
        }
        if(!hasAttemptedBomb && !PauseMenu.GameIsPaused) {
            StartCoroutine(AttemptBombBird());
        }
    }
}

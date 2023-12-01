using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour
{
    [SerializeField] string moveTowards;
    [SerializeField] string type;
    private float verticalSpeed = 1f;

    BirdSpawner birdSpawner;

    float GetHorizontalSpeed() {
        if(type == "flower") {
            return 4f;
        }
        else {
            return 2f;
        }
    }

    void Move() {
        if(moveTowards == "left") {
            transform.position -= new Vector3(GetHorizontalSpeed(), 0, 0) * Time.deltaTime;
        }
        else if(moveTowards == "right") {
            transform.position += new Vector3(GetHorizontalSpeed(), 0, 0) * Time.deltaTime;
        }
        else if(moveTowards == "down") {
            // Faces Bird Towards Closest Plant
            transform.right = GetBomberClosestTarget().transform.position - transform.position;
            //transform.position -= new Vector3(0, 0, verticalSpeed) * Time.deltaTime;
            transform.position += transform.right * verticalSpeed * Time.deltaTime;
        }
    }

    // Gets Closest Plant
    GameObject GetBomberClosestTarget() {
        float xDifference = 1000000;
        // GameObject currentObject = null;
        // For Targeting Non Dead
        GameObject currentObject = birdSpawner.GetPlantsInScene()[0];
        foreach(GameObject o in birdSpawner.GetPlantsInScene()) {
            // For Targeting Non Dead
            if(o.GetComponentInChildren<PlantDeath>() != null) {
                if(!o.GetComponentInChildren<PlantDeath>().hasAlreadDied) {
                    if(Mathf.Abs(o.transform.position.x - transform.position.x) < xDifference) {
                        xDifference = Mathf.Abs(o.transform.position.x - transform.position.x);
                        currentObject = o;
                    }
                }
            }
            if(o.GetComponentInChildren<PlantDeath2>() != null) {
                if(!o.GetComponentInChildren<PlantDeath2>().hasAlreadDied) {
                    if(Mathf.Abs(o.transform.position.x - transform.position.x) < xDifference) {
                        xDifference = Mathf.Abs(o.transform.position.x - transform.position.x);
                        currentObject = o;
                    }
                }
            }
            if(o.GetComponentInChildren<PlantDeathBoss>() != null) {
                if(!o.GetComponentInChildren<PlantDeathBoss>().hasAlreadDied) {
                    if(Mathf.Abs(o.transform.position.x - transform.position.x) < xDifference) {
                    xDifference = Mathf.Abs(o.transform.position.x - transform.position.x);
                    currentObject = o;
                    }
                }
            }
            /*if(Mathf.Abs(o.transform.position.x - transform.position.x) < xDifference) {
                xDifference = Mathf.Abs(o.transform.position.x - transform.position.x);
                currentObject = o;
            }*/
        }
        return currentObject;
    }

    // Walls
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "BirdWall") {
            Destroy(gameObject);
        }
    }

    void Start() {
        // works?
        birdSpawner = FindObjectOfType<BirdSpawner>().GetComponent<BirdSpawner>();
    }

    void Update()
    {
        Move();
    }
}

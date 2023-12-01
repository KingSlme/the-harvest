using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float xSpeed = 1f;
    AsteroidSpawner asteroidSpawner;
    FireProjectile fireProjectile;

    [SerializeField] GameObject tempEffects;

    void Move() {
        transform.right = GetAsteroidClosestTarget().transform.position - transform.position;
        transform.position += transform.right * xSpeed * Time.deltaTime;
    } 

    // Gets Closest Plant
    GameObject GetAsteroidClosestTarget() {
        float xDifference = 1000000;
        GameObject currentObject = asteroidSpawner.GetPlantsInScene()[0];
        foreach(GameObject o in asteroidSpawner.GetPlantsInScene()) {
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

    /*void KillAsteroid() {
        hasntDied = false;
        bloodParticleSystem.Play();
        Singleton.instance.PlayBirdExplodeSound();
        StartCoroutine(DelayDeath());
    }*/

    void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            fireProjectile.Fire();
        }
    }

    // Death
    public void KillAst() {
        Instantiate(tempEffects, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        // For Seed Impact
        if(other.tag == "SeedProjectile") {
            Instantiate(tempEffects, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        // For Plant Collision
        if(other.tag == "Plant" /*&& hasntDied*/) {
            Instantiate(tempEffects, transform.position, Quaternion.identity);
            if(!other.gameObject.GetComponentInChildren<PlantDeath>().hasAlreadDied) {
                other.gameObject.GetComponentInChildren<PlantDeath>().KillFlower();
            }
            // KillAsteroid();
            // temp
            Destroy(gameObject);
        }
        else if(other.tag == "Plant2" /*&& hasntDied*/) {
            Instantiate(tempEffects, transform.position, Quaternion.identity);
            if(!other.gameObject.GetComponentInChildren<PlantDeath2>().hasAlreadDied) {
                other.gameObject.GetComponentInChildren<PlantDeath2>().KillFlower();
            }
            // KillAsteroid();
            // temp
            Destroy(gameObject);
        }
        else if(other.tag == "Boss" /*&& hasntDied*/) {
            Instantiate(tempEffects, transform.position, Quaternion.identity);
            if(!other.gameObject.GetComponentInChildren<PlantDeathBoss>().hasAlreadDied) {
                other.gameObject.GetComponentInChildren<PlantDeathBoss>().KillFlower();
            }
            // KillAsteroid();
            // temp
            Destroy(gameObject);
        }
    }

    void Start()
    {
        asteroidSpawner = FindObjectOfType<AsteroidSpawner>().GetComponent<AsteroidSpawner>();
        fireProjectile = FindObjectOfType<FireProjectile>().GetComponent<FireProjectile>();
    }

    void Update()
    {
        Move();
    }
}

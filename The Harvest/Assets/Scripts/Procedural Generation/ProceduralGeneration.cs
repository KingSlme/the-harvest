using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
  
    [SerializeField] string directionForGeneration;
    // Can be Down, Left, Right
    // Down_Left, Down_Right
    // Down_Left_Right
    
    //RootConfigurations rootConfigurations;
    RootConfigurations rootConfigurations;

    GameObject[] currentArray;
    float currentX;
    float currentY;

    bool stable = false;

    // For New Generation Technique (To Save Rotation of New Generations)
    [SerializeField] GameObject leftPoint;
    [SerializeField] GameObject rightPoint;
    [SerializeField] GameObject downPoint;
    // Also changing Quaternion.identity to transform.root.gameObject.transform.rotation

    IEnumerator GenerateRoot() {
        yield return new WaitForSeconds(0.1f);
        if(directionForGeneration == "down") {
            currentArray = rootConfigurations.GetArray("top");
            int randomNum = Random.Range(0, currentArray.Length);
            // Down: y-1
            // Instantiate(currentArray[randomNum], GetNextCoordinates(0, -1f), Quaternion.identity, this.transform);
            Instantiate(currentArray[randomNum], downPoint.transform.position, transform.root.gameObject.transform.rotation, this.transform);
            rootConfigurations.currentRoots++;
        }
        else if(directionForGeneration == "left") {
            currentArray = rootConfigurations.GetArray("right");
            int randomNum = Random.Range(0, currentArray.Length);
            // Left x-1
            // Instantiate(currentArray[randomNum], GetNextCoordinates(-1f, 0), Quaternion.identity, this.transform);
            Instantiate(currentArray[randomNum], leftPoint.transform.position, transform.root.gameObject.transform.rotation, this.transform);
            rootConfigurations.currentRoots++;
        }
        else if(directionForGeneration == "right") {
            currentArray = rootConfigurations.GetArray("left");
            int randomNum = Random.Range(0, currentArray.Length);
            // Right x+1
            // Instantiate(currentArray[randomNum], GetNextCoordinates(1f, 0), Quaternion.identity, this.transform);
            Instantiate(currentArray[randomNum], rightPoint.transform.position, transform.root.gameObject.transform.rotation, this.transform);
            rootConfigurations.currentRoots++;
        }
        else if(directionForGeneration == "down_left") {
            currentArray = rootConfigurations.GetArray("top");
            GameObject[] currentArray2 = rootConfigurations.GetArray("right");
            int randomNum = Random.Range(0, currentArray.Length);
            int randomNum2 = Random.Range(0, currentArray2.Length);
            //Instantiate(currentArray[randomNum], GetNextCoordinates(0, -1f), Quaternion.identity, this.transform);
            Instantiate(currentArray[randomNum], downPoint.transform.position, transform.root.gameObject.transform.rotation, this.transform);
            yield return new WaitForSeconds(0.1f);
            // Instantiate(currentArray2[randomNum2], GetNextCoordinates(-1f, 0), Quaternion.identity, this.transform);
            Instantiate(currentArray2[randomNum2], leftPoint.transform.position, transform.root.gameObject.transform.rotation, this.transform);
            rootConfigurations.currentRoots++;
            rootConfigurations.currentRoots++;
        }
        else if(directionForGeneration == "down_right") {
            currentArray = rootConfigurations.GetArray("top");
            GameObject[] currentArray2 = rootConfigurations.GetArray("left");
            int randomNum = Random.Range(0, currentArray.Length);
            int randomNum2 = Random.Range(0, currentArray2.Length);
            // Instantiate(currentArray[randomNum], GetNextCoordinates(0, -1f), Quaternion.identity, this.transform);
            Instantiate(currentArray[randomNum], downPoint.transform.position, transform.root.gameObject.transform.rotation, this.transform);
            yield return new WaitForSeconds(0.1f);
            // Instantiate(currentArray2[randomNum2], GetNextCoordinates(1f, 0), Quaternion.identity, this.transform);
            Instantiate(currentArray2[randomNum2], rightPoint.transform.position, transform.root.gameObject.transform.rotation, this.transform);
            rootConfigurations.currentRoots++;
            rootConfigurations.currentRoots++;
        }
        else if(directionForGeneration == "down_left_right") {
            currentArray = rootConfigurations.GetArray("top");
            GameObject[] currentArray2 = rootConfigurations.GetArray("right");
            GameObject[] currentArray3 = rootConfigurations.GetArray("left");
            int randomNum = Random.Range(0, currentArray.Length);
            int randomNum2 = Random.Range(0, currentArray2.Length);
            int randomNum3 = Random.Range(0, currentArray3.Length);
            // Instantiate(currentArray[randomNum], GetNextCoordinates(0, -1f), Quaternion.identity, this.transform);
            Instantiate(currentArray[randomNum], downPoint.transform.position, transform.root.gameObject.transform.rotation, this.transform);
            yield return new WaitForSeconds(0.1f);
            // Instantiate(currentArray2[randomNum2], GetNextCoordinates(-1f, 0), Quaternion.identity, this.transform);
            Instantiate(currentArray2[randomNum2], leftPoint.transform.position, transform.root.gameObject.transform.rotation, this.transform);
            yield return new WaitForSeconds(0.1f);
            // Instantiate(currentArray3[randomNum3], GetNextCoordinates(1f, 0), Quaternion.identity, this.transform);
            Instantiate(currentArray3[randomNum3], rightPoint.transform.position, transform.root.gameObject.transform.rotation, this.transform);
            rootConfigurations.currentRoots++;
            rootConfigurations.currentRoots++;
            rootConfigurations.currentRoots++;
        }
    }

    Vector3 GetNextCoordinates(float x, float y) {
        return new Vector3(currentX + x, currentY + y, 0);
    }

    // This makes sure only relatively newer roots can be destroyed 
    IEnumerator WaitForStabilization() {
        yield return new WaitForSecondsRealtime(0.05f);
        stable = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!stable && other.tag == "Root") {
            Destroy(transform.parent.gameObject);
        }
    }

    void Start()
    {
        //rootConfigurations = FindObjectOfType<RootConfigurations>();
        //rootConfigurations = parentCatalyst.GetComponent<RootConfigurations>();
        rootConfigurations = GetComponentInParent<RootConfigurations>();
        
        currentX = transform.position.x;
        currentY = transform.position.y;
        if(rootConfigurations.currentRoots < rootConfigurations.maxRoots) {
            StartCoroutine(GenerateRoot());
        }
        StartCoroutine(WaitForStabilization());
    }
}

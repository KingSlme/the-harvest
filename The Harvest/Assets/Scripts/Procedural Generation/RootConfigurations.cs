using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootConfigurations : MonoBehaviour
{
    [SerializeField] GameObject[] hasTopOpeningArray;
    [SerializeField] GameObject[] hasLeftOpeningArray;
    [SerializeField] GameObject[] hasRightOpeningArray;

    public int currentRoots = 0;
    public int maxRoots = 40;
    
    public GameObject[] GetArray(string openingType) {
        if(openingType == "top") {
            return hasTopOpeningArray;
        }
        else if(openingType == "left") {
            return hasLeftOpeningArray;
        }
        else {
            return hasRightOpeningArray;
        }
    }
}

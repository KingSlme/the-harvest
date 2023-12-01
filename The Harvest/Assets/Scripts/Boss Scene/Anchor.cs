using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    [SerializeField] GameObject anchorObject;

    void Update()
    {
        transform.position = anchorObject.transform.position;
        transform.rotation = anchorObject.transform.rotation;
    }
}

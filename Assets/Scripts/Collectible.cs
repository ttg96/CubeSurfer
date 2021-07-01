using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    void FixedUpdate()
    {
        Vector3 newRotation = new Vector3(0, 1, 0);
        transform.Rotate(newRotation, Space.Self);
    }
}

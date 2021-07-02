using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{

    [SerializeField]
    private GameObject stackCubePrefab;
    [SerializeField]
    private int cubeStackSize;
    private GameObject[] cubeStack;

    private void Start() {
        Vector3 spawnPosition = transform.position;
        cubeStack = new GameObject[cubeStackSize];
        for(int i = 0; i < cubeStackSize; i++) {
            cubeStack[i] = Instantiate(stackCubePrefab, spawnPosition, Quaternion.identity, transform);
            spawnPosition.y += 1;
        }
    }

    public int TowerCollection() {
        StartCoroutine(selfDestruct());
        return cubeStackSize;
    }

    IEnumerator selfDestruct() {
        for (int i = 0; i < cubeStackSize; i++) {
            Destroy(cubeStack[i]);
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(gameObject);
    }
}

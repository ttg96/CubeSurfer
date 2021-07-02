using System.Collections;
using UnityEngine;

public class Stack : MonoBehaviour
{

    [SerializeField]
    private GameObject stackCubePrefab;
    [SerializeField]
    private int cubeStackSize;
    private GameObject[] cubeStack;

    //Creates stack based on number provided
    private void Start() {
        Vector3 spawnPosition = transform.position;
        cubeStack = new GameObject[cubeStackSize];
        for(int i = 0; i < cubeStackSize; i++) {
            cubeStack[i] = Instantiate(stackCubePrefab, spawnPosition, Quaternion.identity, transform);
            spawnPosition.y += 1;
        }
    }

    //Sends size to player and start self destruction
    public int TowerCollection() {
        StartCoroutine(selfDestruct());
        return cubeStackSize;
    }

    //Destroys cubes one by one with a small delay
    IEnumerator selfDestruct() {
        for (int i = 0; i < cubeStackSize; i++) {
            Destroy(cubeStack[i]);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}

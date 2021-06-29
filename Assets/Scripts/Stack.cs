using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{

    [SerializeField]
    private GameObject cubePrefab;
    [SerializeField]
    private int cubeStackSize;
    private GameObject[] cubeStack;

    private void Start() {
        Vector3 spawnPosition = transform.position;
        cubeStack = new GameObject[cubeStackSize];
        for(int i = 0; i < cubeStackSize; i++) {
            cubeStack[i] = Instantiate(cubePrefab, spawnPosition, Quaternion.identity, transform);
            spawnPosition.y += 1;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        var player = collision.gameObject.transform.GetComponent<Player>();
        if(player) {
            player.AddCubes(cubeStackSize);
            StartCoroutine(selfDestruct());
        }
    }

    IEnumerator selfDestruct() {
        for (int i = 0; i < cubeStackSize; i++) {
            Destroy(cubeStack[i]);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private GameObject towerCubePrefab;
    [SerializeField]
    private int towerSize;
    private List<GameObject> tower;
    private Transform playerPawn;

    // Start is called before the first frame update
    void Start()
    {
        tower = new List<GameObject>(towerSize);

        playerPawn = transform.GetChild(0).gameObject.transform;
        for (int i = 0; i < playerPawn.childCount; i++) {
            tower.Add(playerPawn.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Stack") {
            AddCubes(collision.gameObject.transform.GetComponent<Stack>().TowerCollection());
        }
    }

    public void AddCubes(int amount) {
        for(int i = 0; i < amount; i++) {
            Vector3 newPosition = playerPawn.position;
            newPosition.y += 1;
            playerPawn.position = newPosition;
            Vector3 spawnPosition = tower[towerSize - 1].transform.position;
            spawnPosition.y += -1;
            tower.Add(Instantiate(towerCubePrefab, spawnPosition, Quaternion.identity, playerPawn));
            towerSize++;
        }
    }

    public void RemoveCubes(GameObject cube) {
        tower.Remove(cube);
        towerSize--;
    }
}

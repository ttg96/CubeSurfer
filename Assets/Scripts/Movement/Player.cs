using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject towerCubePrefab;
    [SerializeField]
    private int towerSize;
    private Manager manager;
    private List<GameObject> tower;
    private Transform playerPawn;
    private int score;
    private int multiplier;

    void Start() {
        score = 0;
        multiplier = 1;
        manager = FindObjectOfType<Manager>();
        tower = new List<GameObject>(towerSize);
        playerPawn = transform.GetChild(0).gameObject.transform;
        for (int i = 0; i < playerPawn.childCount; i++) {
            tower.Add(playerPawn.GetChild(i).gameObject);
        }
        manager.UpdateScore(score);
        manager.UpdateMultiplier(multiplier);
    }

    //Check collision with stack
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Stack") {
            AddCubes(collision.gameObject.transform.GetComponent<Stack>().TowerCollection());
        }
    }

    //Check trigger with collectible, turning triggers, bonus or finish
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Collectible") {
            score++;
            manager.PlayAudioClip(1);
            manager.UpdateScore(score);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "TurnLeftTrigger") {
            StartCoroutine(Rotate(-1));
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "TurnRightTrigger") {
            StartCoroutine(Rotate(1));
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "BonusTrigger") {
            multiplier += 3;
            manager.UpdateMultiplier(multiplier);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Finish") {
            EndLevel(true);
            Destroy(collision.gameObject);
        }
    }

    //Add cubes to player
    public void AddCubes(int amount) {
        for (int i = 0; i < amount; i++) {
            Vector3 newPosition = playerPawn.position;
            newPosition.y += 1.2f;
            playerPawn.position = newPosition;
            Vector3 spawnPosition = tower[towerSize - 1].transform.position;
            spawnPosition.y += -1;
            tower.Add(Instantiate(towerCubePrefab, spawnPosition, Quaternion.identity, playerPawn));
            towerSize++;
            manager.PlayAudioClip(0);
        }
    }

    //Remove cubes from player
    public void RemoveCubes(GameObject cube) {
        tower.Remove(cube);
        towerSize--;
        if(towerSize == 0) {
            EndLevel(false);
        }
    }

    //Send level end signal
    public void EndLevel(bool finished) {
        Time.timeScale = 0;
        score *= multiplier;
        if (multiplier > 1) finished = true;
        manager.GameOver(score, finished);
    }

    //Coroutine for smoother rotation
    IEnumerator Rotate(int direction) {
        for (int i = 0; i < 90; i++) {
            Vector3 newRotation = new Vector3(0, direction, 0);
            transform.Rotate(newRotation, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }
    }
}

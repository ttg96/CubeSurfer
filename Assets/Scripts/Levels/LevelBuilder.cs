using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{

    public Level selectedLevel;
    [SerializeField]
    private Level[] allLevels;
    [SerializeField]
    private GameObject[] levelPieces;
    [SerializeField]
    private GameObject startingPiece;
    [SerializeField]
    private GameObject endPiece;
    [SerializeField]
    private GameObject[] currentLevel;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject playerPawn;
    [SerializeField]
    private AssignCamera virtualCamera; 

    public void LevelSelect(int level) {
        selectedLevel = allLevels[level];
        LoadLevel();
        SpawnPlayer();
    }

    public void RestartLevel() {
        SpawnPlayer();
        ResetLevel();
        LoadLevel();
    }

    public void LoadLevel() {
        currentLevel = new GameObject[selectedLevel.levelLayout.Length + 1];
        GameObject previousPiece = startingPiece;
        Vector3 spawnPosition;
        for (int i = 0; i < selectedLevel.levelLayout.Length; i++) {
            GameObject nextPiece = levelPieces[selectedLevel.levelLayout[i]];
            spawnPosition = previousPiece.transform.Find("EndPoint").gameObject.transform.position;
            previousPiece = Instantiate(nextPiece, spawnPosition, Quaternion.identity);
            currentLevel[i] = previousPiece;
        }
        spawnPosition = previousPiece.transform.Find("EndPoint").gameObject.transform.position;
        currentLevel[selectedLevel.levelLayout.Length] = Instantiate(endPiece, spawnPosition, Quaternion.identity);  
    }

    private void SpawnPlayer() {
        Destroy(FindObjectOfType<Player>().gameObject);
        GameObject temp = Instantiate(playerPawn, spawnPoint.position, Quaternion.identity);
        virtualCamera.AssignCameraTarget(temp.transform);
    }

    public void ResetLevel() {
        foreach(GameObject piece in currentLevel) {
            Destroy(piece);
        }
    }

    public void NextLevel() {
        selectedLevel = allLevels[selectedLevel.nextLevel-1];
        LoadLevel();
        SpawnPlayer();
    }

    public void RandomLevel() {
        int[] random = new int[Random.Range(4, 15)];
        for(int i = 0; i < random.Length; i++) {
            random[i] = Random.Range(0, 11);
        }
        selectedLevel = new Level("random", random, 0);
        LoadLevel();
        SpawnPlayer();
    }
}

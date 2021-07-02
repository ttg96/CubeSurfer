using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{

    public Level selectedLevel;
    [SerializeField]
    private GameObject[] levelPieces;
    [SerializeField]
    private GameObject startingPiece;
    [SerializeField]
    private GameObject endPiece;

    private void Awake() {
        GameObject previousPiece = startingPiece;
        Vector3 spawnPosition;
        for (int i = 0; i < selectedLevel.levelLayout.Length; i++) {
            GameObject nextPiece = levelPieces[selectedLevel.levelLayout[i]];
            spawnPosition = previousPiece.transform.Find("EndPoint").gameObject.transform.position;
            previousPiece = Instantiate(nextPiece, spawnPosition, Quaternion.identity);
        }
        spawnPosition = previousPiece.transform.Find("EndPoint").gameObject.transform.position;
        Instantiate(endPiece, spawnPosition, Quaternion.identity);
    }
}

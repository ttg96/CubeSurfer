using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCube : MonoBehaviour
{

    private Player player;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Wall") {
            player.RemoveCubes(transform.gameObject);
            transform.parent = null;
        }
    }

}

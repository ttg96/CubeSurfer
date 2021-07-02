using UnityEngine;

public class TowerCube : MonoBehaviour
{

    private Player player;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    //Checks collision with wall and detaches from player after noyting them
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Wall") {
            player.RemoveCubes(transform.gameObject);
            transform.parent = null;
        }
    }

}

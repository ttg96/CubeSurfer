using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCube : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Stack") {
            AddCubes(collision.gameObject.transform.GetComponent<Stack>().TowerCollection());
        }
    }
}

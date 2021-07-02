using UnityEngine;
using UnityEngine.InputSystem;

public class MoveDetection : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxSideSpeed;
    private float sideSpeed;
    private float moveDirection;
    private bool isTouching;
    private InputAction touchPosition;

    private void Awake() {
        inputManager = FindObjectOfType<InputManager>();
        moveDirection = 0;
        sideSpeed = maxSideSpeed;
        isTouching = false;
    }

    private void OnEnable() {
        inputManager.OnStartTouch += Move;
        inputManager.OnEndTouch += StopMove;
    }

    private void OnDisable() {
        inputManager.OnStartTouch -= Move;
        inputManager.OnEndTouch -= StopMove;
    }

    //Enable side to side movement
    private void Move(InputAction position) {
        touchPosition = position;
        isTouching = true;
    }

    //Recenter move direction
    private void StopMove() {
        moveDirection = 0;
        isTouching = false;
    }

    /*
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Barrier") {
            sideSpeed = 0;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Barrier") {
            sideSpeed = maxSideSpeed;
        }
    }
    */

    //Player tower movement based on touch input value
    private void FixedUpdate() {
        if (isTouching) {
            moveDirection = touchPosition.ReadValue<Vector2>().x;
        }

        transform.Translate(transform.forward * speed * Time.deltaTime);
        transform.Translate(transform.right * moveDirection * sideSpeed * Time.deltaTime);
    }

}

using UnityEngine;
using UnityEngine.InputSystem;

public class MoveDetection : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float sideSpeed;
    private float moveDirection;
    private bool isTouching;
    private InputAction touchPosition;

    private void Awake() {
        moveDirection = 0;
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

    private void Move(InputAction position) {
        touchPosition = position;
        isTouching = true;
    }

    private void StopMove() {
        moveDirection = 0;
        isTouching = false;
    }

    private void Update() {
        if (isTouching) {
            moveDirection = touchPosition.ReadValue<Vector2>().x;
        }

        transform.position += transform.forward * speed * Time.deltaTime;
        transform.position += transform.right * moveDirection * sideSpeed * Time.deltaTime;
    }

}

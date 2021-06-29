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
    private Camera mainCamera;
    private Vector2 touchPoint;
    private bool isTouching;
    private InputAction touchPosition;

    private void Awake() {
        moveDirection = 0;
        mainCamera = Camera.main;
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
        touchPoint = position.ReadValue<Vector2>();
        isTouching = true;
    }

    private void StopMove(Vector2 position, float time) {
        moveDirection = 0;
        isTouching = false;
    }

    private void Update() {
        if (isTouching) {
            if (touchPoint.x > touchPosition.ReadValue<Vector2>().x) {
               moveDirection = -1;
            } else {
               moveDirection = 1;
            }
        }
        Vector3 currentPosition = transform.position;
        currentPosition.z += speed * Time.deltaTime;
        currentPosition.x += moveDirection * sideSpeed * Time.deltaTime;
        transform.position = currentPosition;
    }

}

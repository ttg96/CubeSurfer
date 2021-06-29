using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{

    private Camera mainCamera;
    private PlayerControls playerControls;

    
    #region Events
    public delegate void StartTouchEvent(InputAction position);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 pos, float time);
    public event EndTouchEvent OnEndTouch;
    #endregion
    

    private void Awake() {
        playerControls = new PlayerControls();
        mainCamera = Camera.main;
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    void Start() {
        playerControls.TouchControls.TouchPress.started += ctx => StartTouch(ctx);
        playerControls.TouchControls.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context) {
        Debug.Log("start");
        if (OnStartTouch != null) OnStartTouch(playerControls.TouchControls.TouchPosition);
    }

    private void EndTouch(InputAction.CallbackContext context) {
        Debug.Log("end");
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, playerControls.TouchControls.TouchPosition.ReadValue<Vector2>()),
            (float)context.time);
    }
}

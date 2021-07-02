using UnityEngine;
using Cinemachine;

public class AssignCamera : MonoBehaviour
{

    private CinemachineVirtualCamera virtualCamera;

    private void Awake() {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    //Assigns camera to the player object
    public void AssignCameraTarget(Transform followTarget) {
        virtualCamera.m_Follow = followTarget;
        virtualCamera.m_LookAt = followTarget;
    }
}

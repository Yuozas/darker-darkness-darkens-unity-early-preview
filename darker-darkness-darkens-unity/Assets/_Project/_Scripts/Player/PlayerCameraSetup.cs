using Cinemachine;
using UnityEngine;

public class PlayerCameraSetup : MonoBehaviour
{
    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;

    private void Awake()
    {
        var virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        virtualCamera.Follow = CinemachineCameraTarget.transform;
        Debug.Log("Cameras setup.");
    }
}

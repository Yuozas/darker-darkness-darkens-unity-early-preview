using System;
using Mirror;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerSetup : NetworkBehaviour
{
  [Header("Cinemachine")]
  [SerializeField] private GameObject _cinemachineCameraTarget;
  [SerializeField] private GameObject _playerCameraPrefab;
  
  [Header("Player input")]
  [SerializeField] private PlayerInput _playerInput;
  
  private GameObject _playerCameraInstance;
  
  public override void OnStartLocalPlayer()
  {
    CmdSpawnPlayerCamera();
    CmdSetupInput();
  }
  
  [Command]
  private void CmdSpawnPlayerCamera()
  {
    _playerCameraInstance = Instantiate(_playerCameraPrefab);
    NetworkServer.Spawn(_playerCameraInstance, connectionToClient);
    RpcSetupCamera(_playerCameraInstance);
  }

  [Command]
  private void CmdSetupInput()
  {
    _playerInput.camera = _playerCameraInstance.GetComponent<Camera>();
  }
  
  [ClientRpc]
  private void RpcSetupCamera(GameObject cameraGameObject)
  {
    // This check ensures that only the local player sets up the camera.
    if (!isLocalPlayer)
      return;

    // Now on the client, set the camera's follow target.
    var virtualCamera = cameraGameObject.GetComponentInChildren<CinemachineVirtualCamera>();
    if (virtualCamera == null || _cinemachineCameraTarget == null)
      throw new Exception("Failed to retrieve camera from child.");
    
    virtualCamera.Follow = _cinemachineCameraTarget.transform;
    Debug.Log("Camera setup for local player.");
  }
}
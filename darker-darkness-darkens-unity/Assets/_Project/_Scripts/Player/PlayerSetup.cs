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
  [SerializeField] private GameObject _playerInputPrefab;
  
  public override void OnStartLocalPlayer()
  {
    CmdSpawnPlayerCamera();
  }
  
  [Command]
  private void CmdSpawnPlayerCamera()
  {
    RpcSetup();
  }
  
  [ClientRpc]
  private void RpcSetup()
  {
    // This check ensures that only the local player sets up the camera.
    if (!isLocalPlayer)
      return;
    
    var playerCameraInstance = Instantiate(_playerCameraPrefab);
    // NetworkServer.Spawn(playerCameraInstance, connectionToClient);
    
    // Now on the client, set the camera's follow target.
    var virtualCamera = playerCameraInstance.GetComponentInChildren<CinemachineVirtualCamera>();
    if (virtualCamera == null || _cinemachineCameraTarget == null)
      throw new Exception("Failed to retrieve camera from child.");
    
    virtualCamera.Follow = _cinemachineCameraTarget.transform;
    Debug.Log("Camera setup for local player.");
    var playerInputInstance = Instantiate(_playerInputPrefab);
    
    var playerInput = playerInputInstance.GetComponent<PlayerInput>();
    var inputs = playerInputInstance.GetComponent<Inputs>();
    var firstPersonNetworkController = GetComponent<FirstPersonNetworkController>();
    firstPersonNetworkController.SetupPlayerInput(playerInput, inputs);
    playerInput.camera = playerCameraInstance.GetComponentInChildren<Camera>();
    
    Debug.Log("Input setup for local player.");
  }
}
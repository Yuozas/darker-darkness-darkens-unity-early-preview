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
    CmdInstantiatePlayerPrefabs();
  }
  
  [Command]
  private void CmdInstantiatePlayerPrefabs()
  {
    RpcSetup();
  }
  
  [ClientRpc]
  private void RpcSetup()
  {
    if (!isLocalPlayer)
      return;
    
    var playerCameraInstance = Instantiate(_playerCameraPrefab);
    Debug.Log("Camera instantiated");
    
    var virtualCamera = playerCameraInstance.GetComponentInChildren<CinemachineVirtualCamera>();
    virtualCamera.Follow = _cinemachineCameraTarget.transform;
    Debug.Log("Camera setup for local player.");
    
    var playerInputInstance = Instantiate(_playerInputPrefab);
    Debug.Log("Player Input instantiated.");
    
    var playerInput = playerInputInstance.GetComponent<PlayerInput>();
    var inputs = playerInputInstance.GetComponent<Inputs>();
    var firstPersonNetworkController = GetComponent<FirstPersonNetworkController>();
    firstPersonNetworkController.SetupPlayerInput(playerInput, inputs);
    playerInput.camera = playerCameraInstance.GetComponentInChildren<Camera>();
    
    Debug.Log("Input setup for local player.");
  }
}
using Cinemachine;
using Euphelia.Player.Network;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Euphelia.Player
{
	public class PlayerSetup : NetworkBehaviour
	{
		[Header("Cinemachine")]
		[SerializeField]
		private GameObject _cinemachineCameraTarget;

		[SerializeField] private GameObject _playerCameraPrefab;

		[Header("Player input")]
		[SerializeField]
		private GameObject _playerInputPrefab;

		[Header("Voice chat")][SerializeField] private GameObject _voiceChatPrefab;

		public override void OnStartLocalPlayer() => CmdInstantiatePlayerPrefabs();

		[Command]
		private void CmdInstantiatePlayerPrefabs() => RpcSetup();

		[ClientRpc]
		private void RpcSetup()
		{
			if (!isLocalPlayer)
				return;

			var playerCameraInstance = Instantiate(_playerCameraPrefab);
			Debug.Log("Camera instantiated.");

			var voiceChatInstance = Instantiate(_voiceChatPrefab);
			Debug.Log("Voice instantiated.");
			NetworkServer.Spawn(voiceChatInstance, connectionToClient);

			var virtualCamera = playerCameraInstance.GetComponentInChildren<CinemachineVirtualCamera>();
			virtualCamera.Follow = _cinemachineCameraTarget.transform;
			Debug.Log("Camera setup for local player.");

			var playerInputInstance = Instantiate(_playerInputPrefab);
			Debug.Log("Player Input instantiated.");

			var playerInput                  = playerInputInstance.GetComponent<PlayerInput>();
			var inputs                       = playerInputInstance.GetComponent<Inputs>();
			var firstPersonNetworkController = GetComponent<FirstPersonNetworkController>();
			firstPersonNetworkController.SetupPlayerInput(playerInput, inputs);
			playerInput.camera = playerCameraInstance.GetComponentInChildren<Camera>();

			Debug.Log("Input setup for local player.");
		}
	}
}
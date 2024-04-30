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

		[Header("Voice handler")]
		[SerializeField]
		private GameObject _playerVoiceHandlerPrefab;

		private GameObject _playerCameraInstance;
		private GameObject _playerInputInstance;

		private void OnDestroy()
		{
			Destroy(_playerCameraInstance);
			Destroy(_playerInputInstance);
		}

		public override void OnStartLocalPlayer()
		{
			if (!isLocalPlayer)
				return;

			Debug.Log($"Started setup for netId:{netId}");

			_playerCameraInstance = Instantiate(_playerCameraPrefab);
			Debug.Log("Camera instantiated.");

			SpawnPlayerControlledPrefabs();
			Debug.Log("Voice instantiated.");

			var virtualCamera = _playerCameraInstance.GetComponentInChildren<CinemachineVirtualCamera>();
			virtualCamera.Follow = _cinemachineCameraTarget.transform;
			Debug.Log("Camera setup for local player.");

			_playerInputInstance = Instantiate(_playerInputPrefab);
			Debug.Log("Player Input instantiated.");

			var playerInput                  = _playerInputInstance.GetComponent<PlayerInput>();
			var inputs                       = _playerInputInstance.GetComponent<Inputs>();
			var firstPersonNetworkController = GetComponent<FirstPersonNetworkController>();
			firstPersonNetworkController.SetupPlayerInput(playerInput, inputs);
			playerInput.camera = _playerCameraInstance.GetComponentInChildren<Camera>();

			Debug.Log("Input setup for local player.");
		}

		[Command]
		private void SpawnPlayerControlledPrefabs()
		{
			var voiceManager = Instantiate(_playerVoiceHandlerPrefab);
			NetworkServer.Spawn(voiceManager, connectionToClient);
		}
	}
}
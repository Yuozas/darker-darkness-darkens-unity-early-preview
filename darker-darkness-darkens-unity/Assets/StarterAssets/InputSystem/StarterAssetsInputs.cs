using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	using System.Collections;
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		
		private PlayerInput _playerInput;

		private IEnumerator Start() {
			yield return new WaitForSeconds(5);
			_playerInput = GetComponentInChildren<PlayerInput>();
		}

		private void Update() {
			if (_playerInput == null)
				return;
			
			var moveAction = _playerInput.currentActionMap.FindAction("Move", true);
			
			var lookAction = _playerInput.currentActionMap.FindAction("Look", true);
			
			var jumpAction = _playerInput.currentActionMap.FindAction("Jump", true);
			
			var sprintAction = _playerInput.currentActionMap.FindAction("Sprint", true);
			
			var move = moveAction.ReadValue<Vector2>();
			MoveInput(move);

			if (cursorInputForLook) {
				var look = lookAction.ReadValue<Vector2>();
				LookInput(look);
			}

			var jump = jumpAction.triggered;
				JumpInput(jump);

				var sprint = sprintAction.triggered;
			SprintInput(jump);
		}
// 		
// 		public void OnMove(InputAction.CallbackContext value)
// 		{
// 			MoveInput(value.ReadValue<Vector2>());
// 		}
//
// 		public void OnLook(InputAction.CallbackContext value)
// 		{
// 			if(cursorInputForLook)
// 			{
// 				LookInput(value.Get<Vector2>());
// 			}
// 		}
//
// 		public void OnJump(InputAction.CallbackContext value)
// 		{
// 			JumpInput(value.ReadValueAsButton());
// 		}
//
// 		public void OnSprint(InputAction.CallbackContext value)
// 		{
// 			SprintInput(value.isPressed);
// 		}
// #endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}
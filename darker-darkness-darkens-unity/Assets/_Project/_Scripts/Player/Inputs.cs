using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Euphelia.Player
{
	public class Inputs : MonoBehaviour
	{
		[FormerlySerializedAs("move")]
		[Header("Character Input Values")]
		public Vector2 Move;

		[FormerlySerializedAs("look")]   public Vector2 Look;
		[FormerlySerializedAs("jump")]   public bool    Jump;
		[FormerlySerializedAs("sprint")] public bool    Sprint;

		[FormerlySerializedAs("analogMovement")]
		[Header("Movement Settings")]
		public bool AnalogMovement;

		[FormerlySerializedAs("cursorLocked")]
		[Header("Mouse Cursor Settings")]
		public bool CursorLocked = true;

		[FormerlySerializedAs("cursorInputForLook")]
		public bool CursorInputForLook = true;

		private void OnApplicationFocus(bool hasFocus) => SetCursorState(CursorLocked);

		// ReSharper disable once MemberCanBePrivate.Global
		public void MoveInput(Vector2 newMoveDirection) => Move = newMoveDirection;

		// ReSharper disable once MemberCanBePrivate.Global
		public void LookInput(Vector2 newLookDirection) => Look = newLookDirection;

		// ReSharper disable once MemberCanBePrivate.Global
		public void JumpInput(bool newJumpState) => Jump = newJumpState;

		// ReSharper disable once MemberCanBePrivate.Global
		public void SprintInput(bool newSprintState) => Sprint = newSprintState;

		private static void SetCursorState(bool newState) => Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;

	#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value) => MoveInput(value.Get<Vector2>());

		public void OnLook(InputValue value)
		{
			if (CursorInputForLook)
				LookInput(value.Get<Vector2>());
		}

		public void OnJump(InputValue value) => JumpInput(value.isPressed);

		public void OnSprint(InputValue value) => SprintInput(value.isPressed);
	#endif
	}
}
using UnityEngine;
using UnityEngine.Serialization;

namespace Euphelia.Player.Network
{
	public class BasicNetworkRigidBodyPush : MonoBehaviour
	{
		[FormerlySerializedAs("pushLayers")] public LayerMask PushLayers;
		[FormerlySerializedAs("canPush")]    public bool      CanPush;

		[FormerlySerializedAs("strength")]
		[Range(0.5f, 5f)]
		public float Strength = 1.1f;

		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			if (CanPush)
				PushRigidBodies(hit);
		}

		private void PushRigidBodies(ControllerColliderHit hit)
		{
			// https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

			// make sure we hit a non-kinematic rigid body
			var body = hit.collider.attachedRigidbody;
			if (body == null || body.isKinematic)
				return;

			// make sure we only push desired layer(s)
			var bodyLayerMask = 1 << body.gameObject.layer;
			if ((bodyLayerMask & PushLayers.value) == 0)
				return;

			// We don't want to push objects below us
			if (hit.moveDirection.y < -0.3f)
				return;

			// Calculate push direction from move direction, horizontal motion only
			var pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

			// Apply the push and take strength into account
			body.AddForce(pushDir * Strength, ForceMode.Impulse);
		}
	}
}
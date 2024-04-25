using UnityEngine;

namespace Euphelia.Player
{
	public class PlayerSpawner : MonoBehaviour
	{
		public Vector3 GetSpawnLocation() => transform.position;
	}
}
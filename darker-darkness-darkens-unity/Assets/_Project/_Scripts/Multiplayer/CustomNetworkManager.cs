using System.Linq;
using Euphelia.Player;
using Mirror;
using Unity.Mathematics;

namespace Euphelia.Multiplayer
{
	public class CustomNetworkManager : NetworkManager
	{
		public override void OnServerAddPlayer(NetworkConnectionToClient conn)
		{
			var start  = FindFirstObjectByType<PlayerSpawner>().GetSpawnLocation();
			var player = Instantiate(playerPrefab, start, quaternion.identity);
			NetworkServer.AddPlayerForConnection(conn, player);
		}

		public override void OnServerDisconnect(NetworkConnectionToClient conn)
		{
			// Despawn all objects owned by the disconnected player
			var playerOwnedIdentities = conn.owned.Where(n => !n.isLocalPlayer).ToArray();
			foreach (var identity in playerOwnedIdentities)
				NetworkServer.Destroy(identity.gameObject);

			base.OnServerDisconnect(conn);
		}
	}
}
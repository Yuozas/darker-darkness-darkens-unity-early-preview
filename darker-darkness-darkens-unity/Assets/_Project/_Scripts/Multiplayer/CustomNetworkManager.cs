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
	}
}
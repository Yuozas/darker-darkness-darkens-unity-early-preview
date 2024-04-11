using SwiftLocator.Services.ServiceLocatorServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P2PUserInterface : MonoBehaviour
{
    private P2PNetworkManager _p2pNetworkManager;

    private void Awake()
    {
        _p2pNetworkManager = ServiceLocator.GetSingleton<P2PNetworkManager>();
    }

    private async void OnGUI()
    {
        if (GUILayout.Button("Host Game"))
        {
            var succesfullyHosted = await _p2pNetworkManager.StartHost();
            if (succesfullyHosted)
            {
                await LoadGameScene();
                return;
            }
            GUILayout.Label("Failed to host game...");
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Join Game at IP:");
        string hostIP = GUILayout.TextField("127.0.0.1"); // Default to localhost for testing

        if (GUILayout.Button("Join"))
        {
            var succesfullyJoined = await _p2pNetworkManager.StartClient(hostIP);
            if(succesfullyJoined)
            {
                await LoadGameScene();
                return;
            }

            GUILayout.Label($"Failed to game at {hostIP}...");
        }

        GUILayout.EndHorizontal();
    }

    // Temporary.
    private async Task LoadGameScene()
    {
        await SceneManager.LoadSceneAsync(2);
    }
}

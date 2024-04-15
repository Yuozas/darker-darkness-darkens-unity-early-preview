using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(PlayerInput))]
public class PlayerNetworkInputController : NetworkBehaviour
{
    private PlayerInput _playerInput;


    public override void Spawned()
    {
        base.Spawned();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.camera = Camera.main;
        _playerInput.uiInputModule = FindFirstObjectByType<InputSystemUIInputModule>();
        Debug.Log("Player input setup.");
    }
}

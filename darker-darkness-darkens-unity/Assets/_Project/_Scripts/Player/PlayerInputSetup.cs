using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputSetup : MonoBehaviour
{
    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerInput.camera = Camera.main;
        playerInput.uiInputModule = FindFirstObjectByType<InputSystemUIInputModule>();
        Debug.Log("Player input setup.");
    }
}

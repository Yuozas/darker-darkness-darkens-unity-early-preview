using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    //private CharacterController controller;
    //public float speed = 5.0f;

    //private NetworkVariable<Vector2> moveDirection = new NetworkVariable<Vector2>();

    //private void Awake()
    //{
    //    controller = GetComponent<CharacterController>();
    //}

    //public override void FixedUpdateNetwork()
    //{
    //    if (Object.HasInputAuthority)
    //    {
    //        moveDirection.Value = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    //    }

    //    if (moveDirection.Value.sqrMagnitude > 0)
    //    {
    //        Vector3 move = new Vector3(moveDirection.Value.x, 0, moveDirection.Value.y);
    //        controller.Move(move * speed * Time.fixedDeltaTime);
    //    }
    //}
}

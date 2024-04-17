using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 GetSpawnLocation()
    {
        return transform.position;
    }
}
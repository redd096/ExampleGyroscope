using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] Player playerPrefab = default;
    [SerializeField] Transform playerPosition = default;

    void Start()
    {
        //instantiate prefab at position
        Instantiate(playerPrefab, playerPosition.position, Quaternion.identity);
    }
}

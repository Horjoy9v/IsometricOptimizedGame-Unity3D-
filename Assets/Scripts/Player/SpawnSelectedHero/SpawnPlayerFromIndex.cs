using UnityEngine;
public class SpawnPlayerFromIndex : MonoBehaviour
{
    public GameObject[] Players;
    private Transform SpawnPosition;

    private void Awake()
    {
        SpawnPosition = GameObject.FindWithTag("PlayerSpawnPosition").GetComponent<Transform>();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Vector3 spawnPosition = SpawnPosition.position;
        Quaternion spawnRotation = SpawnPosition.rotation;

        GameObject player = Instantiate(Players[SetPlayerIndex.CharacterIndex - 1], spawnPosition, spawnRotation);
        enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Transform playerTransform;
    private float deleteDistance = 20f; // Distance behind player at which platform will be destroyed

    [SerializeField]
    private GameObject[] collectiblePrefabs; // Array of collectible prefabs

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Spawn a random collectible on top of the platform
        SpawnCollectible();
    }

    void Update()
    {
        float playerX = playerTransform.position.x;
        float platformX = transform.position.x;

        // Check if the platform is behind the player by the delete distance
        if (platformX < playerX - deleteDistance)
        {
            Destroy(gameObject);
        }
    }

    void SpawnCollectible()
    {
        if (collectiblePrefabs.Length > 0)
        {
            // Select a random collectible prefab
            int collectibleIndex = Random.Range(0, collectiblePrefabs.Length);
            GameObject collectiblePrefab = collectiblePrefabs[collectibleIndex];

            // Instantiate the collectible on top of the platform
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z); // Adjust Y offset as needed
            Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity, transform);
        }
    }
}

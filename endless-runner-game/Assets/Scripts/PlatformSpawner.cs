using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private float initialSpawnOffset = 36f; // Distance for the first platform from player
    [SerializeField] private float maxHeight = 1.3f;
    [SerializeField] private float minHeight = -1.3f;

    private Transform playerTransform;
    private float spawnDistance = 12f; // Distance between each platform
    private float lastPlatformX; // X position of the last spawned platform
    private float lastPlatformY; // Y position of the last spawned platform

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Initialize the first platform spawn position
        lastPlatformX = playerTransform.position.x + initialSpawnOffset;

        // Spawn the first platform
        SpawnPlatform(lastPlatformX);
    }

    void Update()
    {
        // Check if it's time to spawn a new platform based on the last platform's position
        if (playerTransform.position.x + initialSpawnOffset > lastPlatformX)
        {
            // Update the position for the next platform
            lastPlatformX += spawnDistance;

            // Spawn a new platform
            SpawnPlatform(lastPlatformX);
        }
    }

    void SpawnPlatform(float xPos)
    {
        float newY = lastPlatformY + Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(xPos, newY, 0);
        int prefabIndex = Random.Range(0, platformPrefabs.Length);
        GameObject newPlatform = Instantiate(platformPrefabs[prefabIndex], spawnPosition, Quaternion.identity);
        newPlatform.transform.parent = transform;
    }

}

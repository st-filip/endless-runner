using UnityEngine;

public class Platform : MonoBehaviour
{
    private Transform playerTransform;
    private float deleteDistance = 20f; // Distance behind player at which platform will be destroyed

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
}


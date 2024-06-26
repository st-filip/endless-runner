using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("Fruit"))
            {
                AudioManager.Instance.PlaySound("Point");
            }
            else
            {
                AudioManager.Instance.PlaySound("Lose");
            }
            

            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private Transform playerSpawn;
    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = playerSpawn.position;
        }
    }
}

using System;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float speed;
    public Transform[] waypoints;
    public SpriteRenderer spriteRenderer;
    public int damage;
    
    
    private Transform target;
    private int destinationPointIndex = 0;

    
    
    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        // Normalized force la magnitude du vector à 1
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Si l'ennemie et quasiment arriver à sa desdtination
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            // Quand on arrice au dernier point, le modulo rend 0 donc on repart au premier point
            destinationPointIndex = (destinationPointIndex + 1) % waypoints.Length;
            target = waypoints[destinationPointIndex];
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerHealth _playerHealth = other.transform.GetComponent<PlayerHealth>();
            _playerHealth.TakeDamage(damage);
        }
    }
}

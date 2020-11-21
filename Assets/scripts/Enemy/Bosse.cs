using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosse : MonoBehaviour
{
    [SerializeField] private int damage;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerHealth _playerHealth = other.transform.GetComponent<PlayerHealth>();
            _playerHealth.TakeDamage(damage);
        }
    }
}

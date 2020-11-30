using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointChanger : MonoBehaviour
{
    public Checkpoint checkpoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkpoint.replaceSpawn();
        }
    }
}

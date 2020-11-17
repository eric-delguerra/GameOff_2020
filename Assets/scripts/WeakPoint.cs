using System;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public GameObject ojectToDestroy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(ojectToDestroy);
        }
    }
}

using System;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    public GameObject destination;
    private Transform destinationPos, endPos;
    private bool canMooveThrow;
    private GameObject player;
    private SpriteRenderer edF;
    public bool canGoBack;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        destinationPos = destination.transform;
        edF = GameObject.Find("F").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canMooveThrow && Input.GetKeyDown(KeyCode.F) && canGoBack)
        {
            player.transform.position = destinationPos.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canGoBack)
        {
            canMooveThrow = true;
            edF.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canGoBack)
        {
            canMooveThrow = false;
            edF.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
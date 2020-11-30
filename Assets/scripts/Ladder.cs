using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool isInRange;

    private MovePlayer playerMovement;

    public BoxCollider2D collider;

    private SpriteRenderer edF;

    // Start is called before the first frame update
    void Awake()
    {
        edF = GameObject.Find("F").GetComponent<SpriteRenderer>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isClimbing && Input.GetKeyDown(KeyCode.F))
        {
            playerMovement.isClimbing = false;
            collider.isTrigger = false;
            playerMovement.animator.SetBool("isClimbing", false);
            playerMovement.sp.flipY = false;
            return;
        }

        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            var transform1 = playerMovement.transform;
            transform1.position = new Vector2(transform.position.x,
                transform1.position.y);
            playerMovement.animator.SetBool("isClimbing", true);
            playerMovement.isClimbing = true;
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            edF.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement.sp.flipY = false;
            playerMovement.animator.SetBool("isClimbing", false);
            isInRange = false;
            playerMovement.isClimbing = false;
            playerMovement.rb.AddForce(new Vector2(0, 0));
            collider.isTrigger = false;
            edF.color = new Color(1f, 1f, 1f, 0f);
            ;
        }
    }
}
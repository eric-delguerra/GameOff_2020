using System;
using System.Collections;
using UnityEngine;

public class HidePlayer : MonoBehaviour
{
    private SpriteRenderer EdSpriteRenderer;
    public Animator animator;

    private void Start()
    {
        EdSpriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("CanHide"))
        {
            EdSpriteRenderer.color = new Color(.3f, .3f, .3f, 1f);
            animator.SetBool("PlayerIsHide", true);
            StartCoroutine(WaitEndAnimation());
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("CanHide"))
        {
            EdSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);  
            animator.SetBool("PlayerIsHide", true);
            StartCoroutine(WaitEndAnimation());
        }
    }
    
    IEnumerator WaitEndAnimation()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        animator.SetBool("PlayerIsHide", false);

    }
}

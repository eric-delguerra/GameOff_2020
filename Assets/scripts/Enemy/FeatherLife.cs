using System.Collections;
using UnityEngine;

public class FeatherLife : MonoBehaviour
{
    private Bosse bossScript;
    private bool hasEnter = false;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ground") && !hasEnter)
        {
            hasEnter = true;
            bossScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<Bosse>();
            bossScript.setBossAttack(gameObject.transform);
            StartCoroutine(waitBeforeDestroy());
        }
    }

    IEnumerator waitBeforeDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}

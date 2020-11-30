using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosse : MonoBehaviour
{
    [SerializeField] private int damage;
    private Transform pointA;
    public Transform pointB;
    public float speed = 20f;
    public Transform basDeLaPatte;
    [SerializeField] private bool touchTheGround = false;
    private Transform b;

    private void Awake()
    {
        var starpoint = Instantiate(new GameObject("startPoint"), transform.position, Quaternion.identity);
        pointA = starpoint.transform;
    }


    IEnumerator move()
    {
        while (!touchTheGround)
        {
            Vector3 dir = pointB.position - basDeLaPatte.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            yield return new WaitForEndOfFrame();
            if (Vector3.Distance(transform.position, pointB.position) < 2f)
            {
                touchTheGround = true;
            }
        }

        if (touchTheGround)
        {
            StartCoroutine(moveUp());
        }
    }

    IEnumerator moveUp()
    {
        yield return new WaitForSeconds(2);

        Debug.Log("transform.position" + transform.position);
        Debug.Log("pointA.position" + pointA.position);
        while (transform.position != pointA.position)
        {
            Debug.Log("Remonte ?");
            Vector3 direc = pointA.position - transform.position;
            transform.Translate(direc.normalized * speed * Time.deltaTime, Space.World);
            yield return new WaitForEndOfFrame();
            if (Vector3.Distance(transform.position, pointA.position) < 2f)
            {
                Destroy(GameObject.Find("startPoint"));
                Destroy(GameObject.Find("endPoint"));
                Destroy(gameObject);
            }
        }
    }

    public void setBossAttack(Transform b)
    {
        var endpoint = Instantiate(new GameObject("endPoint"), b.position, Quaternion.identity);
        pointB = endpoint.transform;
        StartCoroutine(move());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerHealth _playerHealth = other.transform.GetComponent<PlayerHealth>();
            _playerHealth.TakeDamage(damage);
        }
    }
}
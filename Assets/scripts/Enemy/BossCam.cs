using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCam : MonoBehaviour
{
    public GameObject wall1, wall2;
    private Camera cam;
    private GameObject bossCam;
    [SerializeField] private FeatherSpawn _spawn;
    void Start()
    {
        cam = Camera.main;
        bossCam = GameObject.FindGameObjectWithTag("BossCamera");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _spawn.setTheCountdown(5);
            bossCam.SetActive(true);
            cam.GetComponent<CameraFollow>().enabled = false;
            wall1.GetComponent<BoxCollider2D>().isTrigger = false;
            wall2.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    public void unactiveCam()
    {
        bossCam.SetActive(false);
        cam.GetComponent<CameraFollow>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

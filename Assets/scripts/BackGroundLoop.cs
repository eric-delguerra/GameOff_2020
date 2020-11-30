using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenBounds;

    private void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds =
            mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in levels)
        {
            loadChildObject(obj);
        }
    }

    void loadChildObject(GameObject obj)
    {
        float objWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i < childNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
        
    }
}

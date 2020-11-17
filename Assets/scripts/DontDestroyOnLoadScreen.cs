using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadScreen : MonoBehaviour
{
    public GameObject[] objetcs;
    private void Awake()
    {
        foreach (var objetc in objetcs)
        {
            DontDestroyOnLoad(objetc);
        }
    }
}

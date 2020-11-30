using System;
using System.Collections;
using UnityEngine;

public class FadeMusic : MonoBehaviour
{
    public AudioSource music;

    public bool end = false;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(fadeIn());
        if (end)
        {
            music.time = 25f;
        }
    }

    IEnumerator fadeIn()
    {
        while (Math.Abs(music.volume) < 0.5f)
        {
            yield return new WaitForSeconds(0.4f);
            music.volume += 0.05f;
        }
    }
    
  
}

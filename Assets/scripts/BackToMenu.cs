using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private bool canEscape = false;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(backToMenu());
    }
    
    IEnumerator backToMenu()
    {        
        yield return new WaitForSeconds(40);
        canEscape = true;
        yield return new WaitForSeconds(80);
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        if (canEscape && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)))
        {
            SceneManager.LoadScene(0);
        }
    }
}

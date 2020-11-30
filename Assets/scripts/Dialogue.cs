using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI text;

    private String textToDisplay = "\n\nI want more. \n\nI need to jump from the top. \n\nI just want to fly.";
    private GameObject music;

    private void Start()
    {
        music = GameObject.FindGameObjectWithTag("GameMusic");
        text.text = "";
        StartCoroutine(waitSomeTimeBeforeDisplay());
    }

    private void displayText()
    {
        StartCoroutine(typeSentence(textToDisplay));
    }

    IEnumerator waitSomeTimeBeforeDisplay()
    {
        yield return new WaitForSeconds(12);
        displayText();
        yield return new WaitForSeconds(10);
        StartCoroutine(fadeOut(music.GetComponent<AudioSource>()));
        yield return new WaitForSeconds(4.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator typeSentence(String sentence)
    {
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    IEnumerator fadeOut(AudioSource _music)
    {
        while (Math.Abs(_music.volume) > 0)
        {
            yield return new WaitForSeconds(0.4f);
            _music.volume -= 0.05f;
        }

        if (_music.volume == 0)
        {
            Destroy(music);
        }
    }
}
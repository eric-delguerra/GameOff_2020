using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherSpawn : MonoBehaviour
{
    //Array of objects to spawn (note I've removed the private goods variable)
    public GameObject feather;
    public GameObject boss;
    private Bosse bossScript;
    public Transform ed;
    private int nbOfTry = 0;
    public Camera _bossCam;

    //Time it takes to spawn theGoodies
    [Space(3)] public float waitingForNextSpawn = 10;
    [SerializeField] private float theCountdown = 600;
    private float theCountdownFor2Wave = 10;

    // the range of X
    [Header("X Spawn Range")] public float xMin;
    public float xMax;

    // the range of y
    [Header("Y Spawn Range")] public float yMin;
    public float yMax;


    void Start()
    {
        var position = transform.position;
        xMin = position.x - 6;
        yMin = position.y - 1;
        xMax = position.x + 6;
        yMax = position.y + 1;
    }

    public void Update()
    {
        // timer to spawn the next goodie Object
        theCountdown -= Time.deltaTime;
        theCountdownFor2Wave -= Time.deltaTime;
        if (nbOfTry == 5)
        {
            StartCoroutine(waitBeforeDestroy());
            
            return;
        }
        if (theCountdown <= 0 && nbOfTry != 5)
        {
            SpawnGoodies();
            theCountdown = waitingForNextSpawn;
        }
    }


    void SpawnGoodies()
    {
        Vector2 pos = new Vector2(Random.Range(ed.transform.position.x - 1, ed.transform.position.x + 1),
            Random.Range(yMin, yMax));
        Vector2 bossPos = new Vector2(pos.x, pos.y + 5);
        spawn(bossPos, pos);
    }

    void spawn(Vector2 bossPos, Vector2 pos)
    {
        Instantiate(feather, pos, Quaternion.Euler(0f, 0f, Random.Range(0, 180)));
        Instantiate(feather, pos, Quaternion.Euler(0f, 0f, Random.Range(0, 180)));
        Instantiate(feather, pos, Quaternion.Euler(0f, 0f, Random.Range(0, 180)));
        var random = Random.Range(0, 100);
        if (random <= 50)
        {
            var bossInstance = Instantiate(boss, bossPos, Quaternion.identity);
            bossInstance.GetComponent<Bosse>();
        }
        else
        {
            var bossInstance = Instantiate(boss, bossPos, Quaternion.Euler(0f, -180f, 0f));
            bossInstance.GetComponent<Bosse>();
        }

        nbOfTry++;
    }

    public void setTheCountdown(float sec)
    {
        theCountdown = sec;
    }

    IEnumerator waitBeforeDestroy()
    {
        yield return new WaitForSeconds(5f);
        _bossCam.gameObject.SetActive(false);
        var cam = Camera.main.GetComponent<CameraFollow>();
        cam.enabled = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(GameObject.Find("Boss"));
        Destroy(this);
    }
}
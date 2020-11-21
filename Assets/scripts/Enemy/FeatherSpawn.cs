using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherSpawn : MonoBehaviour
{
    //Array of objects to spawn (note I've removed the private goods variable)
    public GameObject feather;
  
    //Time it takes to spawn theGoodies
    [Space(3)]
    public float waitingForNextSpawn = 10;
    public float theCountdown = 10;
  
    // the range of X
    [Header ("X Spawn Range")]
    public float xMin;
    public float xMax;
  
    // the range of y
    [Header ("Y Spawn Range")]
    public float yMin;
    public float yMax;
  
  
    void Start()
    {
        xMin = transform.position.x - 6;
        yMin = transform.position.y - 1;
        xMax = transform.position.x + 6;
        yMax = transform.position.y + 1;
    }
  
    public void Update()
    {
        // timer to spawn the next goodie Object
        theCountdown -= Time.deltaTime;
        if(theCountdown <= 0)
        {
            SpawnGoodies ();
            theCountdown = waitingForNextSpawn;
        }
    }
  
  
    void SpawnGoodies()
    {
        Vector2 pos = new Vector2 (Random.Range (xMin, xMax), Random.Range (yMin, yMax));
        
        GameObject.Instantiate(feather, pos, Quaternion.Euler(0f, 0f ,Random.Range(0, 180)));
        
    }
}

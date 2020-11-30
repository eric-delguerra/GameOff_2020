using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public GameObject[] checkpoints;
    public GameObject[] checkpointsChange;
    private int checkpointIndex = 0;
    private int checkpointChangerIndex = 0;

    public void replaceSpawn()
    {
        var spawn = GameObject.Find("Spawn");
        spawn.transform.position = checkpoints[checkpointIndex].transform.position;
        checkpointIndex++;
        Destroy(checkpointsChange[checkpointChangerIndex]);
        checkpointChangerIndex++;

    }
}

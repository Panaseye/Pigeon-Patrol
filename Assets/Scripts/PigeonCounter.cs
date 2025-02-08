using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PigeonCounter : MonoBehaviour
{
    public string pigeonTag = "enemy";
    public float movementThreshold = 0.05f;
    public int checkFrames = 3;

    private Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();
    private bool isChecking = false;

    public void CountStationaryPigeons(Action<int> callback)
    {
        if (!isChecking)
        {
            StartCoroutine(CheckStationaryPigeonsCoroutine(callback));
        }
    }

    private IEnumerator CheckStationaryPigeonsCoroutine(Action<int> callback)
    {
        isChecking = true;
        int stationaryCount = 0;
        initialPositions.Clear();

        // Get all pigeons tagged as "enemy"
        GameObject[] pigeons = GameObject.FindGameObjectsWithTag(pigeonTag);
        foreach (GameObject pigeon in pigeons)
        {
            initialPositions[pigeon.transform] = pigeon.transform.position;
        }

        // Wait for the given number of frames
        for (int i = 0; i < checkFrames; i++)
        {
            yield return null; // Wait one frame
        }

        // Count pigeons that didn't move
        foreach (var entry in initialPositions)
        {
            Transform pigeon = entry.Key;
            Vector3 initialPos = entry.Value;
            Vector3 currentPos = pigeon.position;
            

            if (Vector3.Distance(initialPos, currentPos) < movementThreshold)
            {
                stationaryCount++;
            }
        }

        isChecking = false;
        callback?.Invoke(stationaryCount); // Call the callback with the result
    }
}
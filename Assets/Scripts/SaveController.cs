using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController instance;

    public SavePoint[] savePoints;

    public Vector3 spawnPoint;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {   
        savePoints = FindObjectsOfType<SavePoint>();

        spawnPoint = PlayerController.instance.transform.position;
        
    }

    public void DeactivateSavePoints()
    {
        for (int i = 0; i < savePoints.Length; i++)
        {
            savePoints[i].ResetSavePoint();
        }
    }

    public void NewSpawnPoint(Vector3 newPos)
    {
        spawnPoint = newPos;
    }
}

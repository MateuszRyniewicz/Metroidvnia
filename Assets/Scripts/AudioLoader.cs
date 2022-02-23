using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    public AudioManager aM;

    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            AudioManager newAM = Instantiate(aM);
            AudioManager.instance = newAM;
            DontDestroyOnLoad(newAM.gameObject);
        }
    }
}

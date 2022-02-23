using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource mainMenuMusic;
    public AudioSource levelMusic;
    public AudioSource bossMusic;

    public AudioSource[] sfx;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }       
    }

    public void MainMenuMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();

        mainMenuMusic.Play();
    }

    public void LevelMusic()
    {
        if (!levelMusic.isPlaying)
        {

            bossMusic.Stop();
            mainMenuMusic.Stop();
            levelMusic.Play();
        }
    }

    public void BossMusic()
    {
        levelMusic.Stop();
        bossMusic.Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void PlaySFXAdjusted(int sfxToAdjust)
    {
        sfx[sfxToAdjust].pitch = Random.Range(0.8f, 1.2f);
        PlaySFX(sfxToAdjust);
    }
}

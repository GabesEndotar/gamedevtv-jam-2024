using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource mainMusic;
    [SerializeField]
    private AudioSource gameOver;

    void Start()
    {
        mainMusic.GetComponent<AudioSource>().Play();
    }
    public void GameOverSound()
    {
        mainMusic.GetComponent<AudioSource>().Stop();
        gameOver.GetComponent<AudioSource>().Play();
    }

}

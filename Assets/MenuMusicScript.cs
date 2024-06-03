using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
        audioSources[0].Play();
    }

    public void PlaySound()
    {
        audioSources[1].Play();
    }
    public void StopSound()
    {
        audioSources[1].Stop();
    }
}
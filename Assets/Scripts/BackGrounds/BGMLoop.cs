using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMLooper : MonoBehaviour
{
    public AudioSource audioSource;
    public int loopStartSample = 287005;
    public int loopLengthSample = 8413247;
    private int loopEndSample;
    private bool isLooping = false;

    void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        loopEndSample = loopStartSample + loopLengthSample;
        audioSource.Play();
        isLooping = true;
    }

    void Update()
    {
        if (isLooping && audioSource.isPlaying)
        {
            if (audioSource.timeSamples >= loopEndSample)
            {
                audioSource.timeSamples = loopStartSample;
            }
        }
    }
}

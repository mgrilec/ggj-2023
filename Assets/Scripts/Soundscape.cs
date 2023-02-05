using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundscape : MonoBehaviour
{
    private new AudioSource audio;

    private float nextPlayTime;
    public float Min;
    public float Max;

    private void Awake()
    {
        audio = GetComponentInChildren<AudioSource>();
        nextPlayTime = Time.time + Random.Range(Min, Max);
    }

    private void Update()
    {
        if (Time.time > nextPlayTime)
        {
            nextPlayTime = nextPlayTime + Random.Range(Min, Max);
            audio.Play();
        }
    }
}

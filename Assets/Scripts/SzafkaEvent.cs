using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SzafkaEvent : MonoBehaviour
{
    public AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void playaudio()
    {
        source.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiatlaScipt : MonoBehaviour
{
    private CameraController ctr;

    public int coRobie; // 0 zgas swiatla, 1 spooky sound

    private AudioSource source;
    private bool gralem;
    private bool graj;
    public float onDelay;

    public GameObject staticHook;

    public GameObject endGame;

    public Doors oknoInspect;

    // Start is called before the first frame update
    void Start()
    {
        ctr = GameObject.Find("GeneralController").GetComponent<CameraController>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (graj)
        {
            if (onDelay <= 0)
            {
                
            }
            else
            {
                onDelay -= Time.deltaTime;
            }


        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(coRobie == 0 && !gralem)
            {
                ctr.zgasSwiatla();
                //graj = true;
                gralem = true;
            }
            if(coRobie == 1 && !gralem)
            {
                oknoInspect.WhatAmSaying = "Gdzie TO siê podzialo...";
                source.Play();
                gralem = true;
                staticHook.SetActive(false);
            }
            if(coRobie == 2 && !gralem)
            {
                this.gameObject.SetActive(false);
                endGame.SetActive(true);
            }

        }
    }
}

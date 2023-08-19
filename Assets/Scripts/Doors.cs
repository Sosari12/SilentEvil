using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Doors : MonoBehaviour
{
    public int typeOfInter;
    private Animator anim;
    public Animator animBlack;
    private TextMeshProUGUI napis;
    private Text coMowie;
    private CameraController ctr;
    public string WhatAmSaying = "";
    bool openDoor;
    float desiredRot;
    float desiredRotStart;
    public float rotSpeed = 250;
    public float damping = 10;
    bool rotFinished;
    public Teleport tp;
    public bool juzOtwarte;
    public GameObject swiatla;

    private bool backsoon;
    private float timetoBack = 3f;

    public AudioSource source;

    private int ilerazy = 0;
    public bool otwierajszafe;
    public float szafaZ;

    public GameObject particle;

    private MeshCollider meshcol;

    [Header("Co Daje?")]
    public int CoDaje; //0 klucz do garagu, 1 axe, 2 shotgun, 3 ammoPistol, 4 ammoShotgun, 5 healthpack, 6 kluczDoBiura; 
    


    // Start is called before the first frame update
    void Start()
    {
        if (typeOfInter == 0) anim = this.GetComponent<Animator>();
        ctr = GameObject.Find("GeneralController").GetComponent<CameraController>();
        napis = ctr.napis;
        desiredRot = transform.eulerAngles.y;
        desiredRotStart = transform.eulerAngles.y;
        source = GetComponent<AudioSource>();
        
        //szafaZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor && !rotFinished)
        {
            if (desiredRot > desiredRotStart + 110f) rotFinished = true;
             desiredRot += rotSpeed * Time.deltaTime;
            var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, desiredRot, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * damping);
        }


        if (backsoon)
        {

            timetoBack -= Time.deltaTime;

            if (timetoBack <= 0)
            {
                backsoon = false;
                //animBlack.SetTrigger("Back");
                animBlack.SetBool("Trans", true);

            }


        }

        if (otwierajszafe && !juzOtwarte)
        {
            if (szafaZ < 0.12f)
            {
                szafaZ += 0.1f * Time.deltaTime;
                transform.position -= new Vector3(0f, 0f, szafaZ);
            }
            else
            {
                juzOtwarte = true;
            }

        }

    }

    public void Interact()
    {
        if(typeOfInter == 0)//doors
        {
            GetComponent<MeshCollider>().enabled = false;
            //anim.SetTrigger("Open");
            openDoor = true;
            source.Play();
        }
        if (typeOfInter == 1)//just inspect
        {
            napis.SetText(WhatAmSaying);
            ctr.ActivateCutscene(2f);
            if(source)source.Play();
        }
        if (typeOfInter == 2 && !juzOtwarte)//pick up
        {
            if (particle != null) particle.SetActive(false);
             juzOtwarte = true;
            napis.SetText(WhatAmSaying);
            ctr.ActivateCutscene(1.5f);
            ctr.AddItem(CoDaje);
            if (source) source.Play();
            //add to eq
        }


        if(typeOfInter == 4)//drzwi tp
        {
            ctr.ActivateCutscene(3f);
            if(ctr.father.posiadamKlucz1 == true)
            {
                tp.father = ctr.father.transform;
                tp.telep = true;
                ctr.father.posiadamKlucz1 = false;
                //animBlack.SetTrigger("Dead");
                animBlack.SetBool("Trans", false);
                backsoon = true;
                source.Play();
            }
            else
            {
                napis.SetText(WhatAmSaying);
            }
        }
        if(typeOfInter == 5 && !juzOtwarte)//drzwi klucz 0
        {
            
            if (ctr.father.posiadamKlucz1 == true)
            {
                openDoor = true;
                juzOtwarte = true;
                ctr.father.posiadamKlucz1 = false;
                source.Play();
            }
            else
            {
                ctr.ActivateCutscene(2f);
                napis.SetText(WhatAmSaying);
            }

        }
        if (typeOfInter == 6 && !juzOtwarte)//drzwi klucz 6
        {
            
            if (ctr.father.posiadamKlucz2 == true)
            {
                openDoor = true;
                juzOtwarte = true;
                ctr.father.posiadamKlucz2 = false;
                source.Play();
            }
            else
            {
                ctr.ActivateCutscene(2f);
                napis.SetText(WhatAmSaying);
            }

        }
        if(typeOfInter == 7)//generator
        {
            ctr.ActivateCutscene(2f);

            if (ctr.swiatlaOn)
            {
                napis.SetText("Lepiej tego nie wylaczac...");
            }
            else
            {
                if (ilerazy == 1) ctr.activateEndGame();
                if (ilerazy == 0) napis.SetText("Tak bedzie troche razniej...");
                if (ilerazy == 1) napis.SetText("Telefon? Lepiej go odebrac...");

                ilerazy++;
                source.Play();
                ctr.swiatlaOn = true;
                swiatla.SetActive(true);
                ctr.father.posiadamKlucz2 = true;
            }
        }

        if(typeOfInter == 8 && !juzOtwarte && !otwierajszafe)
        {
            source.Play();
            //juzOtwarte = true;
            otwierajszafe = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraController : MonoBehaviour
{
    public Camera[] Cameras;
    private GameObject[] CameraHitBox;
    public int CurrentCamera = 0;
    public TextMeshProUGUI napis;
    public TextMeshProUGUI pistolText;
    public TextMeshProUGUI shotgunText;
    public TextMeshProUGUI hpText;

    public bool Cutscene;

    private AudioListener listener;
    private float CutsceneTime;
    private bool pobranoCzas = false;

    public CharacterMovement father;

    public bool swiatlaOn;
    public AudioSource source;
    public GameObject swiatla;

    public GameObject telefon;

    public Doors wajcha;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pistolText.SetText("Pistol Ammo: " + father.pistolAmmo);
        shotgunText.SetText("Shotgun Ammo: " + father.shotgunAmmo);
        hpText.SetText("Health: " + father.health);

        if (Cutscene == true)
        {
            if(CutsceneTime <= 0)
            {
                Cutscene = false;
                pobranoCzas = false;
                napis.SetText(" ");
            }
            else
            {
                CutsceneTime -= Time.deltaTime;
            }


        }

    }

    public void SwitchCamera(int number)
    {
        if (Cameras[CurrentCamera].enabled)
        {

            Cameras[number].GetComponent<AudioListener>().enabled = true;
            Cameras[number].enabled = true;
            Cameras[CurrentCamera].enabled = false;
            Cameras[CurrentCamera].GetComponent<AudioListener>().enabled = false;

            CurrentCamera = number;
        }

    }

    public void ActivateCutscene(float time)
    {
        if (pobranoCzas == false)
        {
            CutsceneTime = time;
            Cutscene = true;
            pobranoCzas = true;
        }
    }

    public void AddItem(int ktory)
    {
        if (ktory == 0) father.posiadamKlucz1 = true;
        if (ktory == 6) father.posiadamKlucz2 = true;
        if (ktory == 1) father.posiadamAxe = true;
        if (ktory == 2) father.posiadamShotgun = true;
        if (ktory == 3 || ktory == 4 || ktory == 5) father.addItem(ktory);

    }

    public void zgasSwiatla()
    {
        source.Play();
        swiatla.SetActive(false);
        swiatlaOn = false;
        wajcha.GetComponent<AudioSource>().Pause();
    }


    public void activateEndGame()
    {
        telefon.SetActive(true);

    }

    
}

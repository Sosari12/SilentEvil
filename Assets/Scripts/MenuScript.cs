using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Animator[] buttons;

    int ktory;

    bool ruszylo;

    public GameObject rules;
    bool czyRules;
    float timer = 2f;

    public Animator BlackScreen;
    bool ladowac;
    float ladowacTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) && ktory > 0) ktory--;
        if (Input.GetKeyDown(KeyCode.S) && ktory < 2) ktory++;

        if (Input.anyKeyDown) ruszylo = true;


        if (ruszylo)
        {
            if(ktory > 0) buttons[ktory - 1].SetBool("Bigger", false);
            buttons[ktory].SetBool("Bigger", true);
            if (ktory < 2) buttons[ktory + 1].SetBool("Bigger", false);
        }

        if(ktory == 0 && Input.GetKeyDown(KeyCode.E))
        {
            rules.SetActive(true);
            czyRules = true;
        }

        if(ktory == 1 && Input.GetKeyDown(KeyCode.E))
        {
            Application.Quit();

        }


        if (czyRules)
        {
            if(timer <= 0)
            {
                
                if (Input.anyKeyDown)
                {
                    ladowac = true;
                    BlackScreen.SetBool("Trans", false);
                }

            }
            else
            {
                timer -= Time.deltaTime;
            }

            if (ladowac)
            {
                if(ladowacTime <= 0)
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    ladowacTime -= Time.deltaTime;
                }
            }

                

        }


    }
}

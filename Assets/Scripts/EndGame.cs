using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    public AudioSource[] source;
    public float timer;
    private float timerMax;
    public int i;
    bool reverse;
    public int x;

    public Animator BlackScreen;
    private float timeToSwitch = 2f;

    // Start is called before the first frame update
    void Start()
    {
        timerMax = timer;
    }

    // Update is called once per frame
    void Update()
    {
        BlackScreen.SetTrigger("Instant");
        if (timer <= 0)
        {

            timer = timerMax;


            if (i >= source.Length)
            {
                i = 0;
                reverse = true;
            }
            else
            {
                source[i].Play();
                    i++;
                    x++;
                
            }


        }
        else
        {
            timer -= Time.deltaTime;

        }

        if(reverse)
        {
            
            if(timeToSwitch <= 0)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                timeToSwitch -= Time.deltaTime;
            }
        }

    }
}

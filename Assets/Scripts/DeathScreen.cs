using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public CharacterMovement father;
    public bool dead;
    private Image myimage;
    private float alpha;
    Color tempColor;

    public Color redColor;
    public Color grayColor;

    public TextMeshProUGUI[] texty;

    //fade
    private float timeElapsed;
    private float valueToLerp;
    private float delayTime = 5f;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        myimage = GetComponent<Image>();
       // anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(father.health <= 0 && !dead)
        {

            dead = true;

        }

        if (dead)
        {
            if(alpha < 1)
            {


                valueToLerp = Mathf.Lerp(1, 0, timeElapsed / delayTime);
                timeElapsed += Time.deltaTime;
                alpha += 0.1f * Time.deltaTime;

                //black screen
                tempColor = myimage.color;
                //tempColor.a = valueToLerp;
                //myimage.color = tempColor;


                //anim.SetTrigger("Dead");
                anim.SetBool("Trans", false);


                //texty
                texty[0].color = redColor;
                texty[1].color = grayColor;
                texty[2].color = grayColor;
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene(0);
            }

        }



    }
}

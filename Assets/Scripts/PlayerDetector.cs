using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    bool seeEnemy;
    public CharacterMovement father;
    public EnemyScript spottedEnemy;
    public int damage;
    // public GameObject bloodSplash;
    public AudioSource source;

    public AudioClip pistolclip;
    public AudioClip shotgunclip;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            seeEnemy = true;
            if(!spottedEnemy)spottedEnemy = other.GetComponent<EnemyScript>();
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            seeEnemy = false;
            spottedEnemy = null;

        }
    }

    public void Shoot()
    {
        if(!father.shotgun && !father.axe)
        {
            source.clip = pistolclip;
            source.Play();
        }

        if (spottedEnemy && !father.shotgun && !father.axe)
        {
            spottedEnemy.TakeDamage(damage);
            //GameObject bloodSplashObj = Instantiate(bloodSplash, spottedEnemy.transform.position, Quaternion.identity);
            spottedEnemy = null;

        }
        if (father.shotgun)
        {
            source.clip = shotgunclip;
            source.Play();
        }
        if (father.axe)
        {

        }
    }


}

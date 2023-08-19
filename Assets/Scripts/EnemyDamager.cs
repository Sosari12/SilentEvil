using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public EnemyScript father;
    bool seePlayer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            seePlayer = true;
            other.GetComponent<CharacterMovement>().takeDamage(father.damage);
            gameObject.SetActive(false);
        }
    }




}

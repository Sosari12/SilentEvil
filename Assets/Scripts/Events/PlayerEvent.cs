using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    public CharacterMovement father;
    public Transform slashTrans;
    public GameObject slashAoe;
    public GameObject shotgunAoe;

    public GameObject krokPref;
    public Transform krokSpawn;

    public float graczaY;

    public void ShootEvent()
    {
        //flash
    }

    public void exitIdle()
    {
        father.blockedWalking = false;
    }

    public void slash()
    {
        GameObject slashObj = Instantiate(slashAoe, slashTrans.position, father.transform.rotation);
    }

    public void shotgunShot()
    {
        GameObject slashObj = Instantiate(shotgunAoe, slashTrans.position, father.transform.rotation);
    }

    public void krok()
    {
        GameObject krokOnj = Instantiate(krokPref, krokSpawn.position, Quaternion.identity);
    }
}

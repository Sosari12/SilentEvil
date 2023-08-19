using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookEvent : MonoBehaviour
{
    public EnemyScript father;
    public GameObject col;

    public void resetAttack()
    {
        father.ResetAttack();
    }

    public void EnableCollider()
    {
        col.SetActive(true);
    }

    public void DisableCollider()
    {
        col.SetActive(false);
    }
}

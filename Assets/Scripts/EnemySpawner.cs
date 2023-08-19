using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Tranfroms")]
    public Transform spawnPoint;
    public Transform spawnTrigger;
    public GameObject enemy;

    public bool triggered;

    [Header("=Jest na Trigger=")]
    public bool usesTrigger;

    [Header("=Je¿eli ma sie powtarzac=")]
    public bool repeat;

    [Header("=Je¿eli na timer to daj > 0 i true=")]
    public bool isOnTimer;
    public float timeMax;
    float timecd;

    private void Awake()
    {
        timecd = timeMax;
    }


    // Update is called once per frame
    void Update()
    {

        if (triggered || isOnTimer) SpawnOnTrigger();



    }


    public void SpawnOnTrigger()
    {


            if (timecd <= 0)
            {
                
                GameObject enemyObj = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
                if (!repeat)
                {
                    timecd = timeMax;
                    Destroy(spawnPoint);
                    Destroy(spawnTrigger);
                    Destroy(gameObject);
                }
                else
                {
                timecd = timeMax;
                }
            }
            else
            {
                timecd -= Time.deltaTime;

            }


    }

}

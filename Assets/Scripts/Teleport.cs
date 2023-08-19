using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public bool telep;
    public float tim;
    public Transform father;
    public Transform tpTo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (telep)
        {
            tim -= Time.deltaTime;
            if (tim <= 0)
            {
                father.position = tpTo.position;
                telep = false;
            } 
        }
    }
}

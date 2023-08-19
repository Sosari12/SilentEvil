using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChecker : MonoBehaviour
{
    public CameraController father;
    public int myNumber;
    public bool seePlayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && father.CurrentCamera != myNumber)
        {
            //father.CurrentCamera = myNumber;
            seePlayer = true;
            father.SwitchCamera(myNumber);
        }
        else
        {
            seePlayer = false;
        }
    }


}

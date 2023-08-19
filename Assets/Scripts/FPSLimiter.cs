using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSLimiter : MonoBehaviour
{
    public int target = 30;

    void Awake()
    {
        
        Application.targetFrameRate = target;
    }

    void Update()
    {
        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;

        if(Input.GetKeyDown(KeyCode.F10))SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }
}

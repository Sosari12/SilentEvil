using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeDmg : MonoBehaviour
{
    public int Damage;
    public Light pointLight;
    public bool shoty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.5f);
        if(pointLight.range < 10 && shoty)pointLight.range += 5f * Time.deltaTime;
    }

}

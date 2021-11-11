using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    float damage = 11f;

    public int currentAmmo;
    public int maxAmmo = 16;


    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnFire()
    {
        if (currentAmmo >= 0)
        {
            currentAmmo--;            
        }
    }
}

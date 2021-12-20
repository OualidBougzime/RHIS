using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPoisonBullet : Bullet
{

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies"))
        {
            Debug.Log("Contact");
            other.GetComponent<EnemyStatus>().SuperPoison();
            Hit();
        }
        else if (!other.CompareTag("Water") && !other.CompareTag("Player"))
        {
            Hit();
        }
    }

}

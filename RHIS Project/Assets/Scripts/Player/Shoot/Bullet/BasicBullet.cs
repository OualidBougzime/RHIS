using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies"))
        {
            other.GetComponent<EnemyStatus>().GetDamage(damage);
            Hit();
        }
        else if (!other.CompareTag("Water") && !other.CompareTag("Player"))
        {
            Hit();
        }
    }

}

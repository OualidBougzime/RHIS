using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : Bullet
{

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies"))
        {
            other.GetComponent<EnemyStatus>().GetDamage(damage);
            other.GetComponent<EnemyStatus>().Freeze();
            Hit();
        }
        else if (!other.CompareTag("Water") && !other.CompareTag("Player"))
        {
            Hit();
        }
    }

}

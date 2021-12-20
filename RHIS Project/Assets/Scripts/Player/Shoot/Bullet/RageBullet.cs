using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBullet : Bullet
{

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies"))
        {
            other.GetComponent<EnemyStatus>().GetDamage(damage);
            other.GetComponent<EnemyStatus>().Rage();
            Hit();
        }
        else if (!other.CompareTag("Water") && !other.CompareTag("Player"))
        {
            Hit();
        }
    }

}

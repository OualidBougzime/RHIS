using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;

    public void Hit()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
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

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    public int GetDamage()
    {
        return damage;
    }
}

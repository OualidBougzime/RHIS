using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected int damage;

    public void Hit()
    {
        Destroy(gameObject);
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies"))
        {
            other.GetComponent<EnemyStatus>().GetDamage(damage);
            //GetComponentInParent<Weapon>().Effect();
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

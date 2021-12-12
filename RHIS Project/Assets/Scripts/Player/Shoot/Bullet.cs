using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int damage;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == null)
        {
            Hit();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStatus>().GetDamage(damage);
            Hit();
        }
    }
}

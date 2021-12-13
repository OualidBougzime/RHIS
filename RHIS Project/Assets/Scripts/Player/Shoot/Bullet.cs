using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private WeaponEffects effect;

    public void Hit()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies"))
        {
            other.GetComponent<EnemyStatus>().GetDamage(damage);
            
            if (effect == WeaponEffects.FIRE) {
                other.GetComponent<EnemyStatus>().Burning();
            }
            else if (effect == WeaponEffects.ICE) {
                other.GetComponent<EnemyStatus>().Freeze();
            }
            else if (effect == WeaponEffects.POISON) {
                other.GetComponent<EnemyStatus>().Poison();
            }
            else if (effect == WeaponEffects.SUPER_POISON) {
                other.GetComponent<EnemyStatus>().SuperPoison();
            }
            else if (effect == WeaponEffects.RAGE) {
                other.GetComponent<EnemyStatus>().Rage();
            }
            
            Hit();
        }
        else if (!other.CompareTag("Water") && !other.CompareTag("Player"))
        {
            Hit();
        }
    }

    public void SetEffect(WeaponEffects effect)
    {
        this.effect = effect;
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

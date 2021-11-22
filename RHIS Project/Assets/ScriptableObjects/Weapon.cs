using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public string Name;
    [SerializeField] public float FiringRate;
    [SerializeField] public float WeaponDamage;
    [SerializeField] public int Accuracy;
    [SerializeField] public int WeaponBonus;

    [SerializeField] public int MagazineSize;
    [SerializeField] public int MaxAmmo;

    public int TotalAmmo;
    public int AmmoInMagazine;

    public int Force;

    public void ReloadWeapon()
    {
        if (TotalAmmo + AmmoInMagazine >= MagazineSize)
        {
            TotalAmmo -= MagazineSize - AmmoInMagazine;
            AmmoInMagazine = MagazineSize;
        }

        else
        {
            AmmoInMagazine += TotalAmmo;
            TotalAmmo = 0;
        }
    }

    public IEnumerator ShootWeapon()
    {
        if (AmmoInMagazine > 0)
        {
            AmmoInMagazine--;
            float waitTime = (1/FiringRate) * 3; // à tester selon les valeurs de cadence de tir
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void AddAmmo(int quantity)
    {
        if (MaxAmmo >= TotalAmmo + quantity)
        {
            TotalAmmo += quantity;
        }
        else
        {
            TotalAmmo = MaxAmmo;
        }
    }
}

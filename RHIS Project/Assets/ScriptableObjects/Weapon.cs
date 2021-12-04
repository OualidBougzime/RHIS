using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponEffect
{
    DEFAULT,
    POISON,
    SUPER_POISON,
    FIRE,
    ICE,
    RAGE
}

public class Weapon : MonoBehaviour
{
    [SerializeField] public string Name;
    [SerializeField] public float FiringRate;
    [SerializeField] public float WeaponDamage;
    [SerializeField] public int Accuracy;
    [SerializeField] public int WeaponBonus;

    [SerializeField] public int MagazineSize;
    [SerializeField] public int MaxAmmo;

    [SerializeField] public WeaponEffect Effect;

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

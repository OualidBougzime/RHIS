using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ItemObject
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

    public void Awake()
    {
        typeObject = ItemType.Weapon;
    }

    public void Update()
    {
        ReloadWeapon();
    }

    public void ReloadWeapon()
    {
        if (Input.GetButton("Reload"))
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
    }
}

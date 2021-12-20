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
    [SerializeField] Effect effect;
    [SerializeField] public string Name;
    [SerializeField] public int WeaponBonus;

    [SerializeField] [Range(1, 100)] private int firingRate;
    [SerializeField] [Range(0, 100)] private int reloadTime;
    [SerializeField] [Range(0, 100)] private int damageBonus;
    [SerializeField] [Range(0, 100)] private int accuracyBonus;
    [SerializeField] [Range(0, 100)] private int rangeBonus;
    [SerializeField] [Range(0, 100)] private int shotSpeedBonus;

    [SerializeField] public int MagazineSize;
    [SerializeField] public int MaxAmmo;

    [SerializeField] public WeaponEffect Effect;


    public int TotalAmmo;
    public int AmmoInMagazine;

    public int Force;

    [SerializeField] private GameObject bullet;
    [SerializeField] private FireStyle fireStyle;

    private Transform myTransform;
    private Coroutine firingCoroutine;
    private bool fire;
    private bool reload;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        fire = true;
        reload = false;
    }


    public void Reload()
    {
        if (!reload)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }
    IEnumerator ReloadCoroutine()
    {
        
        reload = true;
        yield return new WaitForSeconds(reloadTime/10f);
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
        reload = false;
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

    public void StartFire()
    {
        if (firingCoroutine == null && fire)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
            fire = false;
        }
    }

    public IEnumerator StopFire()
    {
        yield return new WaitUntil(() => fire);
        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            yield return new WaitUntil(() => reload == false);
            Fire();
            float time = 10 / (float)firingRate;
            yield return new WaitForSeconds(time * 0.9f);
            fire = true;
            yield return new WaitForSeconds(time * 0.1f);
        }
    }

    public void Fire()
    {
        if (AmmoInMagazine <= 0)
        {
            Reload();
            return;
        }
        Vector3 screenSize = new(Screen.width, Screen.height);
        Vector3 direction = (Input.mousePosition - screenSize / 2).normalized;
        fireStyle.Fire(bullet, myTransform.position, direction, this);

        AmmoInMagazine--;
    }

    public int GetRange()
    {
        return rangeBonus;
    }

    public void SetRange(int range)
    {
        rangeBonus = range;
    }
    public int GetShotSpeed()
    {
        return shotSpeedBonus;
    }

    public void SetShotSpeed(int shotSpeed)
    {
        shotSpeedBonus = shotSpeed;
    }
    public int GetAccuracy()
    {
        return accuracyBonus;
    }

    public void SetAccuracy(int accuracy)
    {
        accuracyBonus = accuracy;
    }
    public int GetDamage()
    {
        return damageBonus;
    }

    public void SetDamage(int damage)
    {
        damageBonus = damage;
    }
}

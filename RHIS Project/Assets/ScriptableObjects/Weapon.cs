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
    [SerializeField] public float WeaponDamage;
    [SerializeField] public int WeaponBonus;

    [SerializeField] [Range(1, 100)] private int firingRate;
    [SerializeField] [Range(0, 100)]  private int accuracyBonus;
    [SerializeField] [Range(0, 100)]  private int rangeBonus;
    [SerializeField] [Range(0, 100)]  private int shotSpeedBonus;

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

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        fire = true;
    }
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
            Fire();
            float time = 10 / (float)firingRate;
            yield return new WaitForSeconds(time * 0.9f);
            fire = true;
            yield return new WaitForSeconds(time * 0.1f);
        }
    }

    public void Fire()
    {
        Vector3 screenSize = new(Screen.width, Screen.height);
        Vector3 direction = (Input.mousePosition - screenSize / 2).normalized;
        List<GameObject> bullets = fireStyle.Fire(bullet, myTransform.position, direction);


        AmmoInMagazine--;
    }
}

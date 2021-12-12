using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStyle : MonoBehaviour
{
    private Transform myTransform;

    [SerializeField] private int nbrBullets;
    [SerializeField] [Range(0, 100)] private int accuracy;
    [SerializeField] [Range(0, 100)] private int damage;
    [SerializeField] [Range(1, 100)] private int range;
    [SerializeField] [Range(1, 100)] private int shotSpeed;


    public void Fire(GameObject bullet,Vector3 position, Vector3 direction, Weapon weapon)
    {
        for (int i = 0; i < nbrBullets; ++i)
        {
            //Vector3 accuracyDirection = new (direction.x + Random.Range(-50+accuracy/2,50-accuracy/2)/100f, direction.y + Random.Range(-50 + accuracy / 2, 50 - accuracy / 2)/100f);
            Vector3 accuracyDirection = new (direction.x + Random.Range(Mathf.Clamp(-50+(accuracy + weapon.GetAccuracy())/2,-50,0), Mathf.Clamp(50-(accuracy + weapon.GetAccuracy())/2,0,50))/100f, direction.y + Random.Range(Mathf.Clamp(-50 + (accuracy + weapon.GetAccuracy()) / 2, -50, 0), Mathf.Clamp(50 - (accuracy + weapon.GetAccuracy()) / 2, 0, 50)) / 100f);
            accuracyDirection = accuracyDirection.normalized;
            Quaternion q = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan(accuracyDirection.y / accuracyDirection.x));
            if (accuracyDirection.x < 0)
            {
                q.eulerAngles = new Vector3(0,0,q.eulerAngles.z + 180);
            }
            GameObject instance = Instantiate(bullet, position, q);

            Rigidbody rigidbodyInstance = instance.GetComponent<Rigidbody>();
            if (rigidbodyInstance != null)
            {
                rigidbodyInstance.velocity = accuracyDirection * (shotSpeed + weapon.GetShotSpeed()) / 10;
            }

            Bullet bulletInstance = instance.GetComponent<Bullet>();
            if (bulletInstance != null)
            {
                bulletInstance.SetDamage(weapon.GetDamage() + damage);
            }

            Destroy(instance, (range + weapon.GetRange()) / 10f);


        }
    }
}

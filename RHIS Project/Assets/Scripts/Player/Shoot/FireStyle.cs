using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStyle : MonoBehaviour
{
    private Transform myTransform;

    [SerializeField] private int nbrBullets;
    [SerializeField] [Range(0, 100)] private int accuracy;
    [SerializeField] [Range(0, 100)] private int range;
    [SerializeField] [Range(1, 100)] private int shotSpeed;


    public List<GameObject> Fire(GameObject bullet,Vector3 position, Vector3 direction)
    {
        List<GameObject> bullets = new();

        for (int i = 0; i < nbrBullets; ++i)
        {
            Vector3 accuracyDirection = new (direction.x + Random.Range(-50+accuracy/2,50-accuracy/2)/100f, direction.y + Random.Range(-50 + accuracy / 2, 50 - accuracy / 2)/100f);
            accuracyDirection = accuracyDirection.normalized;
            print(Vector3.Angle(transform.forward, accuracyDirection));
            Quaternion q = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan(accuracyDirection.y / accuracyDirection.x));
            if (accuracyDirection.x < 0)
            {
                q.eulerAngles = new Vector3(0,0,q.eulerAngles.z + 180);
            }
            GameObject instance = Instantiate(bullet, position, q);

            Rigidbody rb = instance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = accuracyDirection * shotSpeed/10;
            }

            bullets.Add(instance);
        }

        return bullets;
    }
}

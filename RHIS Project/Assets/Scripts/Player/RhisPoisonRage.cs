using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhisPoisonRage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.tag != "Player")
            {
                print(collider);
                bool isPoisoned = collider.GetComponent<EnemyStatus>().GetPoisoned();
                if (isPoisoned == false)
                {
                    collider.GetComponentInParent<EnemyStatus>().SetPoisoned(true);
                    collider.GetComponent<EnemyStatus>().Freeze();
                }
            }
        }
    }

}

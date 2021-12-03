using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhisPoison : MonoBehaviour
{
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameObject.tag = "Player";
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1);

        foreach (Collider collider in hitColliders)
        {
            if (!collider.gameObject.CompareTag("Player") && !collider.gameObject.CompareTag("Backdrops"))
            {
                print(collider);
                bool isPoisoned = collider.GetComponent<EnemyStatus>().GetPoisoned();
                if (isPoisoned == false)
                {
                    collider.GetComponentInParent<EnemyStatus>().SetPoisoned(true);
                    collider.GetComponent<EnemyStatus>().Poison();
                }
            }
        }
    }

}
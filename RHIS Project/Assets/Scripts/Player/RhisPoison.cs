using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhisPoison : MonoBehaviour
{
    [SerializeField] private float range = 0.3f;

    //private Transform poisonCircle;
    // Start is called before the first frame update

    private void Awake()
    {
        //poisonCircle = transform.GetChild(1).GetComponentInChildren<Transform>();
    }
    protected virtual void Start()
    {
        gameObject.tag = "Player";
        //poisonCircle.localScale = new(range * 20, range * 20);
    }

    // Update is called once per frame
    /*protected virtual void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range);

        foreach (Collider collider in hitColliders)
        {
            if (!collider.CompareTag("Player") && !collider.CompareTag("Backdrops") && !collider.CompareTag("Water"))
            {
                //print(collider);
                bool isPoisoned = collider.GetComponent<EnemyStatus>().GetPoisoned();
                if (isPoisoned == false)
                {
                    collider.GetComponentInParent<EnemyStatus>().SetPoisoned(true);
                    collider.GetComponent<EnemyStatus>().Poison();
                }
            }
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies"))
        {
            bool isPoisoned = other.GetComponent<EnemyStatus>().GetPoisoned();
            if (isPoisoned == false)
            {
                other.GetComponent<EnemyStatus>().SetPoisoned(true);
                other.GetComponent<EnemyStatus>().Poison();
            }
        }
    }

}
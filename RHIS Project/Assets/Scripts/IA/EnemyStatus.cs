using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] bool poisoned = false;
    [SerializeField] int healthPointMax = 20;
    int healthPoint;

    // Start is called before the first frame update
    void Start()
    {
        healthPoint = healthPointMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPoisoned(bool status)
    {
        poisoned = status;
    }

    public bool GetPoisoned()
    {
        return poisoned;
    }

    public void GetDamage(int damage)
    {
        if(this.healthPoint > damage)
        {
            healthPoint -= damage;
            GetComponentInChildren<HealthBarBehaviour>().setHealth(healthPoint, healthPointMax);
        }
        else
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void Poison()
    {
        Coroutine firingCoroutine = StartCoroutine(FireContinuously());

    }
    IEnumerator FireContinuously()
    {
        int timePoisoned = 0;
        while (timePoisoned < 20)
        {
            GetDamage(10);

            float timeToNextDamage = 5;

            timePoisoned += 5;

            /*audioPlayer.PlayHurtingClip();*/

            yield return new WaitForSeconds(timeToNextDamage);
        }
        if(timePoisoned >= 20)
        {
            poisoned = false;
        }
    }
}

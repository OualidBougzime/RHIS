using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] bool poisoned = false;
    [SerializeField] int healthPoint = 20;

    // Start is called before the first frame update
    void Start()
    {

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

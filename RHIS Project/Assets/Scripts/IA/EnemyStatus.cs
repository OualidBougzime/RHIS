using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private GameObject EffectAspect;
    [SerializeField] bool poisoned = false;
    [SerializeField] int healthPointMax = 20;
    [SerializeField] int healthPoint;
    [SerializeField] int slower;
    [SerializeField] int BasicDamage = 10;
    [SerializeField] int FireDamage = 5;
    [SerializeField] int IceDamage = 5;
    [SerializeField] int SuperDamage = 20;


    private EnemiesCounter counter;
    //Resources.load("Prefabs/truc") as GameObject;

    // Start is called before the first frame update
    void Start()
    {
        EffectAspect = Resources.Load("Poison") as GameObject;
        healthPoint = healthPointMax;
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
        counter.KillEnemy();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void Poison()
    {
        Coroutine poisonCoroutine = StartCoroutine(PoisonDamage(BasicDamage, EffectAspect));
        GameObject Instance = Instantiate(EffectAspect, transform);

        

    }

    public void Burning()
    {
        EffectAspect = Resources.Load("Fire") as GameObject;
        Coroutine poisonCoroutine = StartCoroutine(PoisonDamage(FireDamage, EffectAspect));
        GameObject Instance = Instantiate(EffectAspect, transform);
    }

    public void Freeze()
    {
        EffectAspect = Resources.Load("Ice") as GameObject;
        Coroutine poisonCoroutine = StartCoroutine(PoisonDamage(IceDamage, EffectAspect));
        gameObject.GetComponent<IaController>().Vitesse = gameObject.GetComponent<IaController>().Vitesse - slower;
        GameObject Instance = Instantiate(EffectAspect, transform);
    }

    public void SuperPoison()
    {
        EffectAspect = Resources.Load("SuperPoison") as GameObject;
        Coroutine poisonCoroutine = StartCoroutine(PoisonDamage(SuperDamage, EffectAspect));
        GameObject Instance = Instantiate(EffectAspect, transform);
    }

    public void Rage()
    {
        EffectAspect = Resources.Load("Rage") as GameObject;
        this.GetComponent<IaController>().SetTarget("Ennemy");
        GameObject Instance = Instantiate(EffectAspect, transform);
    }



    IEnumerator PoisonDamage(int damage, GameObject EffectAspect)
    {
        GameObject Instance = Instantiate(EffectAspect, transform);
        int timePoisoned = 0;
        while (timePoisoned < 5)
        {
            GetDamage(damage);

            float timeToNextDamage = 1;

            timePoisoned += 1;

            Debug.Log("damage");

            /*audioPlayer.PlayHurtingClip();*/

            yield return new WaitForSeconds(timeToNextDamage);
        }
        if(timePoisoned >= 5)
        {
            poisoned = false;
        }
    }

    
    public void SetCounter(EnemiesCounter counter)
    {
        this.counter = counter;
    }
}

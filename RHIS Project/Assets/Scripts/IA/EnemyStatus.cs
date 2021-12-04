using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private GameObject Effect;
    [SerializeField] bool poisoned = false;
    [SerializeField] int healthPointMax = 20;
    [SerializeField] int slower;
    [SerializeField] int BasicDamage = 10;
    [SerializeField] int FireDamage = 5;
    [SerializeField] int IceDamage = 5;
    [SerializeField] int SuperDamage = 20;

    int healthPoint;

    private EnemiesCounter counter;
    //Resources.load("Prefabs/truc") as GameObject;

    // Start is called before the first frame update
    void Start()
    {
        Effect = Resources.Load("Poison") as GameObject;
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
        counter.KillEnemy();
        Destroy(gameObject);
    }

    public void Poison()
    {
        Coroutine poisonCoroutine = StartCoroutine(PoisonDamage(BasicDamage));
        GameObject Instance = Instantiate(Effect, transform);

        

    }

    public void Burning()
    {
        Effect = Resources.Load("Fire") as GameObject;
        Coroutine poisonCoroutine = StartCoroutine(PoisonDamage(FireDamage));
        GameObject Instance = Instantiate(Effect, transform);
    }

    public void Freeze()
    {
        Effect = Resources.Load("Ice") as GameObject;
        Coroutine poisonCoroutine = StartCoroutine(PoisonDamage(IceDamage));
        gameObject.GetComponent<IaController>().Vitesse = gameObject.GetComponent<IaController>().Vitesse - slower;
        GameObject Instance = Instantiate(Effect, transform);
    }

    public void SuperPoison()
    {
        Effect = Resources.Load("SuperPoison") as GameObject;
        Coroutine poisonCoroutine = StartCoroutine(PoisonDamage(SuperDamage));
        GameObject Instance = Instantiate(Effect, transform);
    }

    public void Rage()
    {
        Effect = Resources.Load("Rage") as GameObject;
        this.GetComponent<IaController>().SetTarget("Ennemy");
        GameObject Instance = Instantiate(Effect, transform);
    }



    IEnumerator PoisonDamage(int damage)
    {
        int timePoisoned = 0;
        while (timePoisoned < 20)
        {
            GetDamage(damage);

            float timeToNextDamage = 5;

            timePoisoned += 5;

            /*audioPlayer.PlayHurtingClip();*/

            yield return new WaitForSeconds(timeToNextDamage);
        }
        if(timePoisoned >= 20)
        {
            poisoned = false;
            GetComponent<Effect>().Destroy();
        }
    }

    
    public void SetCounter(EnemiesCounter counter)
    {
        this.counter = counter;
    }
}

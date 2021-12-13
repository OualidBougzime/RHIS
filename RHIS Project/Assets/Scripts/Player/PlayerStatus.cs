using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] int healthPointMax = 20;
    [SerializeField] int healthPoint;

    private SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        healthPoint = healthPointMax;
        sceneLoader = gameObject.AddComponent<SceneLoader>();
    }

    public void GetDamage(int damage)
    {
        if (this.healthPoint > damage)
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
        sceneLoader.LoadGameOver();
    }

    public int GetHealth()
    {
        return healthPoint;
    }

    public int GetHealthMax()
    {
        return healthPointMax;
    }
}

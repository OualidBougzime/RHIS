using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour

{
    [SerializeField] PlayerStatus script;
    Slider HealthBar;
    int health;
    int healthMax = 20;
    // Start is called before the first frame update
    void Start()
    {
        if (script.GetHealthMax() != 0)
        healthMax = script.GetHealthMax();
    }

    // Update is called once per frame
    void Update()
    {
        health = script.GetHealth();
        GetComponent<Image>().fillAmount = health / (float)healthMax;
    }
}

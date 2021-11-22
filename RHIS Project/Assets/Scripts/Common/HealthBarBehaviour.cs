using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Color high;
    public Color low;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset.y+=2*transform.parent.localScale.y;
        slider.transform.position = transform.parent.position + offset;
    }

     public void setHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health <= maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low,high, slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.position = transform.parent.position + offset;
    }
}

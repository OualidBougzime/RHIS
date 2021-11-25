using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Color high;
    [SerializeField] private Color low;
    [SerializeField] private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset.y+=transform.parent.localScale.y/10;
        slider.transform.position = transform.parent.position + offset;
    }

     public void setHealth(int health, int maxHealth)
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
